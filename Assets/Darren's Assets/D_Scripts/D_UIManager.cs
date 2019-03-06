﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class D_UIManager : MonoBehaviour
{
    public TextMeshProUGUI Reputation;
    public TextMeshProUGUI Lives;
    public TextMeshProUGUI Gun;
    public TextMeshProUGUI Invincibility;
    public GameObject PauseCanvas;
    public bool paused;
    public Shoot shootScript;

    // Start is called before the first frame update
    void Start()
    {
        D_SimpleLevelManager.current.OnTakeDamage.AddListener(UpdateLives);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOrResume();
        }
        UpdateGun();
        UpdateDodge();
    }

    public void UpdateLives()
    {
        Lives.text = string.Format("Lives: {0}", D_SimpleLevelManager.current.playerLives);
    }
    public void UpdateScore()
    {
        Reputation.text = string.Format("Reputation: {0}", D_SimpleLevelManager.current.Score);
    }

    public void UpdateGun()
    {
        Gun.text = Shoot.current.gunAmmoUIStr;
    }

    public void UpdateDodge()
    {
        if (!D_PlayerTestScript.current.dodging)
            Invincibility.text = "Activate Dodge";
        else
            Invincibility.text = "";

    }

    public void PauseOrResume()
    {
        if (PauseCanvas == null && shootScript==null)
            return;
        if (!paused)
        {
            paused = true;
            PauseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            shootScript.enabled = false; //MAKE THIS A UNITY EVENT THIS IS TEMPORARY FIX
        }
        else
        {
            paused = false;
            PauseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
            shootScript.enabled = true;
        }
    }
}
