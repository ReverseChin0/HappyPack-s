using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MissionEndPoint : MonoBehaviour
{
    [HideInInspector]public SCR_MissionManager missionMana = default;
    SCR_ShaderColor mishader = default;

    private void Awake() 
    {
        mishader = GetComponent<SCR_ShaderColor>();   
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) 
        {
            missionMana.FinMision();
            mishader.Hide(true);
        }
    }
}
