using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PressAnyButton : MonoBehaviour
{
    public GameObject activable;
    private void Update() {
        if (Input.anyKey) {
            activable.SetActive(true);
            gameObject.SetActive(false);
        }

        if (Input.touchCount > 0) {
            activable.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
