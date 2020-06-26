using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PauseStuff : MonoBehaviour
{
    public void makePause(bool pause) {
        Time.timeScale = pause ? 0 : 1;
    }
}
