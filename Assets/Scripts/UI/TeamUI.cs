using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI teamName;

    public void SetName(string name)
    {
        teamName.text = name;
    }
}
