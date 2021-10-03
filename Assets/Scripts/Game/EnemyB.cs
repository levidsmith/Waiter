//2021 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB : Enemy {

    Vector3 vel;
    float fCountdown;
    float fMaxCountdown;
    float fWaitCountdown;
    float fMaxWaitCountdown;
    float fSpeed = 2f;

    void Start() {
        vel = new Vector3(-2f, 0f, 0f);
        fMaxCountdown = 1f;
        fCountdown = fMaxCountdown;
        fMaxWaitCountdown = 0.5f;

    }

    void Update() {
        //transform.Translate(vel * Time.deltaTime, Space.World);

        if (fWaitCountdown > 0f) {
            fWaitCountdown -= Time.deltaTime;
        } else {
            fCountdown -= Time.deltaTime;

            if (fCountdown <= 0f) {
                int iRand = Random.Range(0, 5);
                switch (iRand) {
                    case 0:
                        vel = new Vector3(-1f * fSpeed, 0f, 0f * fSpeed);
                        break;
                    case 1:
                        vel = new Vector3(1f * fSpeed, 0f, 0f * fSpeed);
                        break;
                    case 2:
                        vel = new Vector3(0f * fSpeed, 0f, -1f * fSpeed);
                        break;
                    case 3:
                        vel = new Vector3(0f * fSpeed, 0f, 1f * fSpeed);
                        break;

                }

                fWaitCountdown = fMaxWaitCountdown;
                fCountdown += fMaxCountdown;
            }
        }

    }

    private void FixedUpdate() {
        if (fWaitCountdown > 0f) {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.MovePosition(transform.position + (vel * Time.deltaTime));
        }
    }
}