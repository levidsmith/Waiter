//2021 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour {
    public GameObject panelLevelComplete;
    public GameObject panelGameOver;
    public GameObject panelWinner;
    public Text TextLevel;
    public Text TextTimer;
    public GameManager gamemanager;


    void Start() {
        
    }

    void Update() {
        TextLevel.text = string.Format("Level     {0}", (gamemanager.iLevel + 1));
        TextTimer.text = string.Format("{0:0}", gamemanager.fTimer);

    }

    public void displayLevelComplete() {
        panelLevelComplete.SetActive(true);
    }

    public void displayGameOver() {
        panelGameOver.SetActive(true);
    }

    public void displayWinner() {
        panelWinner.SetActive(true);
    }


    public void hideAllPanels() {
        panelLevelComplete.SetActive(false);
        panelGameOver.SetActive(false);
    }
}