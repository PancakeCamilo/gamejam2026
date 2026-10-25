using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Loads the main gameplay scene
        SceneManager.LoadScene("GameScreen");
    }
}