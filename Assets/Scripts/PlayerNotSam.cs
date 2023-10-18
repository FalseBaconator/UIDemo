using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNotSam : MonoBehaviour
{

    private GameManager gameManager;
    public int HP;
    public int MaxHP;
    private TextMeshProUGUI HPText;
    private Vector3 spawnPoint;
    public float fireDelay;
    public float animDelay;
    private float currentDelay;
    private float currentAnimDelay;
    private Image fireImg;
    private int fireIndex = 1;
    public Sprite fire1;
    public Sprite fire2;

    public TextMeshProUGUI jumpText;
    int jumps;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        fireImg = gameManager.fireImg;
        HPText = GameObject.FindGameObjectWithTag("HPText").GetComponent<TextMeshProUGUI>();
        HPText.text = "HP: " + HP + "/" + MaxHP;
        spawnPoint = transform.position;
    }

    private void Update()
    {
        jumpText.text = "JUMPS: " + jumps;
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Victory":
                break;
            case "Danger":
                currentDelay -= Time.deltaTime;
                currentAnimDelay -= Time.deltaTime;
                if (currentDelay <= 0)
                {
                    TakeDmg();
                    currentDelay = fireDelay;
                }
                if(currentAnimDelay <= 0)
                {
                    currentAnimDelay = animDelay;
                    switch (fireIndex)
                    {
                        case 1:
                            fireIndex = 2;
                            fireImg.sprite = fire2;
                            break;
                        case 2:
                            fireIndex = 1;
                            fireImg.sprite = fire1;
                            break;
                    }
                }
                break;
        }
    }

    void TakeDmg()
    {
        HP--;
        HPText.text = "HP: " + HP + "/" + MaxHP;
        if (HP <= 0)
        {
            gameManager.gameState = GameManager.GameState.Lose;
        }
    }

    public void Jump()
    {
        jumps++;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Victory":
                gameManager.gameState = GameManager.GameState.Win;
                break;
            case "Danger":
                TakeDmg();
                currentDelay = fireDelay;
                currentAnimDelay = animDelay;
                fireImg.gameObject.SetActive(true);
                fireIndex = 1;
                fireImg.sprite = fire1;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch(other.tag)
        {
            case "Victory":
                break;
            case "Danger":
                fireImg.gameObject.SetActive(false);
                break;
        }
    }
}
