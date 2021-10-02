//2021 Levi D. Smith
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {

    public GameObject CustomerPrefab;

    void Start() {
        setupTable();    
    }

    void Update() {
        
    }

    private void setupTable() {
        int iNumCustomers;

        iNumCustomers = Random.Range(2, 7);

        int i;
        for (i = 0; i < iNumCustomers; i++) {

            Customer customer = Instantiate(CustomerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Customer>();
            customer.transform.SetParent(this.transform);
            //customer.transform.localPosition = new Vector3(i * 1f, 0f, 0f);
            float fAngle = ((float)i / (float)iNumCustomers) * Mathf.PI * 2f;
            float fRadius = 2.5f;
            customer.transform.localPosition = new Vector3( fRadius * Mathf.Cos(fAngle), 0f, fRadius * Mathf.Sin(fAngle));
            customer.transform.LookAt(this.transform);



            //customer.transform.LookAt(this.transform.position);
        }
    }

    public int getGlassesRemainingCount() {
        int iGlassesRemaining = 0;

        Customer[] customers = GetComponentsInChildren<Customer>();
        foreach (Customer customer in customers) {
            if (customer.glass == null) {
                iGlassesRemaining++;
            }
        }

        return iGlassesRemaining;
    }

    public void serveGlass(Glass inGlass) {
        bool isGlassServed = false;

        Customer[] customers = GetComponentsInChildren<Customer>();
        foreach (Customer customer in customers) {
            if (customer.glass == null && !isGlassServed) {
                customer.glass = inGlass;
                inGlass.transform.SetParent(customer.transform);
                inGlass.transform.localPosition = new Vector3(0f, 1f, 1f);
                isGlassServed = true;
            }
        }

    }


    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Table: OnCollisionEnter");
    }

    public bool getAllServed() {
        bool isAllServed = true;

        Customer[] customers = GetComponentsInChildren<Customer>();
        foreach (Customer customer in customers) {
            if (customer.glass == null) {
                isAllServed = false;
            }
        }


            return isAllServed;
    }
}