using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SCR_PlayerProgress : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt_dinero = default, txt_misioHistoria = default, txt_additionMoney = default;
    [SerializeField] Image saveIcon = default;
    int dinerActual = 0; //dineroactual
    int misiActual = 0;
    Sequence genericSequ;
    Vector2 inicialAnchors;

    private void Awake() 
    {
        //==============================================================================================================================================||

        if (PlayerPrefs.HasKey("dinero")) //checo si ya hay llave que guarde el dinero
            dinerActual = PlayerPrefs.GetInt("dinero"); //si si le pido el dinero
        else 
            PlayerPrefs.SetInt("dinero", 0); //si no pues es Nueva Partida y Empieza en 0

        if (txt_dinero != null)
            txt_dinero.text = "$" + dinerActual.ToString(); //asigno dineroactual

        //==============================================================================================================================================||

        if (PlayerPrefs.HasKey("misiones")) //checo si ya hay llave que guarde el dinero
            misiActual = PlayerPrefs.GetInt("misiones"); //si si le pido el dinero
        else
            PlayerPrefs.SetInt("misiones", 0); //si no pues es Nueva Partida y Empieza en 0

        if (txt_misioHistoria != null)
            txt_misioHistoria.text =  misiActual.ToString(); //asigno dineroactual

        //==============================================================================================================================================||
        inicialAnchors = txt_additionMoney.rectTransform.anchoredPosition;
    }

    [ContextMenu("saveStats")]
    public void SaveStats() 
    {
        PlayerPrefs.SetInt("dinero", dinerActual);
        PlayerPrefs.SetInt("misiones", misiActual);
        genericSequ = DOTween.Sequence();
        genericSequ.Append(saveIcon.DOFade(1, 0.35f))
                    .Append(saveIcon.DOFade(.1f, 0.1f))
                    .Append(saveIcon.DOFade(1, 0.35f))
                    .Append(saveIcon.DOFade(0f, 0.1f));
    }

    //[ContextMenu("addmoney")]
    public void AddMoney(int _cantidad) 
    {
        dinerActual += _cantidad;
        txt_additionMoney.color = _cantidad > 0 ? new Color(0.0f, 0.8f, 0.0f) : new Color(0.8f, 0.0f, 0.0f); //elige color basado en si gana o pierde dinero
        txt_additionMoney.text = "+ $ " + _cantidad.ToString(); //pone texto de adicion
        txt_dinero.text = "$ " + dinerActual.ToString(); //pone texto de dinero
        genericSequ = DOTween.Sequence();
        txt_additionMoney.rectTransform.anchoredPosition = inicialAnchors;
        genericSequ.Append(txt_additionMoney.rectTransform.DOAnchorPosY(_cantidad > 0 ? 50.0f : -50.0f, 1.9f)) //checa si el dinero es positivo o negativo para subir o bajar
                   .Append(txt_additionMoney.DOFade(1, 0.9f))
                   .Append(txt_additionMoney.DOFade(0, 1).SetEase(Ease.InExpo));
    }

    [ContextMenu("resetStats")]
    public void ResetStats() 
    { //reiniciar stats
        PlayerPrefs.SetInt("dinero",0);
        PlayerPrefs.SetInt("misiones",0);
        dinerActual = 0;
        misiActual = 0;
        txt_dinero.text = "$" + dinerActual.ToString(); //asigno dineroactual
        txt_misioHistoria.text = misiActual.ToString(); //asigno dineroactual
    }
}
