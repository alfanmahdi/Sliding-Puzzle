using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelChoiceCard : MonoBehaviour
{
    public Transform cardListParent;
    public Transform cardListRed;
    public Transform cardListYellow;
    public Transform cardListGreen;
    public Transform cardListBlue;
    public Transform cardListGrey;

    // Ini class masih kotor banget, bnyk initialize and destroy gameObject. Optimasi kedepan bisa pake predefined gameObject trs di setActive() aja

    private void OnEnable()
    {
        foreach (Transform child in GameManager.Instance.deckCardHolder.transform)
        {
            var cardChoice = Instantiate(GameResource.Instance.cardChoice, cardListParent);
            cardChoice.GetComponent<Image>().sprite = child.GetComponent<Card>().cardDetail.cardSprite;
            cardChoice.GetComponent<CardChoice>().cardDetail = child.GetComponent<Card>().cardDetail;

            switch (child.GetComponent<Card>().cardDetail.cardType)
            {
                case CardType.red:
                    Instantiate(cardChoice, cardListRed);
                    break;

                case CardType.yellow:
                    Instantiate(cardChoice, cardListYellow);
                    break;

                case CardType.green:
                    Instantiate(cardChoice, cardListGreen);
                    break;

                case CardType.blue:
                    Instantiate(cardChoice, cardListBlue);
                    break;

                case CardType.grey:
                    Instantiate(cardChoice, cardListGrey);
                    break;
            }
        }
    }

    private void OnDisable()
    {
        foreach(Transform child in cardListParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in cardListRed.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in cardListYellow.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in cardListGreen.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in cardListBlue.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in cardListGrey.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void InitialiseChoicePanel()
    {
        cardListParent.gameObject.SetActive(false);
        cardListRed.gameObject.SetActive(false);
        cardListYellow.gameObject.SetActive(false);
        cardListGreen.gameObject.SetActive(false);
        cardListBlue.gameObject.SetActive(false);
        cardListGrey.gameObject.SetActive(false);
    }

    public void OpenAllChoicePanel()
    {
        InitialiseChoicePanel();
        cardListParent.gameObject.SetActive(true);
    }

    public void OpenRedChoicePanel()
    {
        InitialiseChoicePanel();
        cardListRed.gameObject.SetActive(true);
    }

    public void OpenYellowChoicePanel()
    {
        InitialiseChoicePanel();
        cardListYellow.gameObject.SetActive(true);
    }

    public void OpenGreenChoicePanel()
    {
        InitialiseChoicePanel();
        cardListGreen.gameObject.SetActive(true);
    }

    public void OpenBlueChoicePanel()
    {
        InitialiseChoicePanel();
        cardListBlue.gameObject.SetActive(true);
    }

    public void OpenGreyChoicePanel()
    {
        InitialiseChoicePanel();
        cardListGrey.gameObject.SetActive(true);
    }
}
