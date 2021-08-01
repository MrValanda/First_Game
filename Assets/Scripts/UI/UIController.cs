using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ContinueGame()
    {
        int lvl =PlayerPrefs.GetInt("LastLvl",1);
        SceneManager.LoadScene(lvl);
    }

    public void OpenMenu()
    {
    Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();

    }

}
