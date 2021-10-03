//2021 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Enemy {

    Vector3 vel;
    float fCountdown;
    float fMaxCountdown;

    void Start() {
        vel = new Vector3(-2f, 0f, 0f);
        fMaxCountdown = 4f;
        fCountdown = fMaxCountdown / 2f;
        
    }

    void Update() {
        //transform.Translate(vel * Time.deltaTime, Space.World);
        
        fCountdown -= Time.deltaTime;
        if (fCountdown <= 0f) {
            vel = new Vector3(vel.x * -1f, 0f, 0f);
            fCountdown += fMaxCountdown;
        }
        
    }

    private void FixedUpdate() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.MovePosition(transform.position + (vel * Time.deltaTime));
    }

}