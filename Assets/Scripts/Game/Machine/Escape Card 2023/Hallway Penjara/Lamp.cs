using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        //SetLampState(false);
    }

    public void SetLampState(bool isOn)
    {
        gameObject.SetActive(isOn);
    }

    public bool CheckState(){
        return gameObject.activeSelf;
    }
}
