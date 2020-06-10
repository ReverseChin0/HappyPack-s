using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MissionPointData : MonoBehaviour
{
    SCR_MissionData misionPropia = default;
    public INotificable miNotificador = default;
    SCR_ShaderColor mishader;
    Collider colli;

    private void Awake() {
        mishader = GetComponent<SCR_ShaderColor>();
    }

    public void GenerarMision(GameObject _go) {
        misionPropia = new SCR_MissionData(_go);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (miNotificador != null) {
                miNotificador.Notificar(gameObject);
            }
        }
    }

    public SCR_MissionData getMission() {
        return misionPropia;
    }

    public void Disable(bool _goCompleto) {
        mishader.Deactivate(_goCompleto);
        misionPropia.RandomizarMision();
    }
}
