using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene("Game");
        GameManager.Reset_();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
