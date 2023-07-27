using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stone : MonoBehaviour
{
    [SerializeField] private MekanismePintuHallwayPenjara pintuPenjara;
    [SerializeField] private int index;
    private bool isOn = false;
    public void Start(){
        SetStateOff();
    }
    public void ToggleSwitch()
    {
        isOn = !isOn;
        pintuPenjara.HandleSwitchChanges(index,isOn);
    }

    public void SetStateOff(){
        isOn=false;
    }
}
