using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SCR_FlechaDireccion : MonoBehaviour
{
    MeshRenderer flechaMat = default;
    Transform trFlecha = default;
    Vector3 Direction = default;
    bool enMision = false;
    int shaderprop = 0;

    private void Awake()
    {
        trFlecha = transform;
        flechaMat = GetComponent<MeshRenderer>();
        shaderprop = Shader.PropertyToID("_disolveamnt");
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
        trFlecha.DOShakeScale(1.5f, 1, 5, 50, true);
        flechaMat.material.DOFloat(1, shaderprop, 1);
    }

    public void FinalizarFlecha()
    {
        Direction = Vector3.up;
        enMision = false;
        flechaMat.material.DOFloat(0, shaderprop, 1).OnComplete(deactivateFlecha);
    }

    void deactivateFlecha() 
    {
        gameObject.SetActive(false);
    }
}
