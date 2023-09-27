using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int gamePlayScene;
    public int mainMenuScene;

    public GameObject uiObject;
    public GameObject levelObject;

    private UIManager uiManager;
    private LevelManager levelManager;
    public FirstPersonController_Sam player;
    public enum GameState { MainMenu, GamePlay, Pause, Win, Lose }

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
                    Time.timeScale = 1;
                    uiManager.OpenMainMenu();
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.GamePlay:
                    Time.timeScale = 1;
                    uiManager.OpenGameScreen();
                    //player = FindFirstObjectByType<FirstPersonController_Sam>();
                    try
                    {
                        player.UnPause();
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
                        player.Pause();
                    }catch { }
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.Win:
                    Time.timeScale = 0;
                    uiManager.OpenWinScreen();
                    //player = FindFirstObjectByType<FirstPersonController_Sam>();
                    try
                    {
                        player.Pause();
                    }
                    catch { }
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case GameState.Lose:
                    Time.timeScale = 0;
                    uiManager.OpenLoseScreen();
                    //player = FindFirstObjectByType<FirstPersonController_Sam>();
                    try
                    {
                        player.Pause();
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
        player = character;
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
                if (Input.GetKeyDown(KeyCode.W))
                {
                    gameState = GameState.Win;
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    gameState = GameState.Lose;
                }
                break;
            case GameState.Pause:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.GamePlay;
                }
                break;
            case GameState.Win:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.GamePlay;
                }
                break;
            case GameState.Lose:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    gameState = GameState.GamePlay;
                }
                break;
        }
    }

    public void OpenGame()
    {
        gameState = GameState.GamePlay;
        SceneManager.LoadScene(gamePlayScene);
    }

    public void QuitToMenu()
    {
        gameState = GameState.MainMenu;
        SceneManager.LoadScene(mainMenuScene);
    }

}
