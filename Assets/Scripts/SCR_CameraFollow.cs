using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CameraFollow : MonoBehaviour
{
    [SerializeField] Transform targetToLook;
    [SerializeField] Transform targetPoint;
    [SerializeField, Range(0.0f,5.0f)] float smoothTime = 1.0f;
    Transform _tran;

    Vector3 velo = Vector3.zero;
    private void Awake() 
    {
        _tran = transform;    
    }

    private void LateUpdate() {
        _tran.position = Vector3.SmoothDamp(_tran.position, targetPoint.position, ref velo, smoothTime);
        Vector3 direccion = targetToLook.position - _tran.position;
        _tran.rotation = Quaternion.LookRotation(direccion,Vector3.up);
    }
}
