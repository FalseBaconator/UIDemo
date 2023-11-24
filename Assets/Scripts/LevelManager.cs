using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public int wins;

    public Button[] levelButtons;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        wins = PlayerPrefs.GetInt("wins", 0);
    }

    public void Win(int level)
    {
        if (level > wins) wins++;
        PlayerPrefs.SetInt("wins", wins);
    }

    public void Refresh()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i <= wins) levelButtons[i].interactable = true;
            else levelButtons[i].interactable = false;
        }
    }


    public void DeleteSave()
    {
        PlayerPrefs.SetInt("wins", 0);
        gameManager.QuitToMenu();
    }


}
