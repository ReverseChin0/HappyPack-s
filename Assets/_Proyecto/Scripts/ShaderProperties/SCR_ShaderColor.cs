using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_ShaderColor : MonoBehaviour
{
    [SerializeField, ColorUsage(true, true)] Color color = default;
    [SerializeField] string colorString = "_color", floatOnTrigger = "_alphaVal";
    [SerializeField] float desde = 1.0f, hasta = 0.0f , duracion = 1.0f;
    bool deactivar = false;
    float alpha = 0;
    Renderer miRenderer;
    MaterialPropertyBlock miProp;
    Collider colli;

    private void Awake() 
    {
        miRenderer = GetComponent<Renderer>();
        miProp = new MaterialPropertyBlock();
        colli = GetComponent<Collider>();
    }

    private void Start() 
    {
        miProp.SetColor(colorString, color);
        miRenderer.SetPropertyBlock(miProp);
    }

    public void Deactivate(bool _goCompleto) {
        colli.enabled = false;
        deactivar = _goCompleto;
        DOTween.To(UpdatePropBlock, desde, hasta, duracion).SetEase(Ease.OutExpo).OnComplete(Disable);
    }

    public void Hide(bool _hide) {
        colli.enabled = !_hide;
        DOTween.To(UpdatePropBlock, _hide ? desde : hasta, _hide ? hasta : desde, duracion).SetEase(Ease.OutExpo);
    }

    void Disable() {
        colli.enabled = true;
        gameObject.SetActive(!deactivar);
    }

    void UpdatePropBlock(float aplha) {
        //print(aplha);
        miProp.SetFloat(floatOnTrigger, aplha);
        miRenderer.SetPropertyBlock(miProp);
    }
}
