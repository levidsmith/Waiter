//2021 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManager : MonoBehaviour {
    public GameObject panelLevelComplete;
    public GameObject panelGameOver;

    void Start() {
        
    }

    void Update() {
        
    }

    public void displayLevelComplete() {
        panelLevelComplete.SetActive(true);
    }

    public void displayGameOver() {
        panelGameOver.SetActive(true);
    }

    public void hideAllPanels() {
        panelLevelComplete.SetActive(false);
        panelGameOver.SetActive(false);
    }
}