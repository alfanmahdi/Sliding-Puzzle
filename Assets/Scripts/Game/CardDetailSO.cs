using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CardDetail_", menuName = "Scriptable Objects/Card Detail")]
public class CardDetailSO : ScriptableObject
{
    // Card detail
    public Sprite cardSprite;
    public string cardDescription;
    public string cardID;
    public CardType cardType;

    // Combine Cards detail, misal card tidak bisa dicombine, isi value dengan 0
    public List<string> combineCardsProducesID;

    // Hidden card detail, misal card tidak ada hiddennya, isi value dengan 0
    public string hiddenCardProducesID;

    // Unlock card detail, misal card tidak ada unlocknya, isi value dengan 0
    public string unlockCardAnswer;
    public string[] unlockCardProducesID;
    public string[] destroyedCardID;
    public int mapIndex;
    public bool haveText = false;
    public string text;
}

