using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject RestartMenuUI;
    [SerializeField] string SceneName = "Scene";

    private void Awake()
    {
        GameIsPaused = false;
        PauseMenuUI.SetActive(false);
        RestartMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (ObstacleCollision.endGame)
        {
            Invoke("RestartMenu", 1f);
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        if (!ObstacleCollision.endGame)
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }

    public void RestartGame()
    {
        Resume();
        SceneManager.LoadScene(SceneName);
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void RestartMenu()
    {
        RestartMenuUI.SetActive(true);
    }
}
