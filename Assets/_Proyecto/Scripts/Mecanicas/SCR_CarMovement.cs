using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_CarMovement : MonoBehaviour
{
    [SerializeField] float aceleracion = 1.0f, velocidadMax = 1.0f, treshold = -20.0f;
    [SerializeField,Range(0.0f,1.0f)]float sensiGiro = 1.0f;
    [SerializeField] Transform _modelo = default, _spawnPoint = default;
    [SerializeField] Image imgLife = default, imgGasoline = default;
    [SerializeField] SCR_PlayerProgress playStats = default;
    Vector3 direccionfinal,rotacionEuler;
    Quaternion desiredRot;
    Transform _transform;
    Rigidbody _rb;
    float velocidad = 0, velocidadActual = 0.0f, rotacion = 0.0f, tiltamount, gaspoints = 1.0f;
    int maxHp = 100, hp = 100;
    public bool isPhone = false, isStoped = false;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
        if(playStats!=null) playStats = FindObjectOfType<SCR_PlayerProgress>();
    }

    private void Start() {
        
    }

    private void Update() 
    {
        if (isPhone)
            InputsCelular();
        else
            InputsTeclas();

        if(!isStoped)
            velocidad = Mathf.Clamp(velocidad, -velocidadMax* 0.05f , velocidadMax);
        else 
            velocidad = 0; 

        TiltCar();

        if (velocidad != 0) {
            if(velocidad > 0)gaspoints -= 0.0001f;
            imgGasoline.fillAmount = gaspoints; 
            velocidadActual = velocidad / velocidadMax;
            float multiplicadorgiro = Remap(velocidadActual, 0, 1, 4, 1);
            
            _transform.Rotate(Vector3.up, rotacion * sensiGiro * multiplicadorgiro);
        }

        direccionfinal = transform.forward * velocidad;

        if(_transform.position.y < treshold) {
            _transform.position = _spawnPoint.position;
            isStoped = true;
            if (playStats != null) playStats.AddMoney(-20);
            StartCoroutine(resetearVehiculo(2.0f));
        }
    }

    private void FixedUpdate() 
    {
        _rb.MovePosition(transform.position + direccionfinal);
    }

    private void InputsCelular() 
    {
        if (Input.touchCount > 0) 
            velocidad -= aceleracion * 0.6f * Time.deltaTime;
        else {
            if (gaspoints > 0)
                velocidad += aceleracion * Time.deltaTime;
        } 

        rotacion = Remap(Input.acceleration.x,-0.3f,0.3f,-1.0f,1.0f);
    }

    private void InputsTeclas() {
        if (Input.GetKey(KeyCode.Space)) 
            velocidad -= aceleracion * 0.7f * Time.deltaTime;
        else {
            if(gaspoints>0)
            velocidad += aceleracion * Time.deltaTime;
            else {
                transform.position = _spawnPoint.position;
                if (playStats != null) playStats.AddMoney(-100);
                gaspoints = 1;
            }
        }
        
        rotacion = Input.GetAxis("Horizontal");
    }

    /*protected void OnGUI() {
        GUI.skin.label.fontSize = Screen.width / 40;
        
        GUILayout.Label("iphone width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);
        GUILayout.Label("CurrentSpeed: " + velocidadActual);
        GUILayout.Label("currentTilt: " + tiltamount);
        if(isPhone)
            GUILayout.Label("PhoneModeActivated");
    }*/

    public void toggleIsPhone() {
        isPhone = !isPhone;
    }

    void TiltCar() 
    {
        tiltamount = -rotacion * 15.0f * velocidadActual;
        rotacionEuler = (Vector3.up * 180.0f) + Vector3.forward * tiltamount;
        desiredRot = Quaternion.Euler(rotacionEuler);//Quaternion.Euler(_transform.rotation.x,_transform.rotation.y , _transform.rotation.z);
        _modelo.localRotation = desiredRot;//Quaternion.Lerp(_modelo.rotation, desiredRot, Time.deltaTime * rotationSpeed);
    }

    void TakeDmg(int _dmg) 
    {
        if (hp > 0) {
            hp -= _dmg;
            imgLife.fillAmount = hp * 0.01f;
            if (hp <= 0) {
                //Morir
                _transform.position = _spawnPoint.position;
                hp = maxHp;
            }
        }
    }

    public bool AddGas(float _gas) {
        gaspoints += _gas;
        if (gaspoints > 1.0f) 
        {
            gaspoints = 1.0f;
            return false; //regresa false si ya no se le puede añadir mas
        } 
        else 
        {
            return true;//regresa true si aun se le puede añadir mas
        }

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Obstaculo")) {
            velocidad = Mathf.Min(velocidad, velocidadMax * 0.2f);
            TakeDmg(10);
        }
    }

    IEnumerator resetearVehiculo(float _valor) {
        yield return new WaitForSeconds(_valor);
        isStoped = false;
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
