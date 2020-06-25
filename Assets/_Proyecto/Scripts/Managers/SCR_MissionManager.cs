﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SCR_MissionManager : MonoBehaviour, INotificable
{
    [SerializeField] Transform[] puntosInicio = default, puntosEntrega = default;
    [SerializeField] GameObject BeginPref = default, EndPref = default;
    [SerializeField] GameObject MissionPopUp = default;
    SCR_MissionPointData[] missionPoints = default;
    SCR_ShaderColor finMision = default;
    SCR_MissionData currentMission = default;
    SCR_MissionEndPoint missionEndpoint = default;
    TextMeshProUGUI tituloTMP = default, descripTMP = default;
    SCR_CarMovement player;
    bool[] ocupados;
    public int maxMisiones = 3;
    int current = 0;

    private void Awake() {
        player = FindObjectOfType<SCR_CarMovement>();
        tituloTMP = MissionPopUp.transform.Find("Titulo").GetComponent<TextMeshProUGUI>();
        descripTMP = MissionPopUp.transform.Find("Descripcion").GetComponent<TextMeshProUGUI>();
    }

    private void Start() 
    {
        int length = puntosInicio.Length;
        ocupados = new bool[length];
        missionPoints = new SCR_MissionPointData[length];
        for (int o = 0; o < ocupados.Length; o++) 
            ocupados[o] = false;

        GameObject go, endpoint;
        endpoint = Instantiate(EndPref, Vector3.zero, Quaternion.identity);
        missionEndpoint = endpoint.GetComponent<SCR_MissionEndPoint>();
        missionEndpoint.missionMana = this;
        finMision = endpoint.GetComponent<SCR_ShaderColor>();
        for (int i = 0; i < maxMisiones; i++) {
            int selector = Random.Range(0, length);

            while (ocupados[selector] == true)
                selector = Random.Range(0, length);

            ocupados[selector] = true;
            go = Instantiate(BeginPref, puntosInicio[selector].position, Quaternion.identity, puntosInicio[selector].parent);
            missionPoints[i] = go.GetComponent<SCR_MissionPointData>();
            missionPoints[i].miNotificador = this;
            missionPoints[i].GenerarMision(endpoint);
        }
        endpoint.SetActive(false);
    }

    public void Notificar(GameObject _go) 
    {   int i = 0;
        SCR_MissionPointData gomision = _go.GetComponent<SCR_MissionPointData>();

        foreach (SCR_MissionPointData mp in missionPoints) 
        {
            if (mp == gomision) 
            {
                current = i;
            }
            i++;
        }
        currentMission =  missionPoints[current].getMission();
        string descrip = getTipoMision(currentMission.tipodeMision) + " en " + currentMission.MaxDuration + " segundos";
        descripTMP.text = descrip;
        MissionPopUp.SetActive(true);
        player.isStoped = true;
    }

    public void Aceptar() {
        player.isStoped = false;
        MissionPopUp.SetActive(false);
        missionPoints[current].Disable(true);
        
        for (int i = 0; i < missionPoints.Length; i++){
            if (missionPoints[i] != null) {
                if (current != i){
                    missionPoints[i].Hide_Unhide(true);
                } else {
                    ocupados[i] = false;
                    int selector = Random.Range(0, missionPoints.Length);

                    while (ocupados[selector] == true)
                        selector = Random.Range(0, missionPoints.Length);

                    ocupados[selector] = true;
                    missionPoints[i].transform.position = puntosInicio[selector].position;
                }
            } 
        }
        finMision.gameObject.SetActive(true);
        finMision.transform.position = puntosEntrega[Random.Range(0, puntosEntrega.Length)].position;
        finMision.Hide(false);
    }

    public void Rechazar() {
        player.isStoped = false;
        MissionPopUp.SetActive(false);
    }

    public void FinMision() {
        currentMission = null;
        for (int i = 0; i < missionPoints.Length; i++) { //reactivamos los puntos de mision
            if (missionPoints[i] != null) {
                    missionPoints[i].Hide_Unhide(false);
            }
        }
    }

    IEnumerator cambiarPos() {
        yield return new WaitForSeconds(2.0f);
    }

    string getTipoMision(MisionType _tipo) {
        string result="";
        switch (_tipo) 
        {
            case MisionType.contraReloj: result = "Contra Reloj"; break;
            case MisionType.noDanios: result = "Sin daños"; break;
            case MisionType.sinFrenar: result = "Sin Frenar"; break;
            default: break;
        }
        return result;
    }
}
