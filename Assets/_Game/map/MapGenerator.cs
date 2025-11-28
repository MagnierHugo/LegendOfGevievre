using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.Tilemaps;
using SimplexNoise; 

public class MapGenerator : MonoBehaviour
{
    [Header("reference")]
    public Tilemap tilemap = null;

    [Header("map settings")]
    [SerializeField] private int xSize = 500;
    [SerializeField] private int ySize = 500;
    [SerializeField] private int chunkSize = 100;

    [Header("sprites")]
    public Sprite TileWater = null;
    public Sprite TileGrass = null;
    public Sprite TileDirt = null;
    public Sprite TileSand = null;
    public Sprite TileStone = null;
    public Sprite TileSnow = null;
    
    // CITY SPRITES
    public Sprite TileRoad = null;
    public Sprite TileSidewalk = null; 

    // BUILDING SPRITES
    public Sprite TileBuildingSmall = null; // Used for 2x2 blocks
    public Sprite TileBuildingTall = null;  // Used for 5x3 blocks
    public Sprite TileChurch = null;        // Used for 8x8 blocks

    [Header("noise settings")]
    public float noiseScale = 300f;
    public int strength = 4;
    [Range(0, 1)] public float persistence = 0.5f;
    public float unitySetter = 2f;

    [Header("Generation seed")]
    public int seed = 72486;
    public Vector2 offset = Vector2.zero;

    [Header("Biome Colors (For Blending)")]
    public Color deepWaterColor = new Color(0f, 0f, 0.5f, 1f);
    public Color waterColor = new Color(0f, 0.5f, 1f, 1f);
    public Color sandColor = new Color(1f, 0.9f, 0.6f, 1f);
    // LAND BLEND COLORS
    public Color dirtColor = new Color(0.6f, 0.4f, 0.2f, 1f);  
    public Color grassColor = new Color(0.4f, 0.8f, 0.4f, 1f); 
    public Color forestColor = new Color(0.1f, 0.5f, 0.1f, 1f); 
    // MOUNTAIN BLEND COLORS
    public Color stoneColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    public Color snowColor = new Color(1f, 1f, 1f, 1f);

    [Header("City Settings")]
    [SerializeField] private int cityRadius = 60; 
    [SerializeField] private int numberOfCities = 3; // NOUVEAU: Nombre de villes à générer
    [SerializeField] private int maxPlacementAttempts = 100;

    // Building definitions
    private readonly Vector2Int smallBuildingSize = new Vector2Int(2, 2);
    private readonly Vector2Int tallBuildingSize = new Vector2Int(5, 3);
    private readonly Vector2Int churchBuildingSize = new Vector2Int(8, 8); 

    // Private Tile instances
    private Tile deepWaterTile, waterTile, sandTile, grassTile, forestTile, dirtTile, stoneTile, snowTile;
    private Tile roadTile, sidewalkTile; 
    private Tile smallBuildingTile, tallBuildingTile; 
    private Tile churchTile; 

    // A list to track positions where buildings have been placed, preventing overlap
    // CETTE HASHSET EST MAINTENANT GLOBALE ET CONTIENT LES TUILES DE TOUTES LES VILLES
    private HashSet<Vector3Int> placedBuildingTiles = new HashSet<Vector3Int>();


    void Start()
    {
        // 1. Initialize Biome Tiles
        deepWaterTile = createTile(TileWater);
        waterTile = createTile(TileWater);
        sandTile = createTile(TileSand);
        grassTile = createTile(TileGrass);
        forestTile = createTile(TileGrass); 
        dirtTile = createTile(TileDirt);
        stoneTile = createTile(TileStone);
        snowTile = createTile(TileSnow);
        
        // 2. Initialize City Tiles
        roadTile = createTile(TileRoad);
        sidewalkTile = createTile(TileSidewalk); 
        
        // Initialize the new building tiles
        smallBuildingTile = createTile(TileBuildingSmall);
        tallBuildingTile = createTile(TileBuildingTall); 
        churchTile = createTile(TileChurch); 

        // 3. Start Map Generation
        StartCoroutine(GenerateMapAndCity());
    }

    // --- Utility Methods (inchangées) ---

