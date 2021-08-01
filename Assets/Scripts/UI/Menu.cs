using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private CameraRotation CameraRotation;
    private bool _startGame;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_startGame)
                StopGame();
            else
                StartGame();
        }
    }

    void StopGame()
    {
        Time.timeScale = 0;
        settingPanel.SetActive(false);
        CameraRotation.enabled = false;
        panel.SetActive(true);
        crossHair.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _startGame = false;
    }
    void StartGame()
    {
        CameraRotation.enabled = true;
        Time.timeScale = 1;
        panel.SetActive(false);
        crossHair.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _startGame = true;
    }
}
