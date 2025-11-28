using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuManager : MonoBehaviour
{
    // Back Button
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
