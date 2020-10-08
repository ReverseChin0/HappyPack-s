using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_FlechaDireccion : MonoBehaviour
{
    Transform trFlecha = default;
    Vector3 Direction = default;
    bool enMision = false;

    private void Awake()
    {
        trFlecha = transform;
    }

    private void Update()
    {
        if (!enMision)
            return;

        Vector3 dir = Direction - trFlecha.position;
        
        var rotation = Quaternion.LookRotation(dir, Vector3.up);
        rotation *= Quaternion.Euler(0, 270, -10);
        trFlecha.rotation = Quaternion.Slerp(trFlecha.rotation, rotation, Time.deltaTime * 100); ;
    }

    public void iniciarFlecha(Vector3 _puntofinal)
    {
        Direction = _puntofinal;
        enMision = true;
    }

    public void FinalizarFlecha()
    {
        Direction = Vector3.up;
        enMision = false;
        gameObject.SetActive(false);
    }
}
