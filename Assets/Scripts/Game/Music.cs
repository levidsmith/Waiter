//2021 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public AudioSource musicGame;
    public AudioSource musicVictory;


    public void stopAll() {
        musicGame.Stop();
        musicVictory.Stop();
    }
}