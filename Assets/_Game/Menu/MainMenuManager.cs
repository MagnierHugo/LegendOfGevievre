using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Loads the game scene (replace "GameScene" with your scene name)
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Loads the options menu scene (replace "OptionsScene" with your scene name)
    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    // Quits the application
    public void QuitGame()
    {
        Debug.Log("Quit Game!"); // Just for editor testing
        Application.Quit();
    }
}
