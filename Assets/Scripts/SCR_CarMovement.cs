using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CarMovement : MonoBehaviour
{
    [SerializeField] float aceleracion = 1.0f, velocidadMax = 1.0f;
    [SerializeField,Range(0.0f,1.0f)]float sensiGiro = 3.0f;
    public bool isPhone = false;

    Vector3 direccionfinal;
    Transform _transform;
    Rigidbody _rb;
    float velocidad=0, velocidadActual=0.0f;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void Start() {
        
    }

    private void Update() 
    {
        if (isPhone)
            InputsCelular();
        else
            InputsTeclas();

        velocidad = Mathf.Clamp(velocidad, -velocidadMax*0.1f, velocidadMax);

        if(velocidad!=0)
        velocidadActual = velocidad/velocidadMax;

        if (velocidad > 0) 
        {
            direccionfinal = transform.forward * velocidad;
        } 
        else 
        {
            direccionfinal = Vector3.zero;
        }
    }

    private void FixedUpdate() 
    {
        _rb.MovePosition(transform.position + direccionfinal);
    }

    private void InputsCelular() 
    {
        if (Input.touchCount > 0) {
            velocidad -= aceleracion * 0.5f * Time.deltaTime;
        } else {
            velocidad += aceleracion * Time.deltaTime;
        }
        float rotacion = Input.acceleration.x;
        _transform.Rotate(Vector3.up, rotacion * sensiGiro);
    }

    private void InputsTeclas() {
        if (Input.GetKey(KeyCode.Space)) {
            velocidad -= aceleracion * 0.5f * Time.deltaTime;
        } else {
            velocidad += aceleracion * Time.deltaTime;
        }
        float rotacion = Input.GetAxis("Horizontal");
        _transform.Rotate(Vector3.up, rotacion * sensiGiro);
    }

    protected void OnGUI() {
        GUI.skin.label.fontSize = Screen.width / 40;

        /*GUILayout.Label("Orientation: " + Screen.orientation);
        if (isPhone) {
            GUILayout.Label("Acelerometro: " + Input.acceleration);
        }*/
        GUILayout.Label("iphone width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);
        GUILayout.Label("CurrentSpeed: " + velocidadActual);
        if(isPhone)
            GUILayout.Label("PhoneModeActivated");
    }

    public void toggleIsPhone() {
        isPhone = !isPhone;
    }

    public static float Remap(float value, float min, float max, float newMin, float newMax) {
        var fromAbs = value - min;
        var fromMaxAbs = max - min;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = newMax - newMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + newMin;

        return to;
    }

}
