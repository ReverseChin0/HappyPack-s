using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MisionType
{
    contraReloj,
    noDanios,
    sinFrenar
}

public class SCR_MissionData
{
    public GameObject Meta;
    public MisionType tipodeMision;
    public float MaxDuration;
    public int precioMision;
    public int paqueteCount = 1;

    public SCR_MissionData(GameObject _meta) {
        Meta = _meta;
        int lenght = MisionType.GetNames(typeof(MisionType)).Length;
        tipodeMision = (MisionType)Random.Range(0, lenght);
        MaxDuration = Random.Range(45, 60);
        precioMision = (int)(Remap(MaxDuration, 45, 90, 1, 1.5f) * 50.0f);
    }

    public SCR_MissionData(GameObject _meta, MisionType _misiontype, float _duration) {
        Meta = _meta;
        tipodeMision = _misiontype;
        MaxDuration = _duration;
    }

    public SCR_MissionData() {
        Meta = null;
        tipodeMision = MisionType.contraReloj;
        MaxDuration = 45;
        precioMision = 50;
    }

    public void RandomizarMision() 
    {
        int lenght = MisionType.GetNames(typeof(MisionType)).Length;
        tipodeMision = (MisionType)Random.Range(0, lenght);
        MaxDuration = Random.Range(45, 60);
        precioMision = (int)(Remap(MaxDuration, 45, 90, 1, 1.5f) * 50.0f);
        paqueteCount = Random.Range(1, 4);
    }

    static float Remap(float value, float min, float max, float newMin, float newMax) {
        var fromAbs = value - min;
        var fromMaxAbs = max - min;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = newMax - newMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + newMin;

        return to;
    }
}
