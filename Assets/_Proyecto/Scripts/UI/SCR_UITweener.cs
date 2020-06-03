using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
 * Francisco Emmanuel Castañeda López
 * Libreria de Tween para UI Extensible
 */

public enum TipoDeTween
{
    Mover,
    Escalar,
    Desvanecer,
    Saltar,
    Columpiar
}

public class SCR_UITweener : MonoBehaviour
{
    [SerializeField] private bool reproducirStart = false;
    [SerializeField] private bool reproducirEnable = false;
    [SerializeField] private bool usarPosicionInicial = false;
    [SerializeField] private bool loops = false;
    [SerializeField] private float delay = 0.0f, duracion = 0.5f;
    [SerializeField] private TipoDeTween tipoTween = TipoDeTween.Mover;
    [SerializeField] private Ease tipoDeSuavizado = default;
    [SerializeField] private Vector2 posfinal=default;
    [SerializeField] Vector2 posInicial=default;
    RectTransform rectTransform = default;
    CanvasGroup cGroup = default;

    private void Awake() 
    {
        rectTransform = GetComponent<RectTransform>();

        if (tipoTween == TipoDeTween.Desvanecer) {
            cGroup = GetComponent<CanvasGroup>();
            if (cGroup == null)
                cGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void Start() 
    {
        if (!usarPosicionInicial)
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
                if (usarPosicionInicial)
                    rectTransform.anchoredPosition = posInicial;

                rectTransform.
                    DOAnchorPos(posfinal, duracion).
                    SetDelay(delay).
                    SetEase(tipoDeSuavizado).SetLoops(loops ? -1 : 0);
                break;

            case TipoDeTween.Escalar:
                if (usarPosicionInicial)
                    rectTransform.localScale = new Vector3(posInicial.x, posInicial.y, 0);

                rectTransform.
                    DOScale(new Vector3(posfinal.x,posfinal.y,0),duracion).
                    SetDelay(delay).
                    SetEase(tipoDeSuavizado).
                    SetLoops(loops ? -1:0);
                break;

            case TipoDeTween.Desvanecer:
                if (usarPosicionInicial)
                    cGroup.alpha = posInicial.x;

                cGroup.
                    DOFade(posfinal.x,duracion).
                    SetDelay(delay).
                    SetEase(tipoDeSuavizado).
                    SetLoops(loops ? -1 : 0,LoopType.Yoyo);
                break;

            case TipoDeTween.Saltar:
                if (usarPosicionInicial)
                    rectTransform.anchoredPosition = posInicial;

                rectTransform.
                    DOJumpAnchorPos(posfinal,100.0f,0,duracion).
                    SetDelay(delay).
                    SetLoops(loops ? -1 : 0);
                break;

            case TipoDeTween.Columpiar:
                if (usarPosicionInicial)
                    rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, posInicial.x));

                rectTransform.
                    DOLocalRotate(new Vector3(0,0,posfinal.x),duracion).
                    SetEase(tipoDeSuavizado).
                    SetLoops(loops ? -1 : 0,LoopType.Yoyo);
                break;

            default:
                break;
        }
    }

    public void Switch() 
    {
        Vector2 temp = posInicial;
        posInicial = posfinal;
        posfinal = temp;
    }

    public void InvertirYReproducir() {
        Switch();
        Reproducir();
    }
}
