using System.Collections;
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
        foreach(SCR_MissionPointData mp in missionPoints) {

            if (mp == _go.GetComponent<SCR_MissionPointData>()) 
            {
                current = i;
            }
            i++;
        }
        currentMission =  missionPoints[current].getMission();
        string descrip = currentMission.tipodeMision.ToString() + " en " + currentMission.MaxDuration + "segundos";
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
        finMision.Hide(false);
    }

    public void Rechazar() {
        player.isStoped = false;
        MissionPopUp.SetActive(false);
    }

    IEnumerator cambiarPos() {

        yield return new WaitForSeconds(2.0f);

    }
}
