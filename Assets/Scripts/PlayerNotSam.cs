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
    public float stamina;
    public float MaxStamina;
    public bool canSprint;
    public bool isSprinting;
    //private TextMeshProUGUI HPText;
    public Slider HPSlider;
    public Slider staminaSlider;
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

    FirstPersonController_Sam playerSam;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        fireImg = gameManager.fireImg;
        //HPText = GameObject.FindGameObjectWithTag("HPText").GetComponent<TextMeshProUGUI>();
        //HPText.text = "HP: " + HP + "/" + MaxHP;
        HPSlider = GameObject.FindGameObjectWithTag("HPSlider").GetComponent<Slider>();
        HPSlider.maxValue = MaxHP;
        staminaSlider = GameObject.FindGameObjectWithTag("StaminaSlider").GetComponent<Slider>();
        staminaSlider.maxValue = MaxStamina;
        playerSam = GetComponent<FirstPersonController_Sam>();
        canSprint = true;
        spawnPoint = transform.position;
    }

    private void Update()
    {
        jumpText.text = "JUMPS: " + jumps;
        if(HPSlider.value != HP)
        {
            HPSlider.value = HP;
        }

        if (Input.GetKey(KeyCode.LeftShift) && isSprinting == false)
        {
            if (canSprint) isSprinting = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        if (isSprinting)
        {
            stamina -= Time.deltaTime;
            if(stamina <= 0)
            {
                stamina = 0;
                canSprint = false;
                //playerSam.canZoom = false;
                playerSam.StopSprint();
                isSprinting = false;
            }
        }
        if (!isSprinting)
        {
            if(stamina < MaxStamina)
            {
                stamina += Time.deltaTime;
            }else if (!canSprint)
            {
                canSprint = true;
                //playerSam.canZoom = true;
            }
            if(stamina > MaxStamina)
            {
                stamina = MaxStamina;
            }
        }

        staminaSlider.value = stamina;

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
        //HPText.text = "HP: " + HP + "/" + MaxHP;
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
