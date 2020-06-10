using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MisionType
{
    contraReloj,
    noDanios,
    sinFrenar
}

public class MissionData
{
    public GameObject Meta;
    public MisionType tipodeMision;
    public float MaxDuration;

    public MissionData(GameObject _meta) {
        Meta = _meta;
        int lenght = MisionType.GetNames(typeof(MisionType)).Length;
        tipodeMision = (MisionType)Random.Range(0, lenght);
        MaxDuration = Random.Range(45, 90);
    }

    public MissionData(GameObject _meta, float _duration) {
        Meta = _meta;
        int lenght = MisionType.GetNames(typeof(MisionType)).Length;
        tipodeMision = (MisionType)Random.Range(0, lenght);
        MaxDuration = _duration;
    }

    public MissionData(GameObject _meta, MisionType _misiontype, float _duration) {
        Meta = _meta;
        tipodeMision = _misiontype;
        MaxDuration = _duration;
    }
}
