using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DiscardSystem : MonoBehaviour
{
    [SerializeField] public Text discardCount;

    public void SetDiscard(int card)
    {
        string discard = string.Format("Discard Card : {0:0}", card);
        discardCount.text = discard;
    }
}
