using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderColor : MonoBehaviour
{
    [SerializeField, ColorUsage(true, true)] Color color;
    [SerializeField] string colorString = "_color", floatOnTrigger = "_alphaVal";
    [SerializeField] float desde = 1.0f, hasta = 0.0f , duracion = 1.0f;
    float alpha = 0;
    Renderer miRenderer;
    MaterialPropertyBlock miProp;

    private void Awake() 
    {
        miRenderer = GetComponent<Renderer>();
        miProp = new MaterialPropertyBlock();
    }

    private void Start() 
    {
        miProp.SetColor(colorString, color);
        miRenderer.SetPropertyBlock(miProp);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) {
            DOTween.To(UpdatePropBlock,desde,hasta, duracion).SetEase(Ease.OutExpo);
        }
    }

    void UpdatePropBlock(float aplha) {
        print(aplha);
        miProp.SetFloat(floatOnTrigger, aplha);
        miRenderer.SetPropertyBlock(miProp);
    }
}
