//2021 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Vector3 vel;
    float fSpeed = 5f;
    public const int MAX_GLASSES = 10;

    public GameObject GlassPrefab;

    public GameManager gamemanager;

    void Start() {
        vel = new Vector3();
        
    }

    void Update() {

        if (!gamemanager.isGameOver && !gamemanager.isLevelComplete) {
            handleInput();
            CharacterController controller = GetComponent<CharacterController>();
            controller.Move(vel * Time.deltaTime * fSpeed);
        }
        

        

        
    }

    private void handleInput() {
        vel = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

    }

    private void refillGlasses() {
        Debug.Log("refillGlasses");
        Glass[] glasses = GetComponentsInChildren<Glass>();


        int i;
        for (i = glasses.Length; i < MAX_GLASSES; i++) {
            Glass glass = Instantiate(GlassPrefab, Vector3.zero, Quaternion.identity).GetComponent<Glass>();
            glass.transform.SetParent(this.transform);
        }

        glasses = GetComponentsInChildren<Glass>();
        i = 0;
        foreach (Glass glass in glasses) {
            float fGlassHeight = 0.3f;
            glass.transform.localPosition = new Vector3(1f, 1f + (fGlassHeight * MAX_GLASSES) - (fGlassHeight * i), 0f);
            i++;
        }
    }



    private void OnCollisionEnter(Collision other) {
        Debug.Log("trigger");
    }

    
    private void OnControllerColliderHit(ControllerColliderHit hit) {
    //void OnCollisionEnter(Collision collision) {

        //Debug.Log("Collied: " + hit.collider.name);
        Debug.Log("CollisionEnter");


        Table hitTable = hit.collider.transform.GetComponentInParent<Table>();
        //Table hitTable = collision.transform.GetComponentInParent<Table>();
        if (hitTable != null) {
            Debug.Log("hit table");

            Glass[] glasses = GetComponentsInChildren<Glass>();

            foreach (Glass glass in glasses) {
                if (hitTable.getGlassesRemainingCount() > 0) {
                    hitTable.serveGlass(glass);
                    gamemanager.checkLevelComplete();
                }
            }


            
        }

        PickupTable hitPickupTable = hit.collider.GetComponentInParent<PickupTable>();
        if (hitPickupTable != null) {
            Debug.Log("hit pickup table");
            refillGlasses();
        }

    }
}
