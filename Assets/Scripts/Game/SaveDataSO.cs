using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SaveData_", menuName = "Scriptable Objects/Save Data")]
public class SaveDataSO : ScriptableObject
{
    public int score;
    public int mapIndex;
    public int currentCoin;
    public float currentTime;
    public int currentDiscard;
    public List<string> ownedCardId;
}