    Tile createTile(Sprite sprite)
    {
        if (sprite == null)
        {
            Debug.LogError("Sprite is null when attempting to create a tile. Check public references.");
            return null;
        }

        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = sprite;
        tile.color = Color.white;
        return tile;
    }

    IEnumerator GenerateMapAndCity()
    {
        for (int x = 0; x < ySize; x += chunkSize)
            for (int y = 0; y < xSize; y += chunkSize)
            {
                GenerateChunk(x, y);
                yield return null;
            }

        Debug.Log("Natural Map Generation Complete. Attempting to place cities...");
        PlaceCities(); // CHANGÉ: Appel à la nouvelle fonction de placement
    }

    void GenerateChunk(int startX, int startY)
    {
        int currentChunkySize = Mathf.Min(chunkSize, ySize - startX);
        int currentChunkxSize = Mathf.Min(chunkSize, xSize - startY);

        int totalTiles = currentChunkySize * currentChunkxSize;
        Vector3Int[] positions = new Vector3Int[totalTiles];
        TileBase[] tiles = new TileBase[totalTiles];
        Color[] colors = new Color[totalTiles]; 

        int index = 0;

        for (int x = 0; x < currentChunkySize; x++)
        {
            for (int y = 0; y < currentChunkxSize; y++)
            {
                int worldX = startX + x;
                int worldY = startY + y;
                float elevation = GetFractalNoise(worldX, worldY, seed, noiseScale);
                float moisture = GetFractalNoise(worldX, worldY, seed + 5000, noiseScale);

                tiles[index] = GetBiomeTile(elevation, moisture);
                colors[index] = GetSmoothColor(elevation, moisture);
                positions[index] = new Vector3Int(worldX, worldY, 0);
                index++;
            }
        }

        tilemap.SetTiles(positions, tiles);

        for (int i = 0; i < totalTiles; i++)
        {
            tilemap.SetTileFlags(positions[i], TileFlags.None);
            tilemap.SetColor(positions[i], colors[i]);
        }
    }
    
    TileBase GetBiomeTile(float elevation, float moisture)
    {
        if (elevation < 0.25f) return deepWaterTile;
        if (elevation < 0.35f) return waterTile;
        if (elevation < 0.40f) return sandTile;
        
        if (elevation > 0.90f) return snowTile;
        if (elevation > 0.85f) return stoneTile;

        if (moisture < 0.3f) return dirtTile;
        else if (moisture < 0.55f) return grassTile;
        else return forestTile;
    }

    Color GetSmoothColor(float elevation, float moisture)
    {
        if (elevation < 0.25f) return deepWaterColor;
        
        if (elevation < 0.35f) return Color.Lerp(deepWaterColor, waterColor, (elevation - 0.25f) / 0.10f);
        
        if (elevation < 0.40f) return sandColor;

        if (elevation > 0.85f) 
        {
            float t = Mathf.InverseLerp(0.85f, 0.95f, elevation); 
            return Color.Lerp(stoneColor, snowColor, t);
        }

        if (moisture < 0.5f)
        {
            float t = moisture / 0.5f;
            return Color.Lerp(dirtColor, grassColor, t);
        }
        else
        {
            float t = (moisture - 0.5f) / 0.5f;
            return Color.Lerp(grassColor, forestColor, t);
        }
    }

    float GetFractalNoise(int x, int y, int seed, float scale)
    {
        float amplitude = 1;
        float frequency = 1;
        float noisexSize = 0;

        for (int i = 0; i < strength; i++)
        {
            float xCoord = (x + offset.x) / scale * frequency + seed;
            float yCoord = (y + offset.y) / scale * frequency + seed;
            noisexSize += Mathf.PerlinNoise(xCoord, yCoord) * amplitude;
            amplitude *= persistence;
            frequency *= unitySetter;
        }
        return Mathf.Clamp01(noisexSize / 1.5f);
    }

    // --- CITY GENERATION LOGIC ---

