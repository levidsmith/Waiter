//2021 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : Enemy {

    Player player = null;
    float fSpeed = 3f;
    float fDistanceToChase = 8f;

    void Start() {

    }

    void Update() {


    }

    private void FixedUpdate() {
        if (player == null) {
            player = GameObject.FindObjectOfType<Player>();
        }

        if (Vector3.Distance(transform.position, player.transform.position) < fDistanceToChase) {

            Vector3 movePos = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * fSpeed);

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.MovePosition(movePos);
        }
    }
}