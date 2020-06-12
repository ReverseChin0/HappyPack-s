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

    public SCR_MissionData(GameObject _meta) {
        Meta = _meta;
        int lenght = MisionType.GetNames(typeof(MisionType)).Length;
        tipodeMision = (MisionType)Random.Range(0, lenght);
        MaxDuration = Random.Range(45, 90);
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
    }

    public void RandomizarMision() {
        int lenght = MisionType.GetNames(typeof(MisionType)).Length;
        tipodeMision = (MisionType)Random.Range(0, lenght);
        MaxDuration = Random.Range(45, 90);
    }
}
