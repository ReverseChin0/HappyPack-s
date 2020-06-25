using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_SkyBoxRotate : MonoBehaviour
{
    public float RotateSpeed;


    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotateSpeed);
    }
}
