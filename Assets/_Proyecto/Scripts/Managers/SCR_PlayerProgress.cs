using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SCR_PlayerProgress : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt_dinero = default, txt_misioHistoria = default, txt_additionMoney = default;
    [SerializeField] Image saveIcon;
    int dinerActual = 0; //dineroactual
    int misiActual = 0;
    Sequence saveSequence;

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
    }

    [ContextMenu("saveStats")]
    public void SaveStats() 
    {
        PlayerPrefs.SetInt("dinero", dinerActual);
        PlayerPrefs.SetInt("misiones", misiActual);
        saveSequence = DOTween.Sequence();
        saveSequence.Append(saveIcon.DOFade(1, 0.35f))
                    .Append(saveIcon.DOFade(.1f, 0.1f))
                    .Append(saveIcon.DOFade(1, 0.35f))
                    .Append(saveIcon.DOFade(0f, 0.1f));
        //saveSequence.Play();
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
