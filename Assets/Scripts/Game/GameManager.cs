//2021 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject PlayerPrefab;
    public GameObject TablePrefab;
    public GameObject PickupTablePrefab;

    public GameObject room;

    public DisplayManager displaymanager;

    Player currentPlayer;
    //    List<Table> table;
    public bool isLevelComplete;
    public int iLevel;

    public const int MAX_LEVELS = 3;
    public float fTimer;
    public bool isGameOver;

    
    void Start() {
        iLevel = 0;
        setupGame();
        
    }

    void Update() {

        if (fTimer > 0f && !isLevelComplete && !isGameOver) {
            fTimer -= Time.deltaTime;
            if (fTimer <= 0f) {
                timeExpired();
            }
        }
        
    }

    private void setupGame() {
        isLevelComplete = false;

        currentPlayer = Instantiate(PlayerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Player>();
        currentPlayer.gamemanager = this;
        currentPlayer.transform.SetParent(room.transform);

        int iTotalTables = 6 + (iLevel * 3);


        int i;
        for (i = 0; i < iTotalTables; i++) {
            Vector3 pos = new Vector3(-5f + ((i / 3) * 10f), 0f, -10f + (i % 3) * 10f);
            Table t = Instantiate(TablePrefab, pos, Quaternion.identity).GetComponent<Table>();
            t.transform.SetParent(room.transform);

        }

        PickupTable pickuptable = Instantiate(PickupTablePrefab, new Vector3(-12f, 0f, 0f), Quaternion.identity).GetComponent<PickupTable>();
        pickuptable.transform.SetParent(room.transform);

        fTimer = 60f;
        isGameOver = false;

    }

    private void timeExpired() {
        isGameOver = true;
        displaymanager.displayGameOver();
    }

    public void checkLevelComplete() {
        Table[] tables = room.GetComponentsInChildren<Table>();
        bool allTablesComplete = true;
        foreach (Table table in tables) {
            if (!table.getAllServed()) {
                allTablesComplete = false;

            }
        }
        isLevelComplete = allTablesComplete;

        if (isLevelComplete) {
            if (iLevel + 1 >= MAX_LEVELS) {
                displaymanager.displayWinner();
            } else {
                displaymanager.displayLevelComplete();
            }
        }

    }

    public void doNextLevel() {
        iLevel++;
        int i;
        for (i = room.transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(room.transform.GetChild(i).gameObject);
        }

        displaymanager.hideAllPanels();


        setupGame();
    }

    public void doReturnToTitle() {
        SceneManager.LoadScene("title");
    }

}