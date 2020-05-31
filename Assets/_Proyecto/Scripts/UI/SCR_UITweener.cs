using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum TipoDeTween
{
    Mover,
    Escalar
}

public class SCR_UITweener : MonoBehaviour
{
    [SerializeField] private bool reproducirStart = false;
    [SerializeField] private bool reproducirEnable = false;
    [SerializeField] private bool posicionInicial = false;
    [SerializeField] private float delay = 0.0f, duracion = 0.5f;
    [SerializeField] private TipoDeTween tipoTween = TipoDeTween.Mover;
    [SerializeField] private Ease tipoDeSuavizado = default;
    [SerializeField] private Vector2 posfinal=default;
    [SerializeField] Vector2 posInicial=default;
    RectTransform rectTransform;
    Sequence mySequence;

    private void Awake() 
    {
        rectTransform = GetComponent<RectTransform>();
        mySequence = DOTween.Sequence();
    }

    private void Start() 
    {
        if (!posicionInicial)
            posInicial = rectTransform.anchoredPosition;

        if (reproducirStart)
        Reproducir();
    }

    private void OnEnable() 
    {
        if (reproducirEnable)
            Reproducir();
    }

    public void Reproducir() 
    {
        switch (tipoTween) 
        {
            case TipoDeTween.Mover:
                if (posicionInicial)
                    rectTransform.anchoredPosition = new Vector3(posInicial.x, posInicial.y, 0);

                rectTransform.
                    DOAnchorPos(posfinal, duracion).
                    SetDelay(delay).
                    SetEase(tipoDeSuavizado);
                break;

            case TipoDeTween.Escalar:
                if (posicionInicial)
                    rectTransform.localScale = new Vector3(posInicial.x, posInicial.y, 0);

                rectTransform.
                    DOScale(new Vector3(posfinal.x,posfinal.y,0),duracion).
                    SetDelay(delay).
                    SetEase(tipoDeSuavizado);
                break;

            default: break;
        }

        
    }

    public void Switch() 
    {
        Vector2 temp = posInicial;
        posInicial = posfinal;
        posfinal = temp;
    }
}