    // RENOMMÉ: Fonction pour tenter de placer toutes les villes
    void PlaceCities()
    {
        int placedCount = 0;
        int attempts = 0;
        
        int minCoord = cityRadius;
        int maxX = xSize - cityRadius;
        int maxY = ySize - cityRadius;

        // Boucle pour tenter de placer le nombre de villes spécifié
        while (placedCount < numberOfCities && attempts < maxPlacementAttempts * numberOfCities)
        {
            int randomX = Random.Range(minCoord, maxX); 
            int randomY = Random.Range(minCoord, maxY);
            attempts++;

            // NOUVEAU: Vérifie la disponibilité du terrain ET l'absence de chevauchement avec une autre ville
            if (IsAreaAvailableForNewCity(randomX, randomY))
            {
                Debug.Log($"City {placedCount + 1} successfully placed at: ({randomX}, {randomY}).");
                DrawCity(randomX, randomY);
                placedCount++;
            }
        }
        
        if (placedCount < numberOfCities)
        {
             Debug.LogWarning($"Could only place {placedCount} out of {numberOfCities} cities after {attempts} attempts.");
        }
        else
        {
            Debug.Log($"Successfully placed {placedCount} cities.");
        }
    }
    
    // RENOMMÉ: Vérifie si la zone est libre et sur un terrain adapté
    bool IsAreaAvailableForNewCity(int centerX, int centerY)
    {
        int minX = centerX - cityRadius;
        int maxX = centerX + cityRadius;
        int minY = centerY - cityRadius;
        int maxY = centerY + cityRadius;
        
        float sqrCityRadius = cityRadius * cityRadius;

        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                float sqrDist = (x - centerX) * (x - centerX) + (y - centerY) * (y - centerY);
                
                if (sqrDist < sqrCityRadius)
                {
                    // 1. Check for overlap with existing city (using the global placedBuildingTiles)
                    if (placedBuildingTiles.Contains(pos))
                    {
                        return false;
                    }

                    // 2. Check for environment suitability (no water or mountains)
                    float elevation = GetFractalNoise(x, y, seed, noiseScale);
                    
                    if (elevation < 0.40f || elevation > 0.85f)
                    {
                        return false;
                    }
                }
            }
        }
        
        return true;
    }

    void DrawCity(int centerX, int centerY)
    {
        int minX = centerX - cityRadius;
        int maxX = centerX + cityRadius;
        int minY = centerY - cityRadius;
        int maxY = centerY + cityRadius;
        
        float sqrCityRadius = cityRadius * cityRadius;
        
        // ATTENTION: placedBuildingTiles.Clear() a été supprimé pour permettre
        // l'accumulation des tuiles de toutes les villes.
        
        // Une liste pour stocker toutes les tuiles à dessiner (routes, trottoirs, bâtiments)
        var cityPositions = new List<Vector3Int>();
        var cityTiles = new List<TileBase>();

        // --- Phase 0: Calcul de la limite intérieure du ring road ---
        float ringRoadRadius = cityRadius; 
        float sqrRingRoadInnerLimit = (ringRoadRadius - 4f) * (ringRoadRadius - 4f); 

        // Dictionnaire pour stocker les positions de tous les bâtiments placés
        var buildingFootprints = new HashSet<Vector3Int>();

        // --- Phase 1: Placement du Ring Road Circulaire et des Bâtiments de la Grille (avec 1 tile de séparation) ---
        
        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                float sqrDist = (x - centerX) * (x - centerX) + (y - centerY) * (y - centerY);

                if (sqrDist < sqrCityRadius) 
                {
                    TileBase tileToPlace = null;
                    
                    // 1A. Placement du Ring Road
                    if (sqrDist >= sqrRingRoadInnerLimit)
                    {
                        float dist = Mathf.Sqrt(sqrDist);
                        float distFromEdge = cityRadius - dist;

                        if (distFromEdge < 1f || (distFromEdge >= 3f && distFromEdge < 4f))
                        {
                            tileToPlace = sidewalkTile;
                        }
                        else if (distFromEdge >= 1f && distFromEdge < 3f)
                        {
                            tileToPlace = roadTile;
                        }
                        
                        if (tileToPlace != null)
                        {
                            cityPositions.Add(pos);
                            cityTiles.Add(tileToPlace);
                            placedBuildingTiles.Add(pos); // AJOUTÉ à l'ensemble GLOBAL
                        }
                    }
                    
                    // 1B. Placement des Bâtiments de la Grille Intérieure
                    // placedBuildingTiles contient ici les tuiles du Ring Road et des bâtiments des autres villes
                    else if (!placedBuildingTiles.Contains(pos))
                    {
                        // --- LOGIQUE DE SÉLECTION DE BÂTIMENT (10% Église, 45% Small, 45% Tall) ---
                        Vector2Int buildingSize;
                        Tile buildingTileType;
                        float randomValue = Random.value;

                        if (randomValue < 0.1f) // 10% de chance
                        {
                            buildingSize = churchBuildingSize;
                            buildingTileType = churchTile;
                        }
                        else if (randomValue < 0.55f) // 45% de chance
                        {
                            buildingSize = smallBuildingSize;
                            buildingTileType = smallBuildingTile;
                        }
                        else // 45% de chance
                        {
                            buildingSize = tallBuildingSize;
                            buildingTileType = tallBuildingTile;
                        }
                        // --------------------------------------------------------------------------
                        
                        bool canPlace = true;
                        // Vérifier si la zone du bâtiment + 1 tuile de padding est disponible
                        for (int i = -1; i < buildingSize.x + 1; i++) 
                        {
                            for (int j = -1; j < buildingSize.y + 1; j++) 
                            {
                                Vector3Int checkPos = new Vector3Int(x + i, y + j, 0);

                                // Vérifier l'overlap avec un autre élément de la ville actuelle ou des autres villes
                                if (placedBuildingTiles.Contains(checkPos))
                                {
                                    canPlace = false;
                                    break;
                                }
                                float checkSqrDist = (checkPos.x - centerX) * (checkPos.x - centerX) + (checkPos.y - centerY) * (checkPos.y - centerY);
                                if (checkSqrDist >= sqrRingRoadInnerLimit)
                                {
                                    canPlace = false;
                                    break;
                                }
                            }
                            if (!canPlace) break;
                        }

                        if (canPlace)
                        {
                            // Placer le bâtiment et marquer l'empreinte
                            for (int i = 0; i < buildingSize.x; i++)
                            {
                                for (int j = 0; j < buildingSize.y; j++)
                                {
                                    Vector3Int buildingPos = new Vector3Int(x + i, y + j, 0);
                                    cityPositions.Add(buildingPos);
                                    cityTiles.Add(buildingTileType);
                                    placedBuildingTiles.Add(buildingPos); // AJOUTÉ à l'ensemble GLOBAL
                                    buildingFootprints.Add(buildingPos); 
                                }
                            }
                        }
                    }
                }
            }
        }
        
        // --- Phase 2: Placement des Trottoirs autour des Bâtiments ---
        foreach (var bldgPos in buildingFootprints)
        {
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue; 

                    Vector3Int sidewalkPos = new Vector3Int(bldgPos.x + dx, bldgPos.y + dy, 0);
                    
                    float sqrDistSidewalk = (sidewalkPos.x - centerX) * (sidewalkPos.x - centerX) + (sidewalkPos.y - centerY) * (sidewalkPos.y - centerY);
                    
                    if (sqrDistSidewalk < sqrRingRoadInnerLimit && !placedBuildingTiles.Contains(sidewalkPos))
                    {
                        cityPositions.Add(sidewalkPos);
                        cityTiles.Add(sidewalkTile);
                        placedBuildingTiles.Add(sidewalkPos); // AJOUTÉ à l'ensemble GLOBAL
                    }
                }
            }
        }

        // --- Phase 3: Placement des Routes dans les Cases Restantes (Inner Grid uniquement) ---
        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                float sqrDist = (x - centerX) * (x - centerX) + (y - centerY) * (y - centerY);

                if (sqrDist < sqrRingRoadInnerLimit && !placedBuildingTiles.Contains(pos))
                {
                    cityPositions.Add(pos);
                    cityTiles.Add(sidewalkTile);
                    placedBuildingTiles.Add(pos); 
                }
            }
        }

        // --- Finalisation du dessin des tuiles ---
        tilemap.SetTiles(cityPositions.ToArray(), cityTiles.ToArray());

        // Appliquer les couleurs
        for (int i = 0; i < cityPositions.Count; i++)
        {
            tilemap.SetTileFlags(cityPositions[i], TileFlags.None);
            tilemap.SetColor(cityPositions[i], Color.white);
        }
    }
}