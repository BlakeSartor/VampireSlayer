using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject teamSelectPanel;
    public GameObject crosshair;
    public GameObject pauseMenu;
    public static bool gameIsPaused = false;
    public static bool teamSelectIsOpen = false;

    public void Start()
    {
        gameIsPaused = false;
        teamSelectIsOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (teamSelectIsOpen)
            {
                Resume();
            }
            else
            {
                OpenTeamSelect();
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        crosshair.SetActive(true);
        pauseMenu.SetActive(false);
        teamSelectPanel.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        teamSelectIsOpen = false;
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        crosshair.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void QuitGame()
    {
        BoltNetwork.Shutdown();
        SceneManager.LoadSceneAsync("Menu");
    }

    public bool getIsPaused()
    {
        return gameIsPaused;
    }

    public void OpenTeamSelect()
    {
        Cursor.lockState = CursorLockMode.None;
        crosshair.SetActive(false);
        teamSelectPanel.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        teamSelectIsOpen = true;
    }
}
