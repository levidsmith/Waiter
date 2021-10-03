//2021 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    void Start() {
        
    }

    void Update() {
        
    }

    private void OnCollisionEnter(Collision collision) {
        Player player = collision.collider.GetComponent<Player>();
        if (player != null) {
            Debug.Log("Enemy hit player");
            player.dropGlasses();
        }
    }
}