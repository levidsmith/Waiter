//2021 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject PlayerPrefab;
    public GameObject TablePrefab;
    public GameObject PickupTablePrefab;

    public GameObject room;

    public DisplayManager displaymanager;

    Player currentPlayer;
    //    List<Table> table;
    bool isLevelComplete;
    
    void Start() {
        setupGame();
        
    }

    void Update() {
        
    }

    private void setupGame() {
        isLevelComplete = false;

        currentPlayer = Instantiate(PlayerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Player>();
        currentPlayer.gamemanager = this;
        currentPlayer.transform.SetParent(room.transform);

        int i;
        for (i = 0; i < 6; i++) {
            Vector3 pos = new Vector3(-5f + ((i / 3) * 10f), 0f, -10f + (i % 3) * 10f);
            Table t = Instantiate(TablePrefab, pos, Quaternion.identity).GetComponent<Table>();
            t.transform.SetParent(room.transform);

        }

        PickupTable pickuptable = Instantiate(PickupTablePrefab, new Vector3(-12f, 0f, 0f), Quaternion.identity).GetComponent<PickupTable>();
        pickuptable.transform.SetParent(room.transform);

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
            displaymanager.displayLevelComplete();
        }

    }

    public void doNextLevel() {
        int i;
        for (i = room.transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(room.transform.GetChild(i).gameObject);
        }

        displaymanager.hideAllPanels();


        setupGame();
    }

}