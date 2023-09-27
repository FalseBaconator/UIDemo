using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNotSam : MonoBehaviour
{

    private GameManager gameManager;
    private Vector3 spawn;
    public int HP;
    public int MaxHP;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Victory":
                gameManager.gameState = GameManager.GameState.Win;
                break;
            case "Danger":
                gameManager.gameState = GameManager.GameState.Lose;
                break;
        }
    }
}
