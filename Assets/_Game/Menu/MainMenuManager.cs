using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Loads the game scene (replace "GameScene" with your scene name)
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        GameManager.Reset_();
    }

    // Loads the options menu scene (replace "OptionsScene" with your scene name)
    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    // Quit the application
    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
