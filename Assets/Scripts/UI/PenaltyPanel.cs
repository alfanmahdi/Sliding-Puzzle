using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyPanel : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().penaltySoundPlay();
    }

    private void OnDisable()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().penaltySoundStop();
    }
}
