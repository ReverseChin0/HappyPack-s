using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CarMovement : MonoBehaviour
{
    [SerializeField] float aceleracion = 1.0f, velocidadMax = 1.0f;
    [SerializeField,Range(0.0f,1.0f)]float sensiGiro = 1.0f;
    Vector3 direccionfinal;
    Transform _transform;
    Rigidbody _rb;
    float velocidad=0, velocidadActual=0.0f, rotacion=0.0f;

    public bool isPhone = false;

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

        velocidad = Mathf.Clamp(velocidad, -velocidadMax* 0.1f, velocidadMax);

        if (velocidad != 0) {
            velocidadActual = velocidad / velocidadMax;
            float multiplicadorgiro = Remap(velocidadActual, 0, 1, 6, 1);
            //print(multiplicadorgiro);
            _transform.Rotate(Vector3.up, rotacion * sensiGiro * multiplicadorgiro);
        }

        direccionfinal = transform.forward * velocidad;
    }

    private void FixedUpdate() 
    {
        _rb.MovePosition(transform.position + direccionfinal);
    }

    private void InputsCelular() 
    {
        if (Input.touchCount > 0) 
            velocidad -= aceleracion * 0.6f * Time.deltaTime;
         else 
            velocidad += aceleracion * Time.deltaTime;
        
         rotacion = Remap(Input.acceleration.x,-0.3f,0.3f,-1.0f,1.0f);
        
    }

    private void InputsTeclas() {
        if (Input.GetKey(KeyCode.Space)) 
            velocidad -= aceleracion * 0.6f * Time.deltaTime;
        else 
            velocidad += aceleracion * Time.deltaTime;
        
        rotacion = Input.GetAxis("Horizontal");
    }

    protected void OnGUI() {
        GUI.skin.label.fontSize = Screen.width / 40;
        
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
