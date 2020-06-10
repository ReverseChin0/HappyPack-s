using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] Transform[] puntosInicio;
    [SerializeField] GameObject BeginPref = default, EndPref = default;
    bool[] ocupados;
    public int maxMisiones = 3;

    private void Start() 
    {
        ocupados = new bool[puntosInicio.Length];

        for (int o = 0; o < ocupados.Length; o++) 
            ocupados[o] = false;
        
        for (int i = 0; i < maxMisiones; i++) {
            int selector = Random.Range(0, puntosInicio.Length);

            while(ocupados[selector]==true)
                selector = Random.Range(0, puntosInicio.Length);

            ocupados[selector] = true;
            Instantiate(BeginPref, puntosInicio[selector].position, Quaternion.identity, puntosInicio[selector].parent);
        }
    }

}
