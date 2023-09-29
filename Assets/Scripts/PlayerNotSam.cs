using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNotSam : MonoBehaviour
{

    private GameManager gameManager;
    public int HP;
    public int MaxHP;
    private TextMeshProUGUI HPText;
    private Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        HPText = GameObject.FindGameObjectWithTag("HPText").GetComponent<TextMeshProUGUI>();
        HPText.text = "HP: " + HP + "/" + MaxHP;
        spawnPoint = transform.position;
    }

    private void Update()
    {
        Debug.Log(spawnPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Victory":
                gameManager.gameState = GameManager.GameState.Win;
                break;
            case "Danger":
                HP--;
                HPText.text = "HP: " + HP + "/" + MaxHP;
                if (HP <= 0)
                {
                    gameManager.gameState = GameManager.GameState.Lose;
                }
                else
                {
                    gameObject.GetComponent<CharacterController>().enabled = false;
                    transform.position = spawnPoint;
                    gameObject.GetComponent<CharacterController>().enabled = true;
                }
                break;
        }
    }
}
