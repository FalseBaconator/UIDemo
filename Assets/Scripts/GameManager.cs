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

    public AudioManager audioManager;

    public GameObject fireParent;
    public Image fireImg;
    public Image fireTimeIndicator;

    public int currentLevel;

    public enum GameState { MainMenu, Options, LevelSelect, GamePlay, Pause, Win, Lose, Sure }

    private GameState _gameState;
    public GameState gameState
    {
        get => _gameState;
        set
        {
            if (_gameState == GameState.Pause)
            {
                audioManager.UnpauseAllAudio();
            }
            prevState = _gameState;
            if(_gameState != GameState.Pause && _gameState != GameState.Options && _gameState != GameState.Sure) prevNotPauseState = _gameState;
            switch (value)
            {
                case GameState.MainMenu:
                    fireParent.SetActive(false);
                    uiManager.OpenMainMenu();
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.Options:
                    uiManager.OpenOptions();
                    Cursor.visible = true;
                    Cursor.lockState= CursorLockMode.None;
                    break;
                case GameState.LevelSelect:
                    levelManager.Refresh();
                    fireParent.SetActive(false);
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
                    try
                    {
                        firstPersonSam.Pause();
                    }catch { }
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    audioManager.PauseAllAudio();
                    break;
                case GameState.Win:
                    Time.timeScale = 0;
                    levelManager.Win(currentLevel);
                    fireParent.SetActive(false);
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
                    Time.timeScale = 0;
                    fireParent.SetActive(false);
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
                case GameState.Sure:
                    fireParent.SetActive(false);
                    uiManager.OpenSureScreen();
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
            }
            _gameState = value;
        }
    }
    private GameState prevState = GameState.MainMenu;
    private GameState prevNotPauseState = GameState.MainMenu;

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
        currentLevel = scene;
        gameState = GameState.GamePlay;
        SceneManager.LoadScene(scene);
    }

    public void GoToPrevNotPause()
    {
        gameState = prevNotPauseState;
    }

    public void OpenOptions()
    {
        gameState = GameState.Options;
    }

    public void GoToSure()
    {
        gameState = GameState.Sure;
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
