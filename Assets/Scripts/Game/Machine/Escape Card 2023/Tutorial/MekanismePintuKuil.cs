using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MekanismePintuKuil : MonoBehaviour
{
    public Buletan B1,B2,B3,B4,B5,B6;

    /*
    1 2
    3 4
    5 6
    */

    bool correct;
    // 2 dan 3 di rotate

    void Awake()
    {
        correct=false;
        // Debug.Log("correct set to false");
    }

    public void Submit()
    {
        // Debug.Log("Submitted");
        if(!B1.isMuter && B2.isMuter && B3.isMuter&& !B4.isMuter&& !B5.isMuter && !B6.isMuter)
        {
            correct=true;
            // Debug.Log("KOMBINASI PUTAR BENAR :)");
        }
        else
        {
            // Debug.Log("KOMBINASI PUTAR SALAH");
        }
    }

    public void Reset()
    {
        // Debug.Log("Reset All Buletan");
        B1.BuletReset();
        B2.BuletReset();
        B3.BuletReset();
        B4.BuletReset();
        B5.BuletReset();
        B6.BuletReset();

    }
}


/*
*/