using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // singleton => only one instance can be created
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;


    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER!");
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // stop time in game

        // for debugging
        //Debug.Log($"GAME OVER called at {Time.realtimeSinceStartup}");
        //gameOverPanel.SetActive(true);
        //Debug.Log($"Panel activated at {Time.realtimeSinceStartup}");
        //Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // returns time flow to normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinGame()
    {
        Debug.Log("YOU WIN!");
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
