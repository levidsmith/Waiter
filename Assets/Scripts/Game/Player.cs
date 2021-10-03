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
    public Animator animator;

    void Start() {
        vel = new Vector3();
        
    }

    void Update() {

        if (!gamemanager.isGameOver && !gamemanager.isLevelComplete) {
            handleInput();
            CharacterController controller = GetComponent<CharacterController>();


            controller.Move(vel * Time.deltaTime * fSpeed);


            Vector3 vectFall = new Vector3(0f, -9.81f, 0f);
            controller.Move(vectFall * Time.deltaTime);

            animator.SetFloat("velocity", vel.magnitude);
            
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
            float fGlassWidth = 0.2f;
            //glass.transform.localPosition = new Vector3(1f, 1f + (fGlassHeight * MAX_GLASSES) - (fGlassHeight * i), 0f);
            Vector3 pos;
            switch (i) {
                case 0:
                    pos = new Vector3(3f * fGlassWidth, 3f * fGlassHeight, 0f);
                    break;
                case 1:
                    pos = new Vector3(2f * fGlassWidth, 2f * fGlassHeight, 0f);
                    break;
                case 2:
                    pos = new Vector3(4f * fGlassWidth, 2f * fGlassHeight, 0f);
                    break;
                case 3:
                    pos = new Vector3(1f * fGlassWidth, 1f * fGlassHeight, 0f);
                    break;
                case 4:
                    pos = new Vector3(3f * fGlassWidth, 1f * fGlassHeight, 0f);
                    break;
                case 5:
                    pos = new Vector3(5f * fGlassWidth, 1f * fGlassHeight, 0f);
                    break;
                case 6:
                    pos = new Vector3(0f * fGlassWidth, 0f * fGlassHeight, 0f);
                    break;
                case 7:
                    pos = new Vector3(2f * fGlassWidth, 0f * fGlassHeight, 0f);
                    break;
                case 8:
                    pos = new Vector3(4f * fGlassWidth, 0f * fGlassHeight, 0f);
                    break;
                case 9:
                    pos = new Vector3(6f * fGlassWidth, 0f * fGlassHeight, 0f);
                    break;

                default:
                    pos = Vector3.zero;
                    break;
            }
            i++;

            glass.transform.localPosition = new Vector3(0.7f, 1.2f, 0f) + pos;

        }
    }

    public void dropGlasses() {
        Debug.Log("dropping glasses");

        Glass[] glasses = GetComponentsInChildren<Glass>();
        GameObject gobjRoom = GameObject.Find("Room");

        foreach (Glass glass in glasses) {
            glass.transform.SetParent(gobjRoom.transform);
            Rigidbody rigidbody = glass.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.AddForce(new Vector3(Random.Range(-5f, 5f), 20f, Random.Range(-5f, 5f)));
            rigidbody.AddTorque(new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f)));
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

        Enemy hitEnemy = hit.collider.GetComponent<Enemy>();
        if (hitEnemy != null) {
            Debug.Log("hit enemy");
            dropGlasses();
        }

    }
}
