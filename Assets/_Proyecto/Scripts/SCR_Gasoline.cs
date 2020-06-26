using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Gasoline : MonoBehaviour
{
    [SerializeField] SCR_CarMovement carGas = default;
    [SerializeField] SCR_PlayerProgress dinero = default;
    bool canAdd = true;

    private void OnTriggerStay(Collider other) 
    {
        if (other.CompareTag("Player")) {
            if (canAdd) {
                bool pay = false;
                if (carGas != null) pay = carGas.AddGas(0.1f);
                if (dinero != null) {
                    if(pay)
                    dinero.AddMoney(-5); //cobra 5 dollars
                }
                canAdd = false;
                StartCoroutine(resetAdd(0.15f));
            }
            
        }
    }

    IEnumerator resetAdd(float _t) {
        yield return new WaitForSeconds(_t);
        canAdd = true;
    }
}
