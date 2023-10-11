using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public int gamePlayScene;
    public int mainMenuScene;

    public GameObject uiObject;
    public GameObject levelObject;

    private UIManager uiManager;
    private LevelManager levelManager;
    public FirstPersonController_Sam firstPersonSam;

    public Image fireImg;

    public enum GameState { MainMenu, Options, LevelSelect, GamePlay, Pause, Win, Lose }

    private GameState _gameState;
    public GameState gameState
    {
        get => _gameState;
        set
        {
            prevState = _gameState;
            switch (value)
            {
                case GameState.MainMenu:
                    fireImg.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    uiManager.OpenMainMenu();
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.Options:
                    Time.timeScale = 0;
                    uiManager.OpenOptions();
                    Cursor.visible = true;
                    Cursor.lockState= CursorLockMode.None;
                    break;
                case GameState.LevelSelect:
                    fireImg.gameObject.SetActive(false);
                    Time.timeScale = 1;
                    uiManager.OpenLevelSelect();
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.GamePlay:
                    Time.timeScale = 1;
                    uiManager.OpenGameScreen();
                    //player = FindFirstObjectByType<FirstPersonController_Sam>();
                    try
                    {
                        firstPersonSam.UnPause();
                    }
                    catch { }
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case GameState.Pause:
                    Time.timeScale = 0;
                    uiManager.OpenPauseScreen();
                    //player = FindFirstObjectByType<FirstPersonController_Sam>();
                    try
                    {
                        firstPersonSam.Pause();
                    }catch { }
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.Win:
                    fireImg.gameObject.SetActive(false);
                    Time.timeScale = 0;
                    uiManager.OpenWinScreen();
                    //player = FindFirstObjectByType<FirstPersonController_Sam>();
                    try
                    {
                        firstPersonSam.Pause();
                    }
                    catch { }
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.Lose:
                    fireImg.gameObject.SetActive(false);
                    Time.timeScale = 0;
                    uiManager.OpenLoseScreen();
                    //player = FindFirstObjectByType<FirstPersonController_Sam>();
                    try
                    {
                        firstPersonSam.Pause();
                    }
                    catch { }
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
            }
            _gameState = value;
        }
    }
    private GameState prevState = GameState.MainMenu;

    public void SetPlayer(FirstPersonController_Sam character)
    {
        firstPersonSam = character;
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = uiObject.GetComponent<UIManager>();
        levelManager = levelObject.GetComponent<LevelManager>();
        gameState = GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                break;
            case GameState.GamePlay:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.Pause;
                }
                break;
            case GameState.Pause:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.GamePlay;
                }
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
        }
    }

    public void OpenLevelScreen()
    {
        gameState = GameState.LevelSelect;
    }

    public void GoToLevel(int scene)
    {
        gameState = GameState.GamePlay;
        SceneManager.LoadScene(scene);
    }

    public void GoToPrevious()
    {
        gameState = prevState;
    }

    public void OpenOptions()
    {
        gameState = GameState.Options;
    }

    public void QuitToMenu()
    {
        gameState = GameState.MainMenu;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
