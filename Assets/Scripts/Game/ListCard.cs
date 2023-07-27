using UnityEngine;
using UnityEngine.UI;

public class ListCard : MonoBehaviour
{
    public static ListCard instance;
    public GameObject redList;
    public GameObject blueList;
    public GameObject yellowList;
    public GameObject greyList;
    public GameObject greenList;
    public Transform cardRedList;
    public Transform cardBlueList;
    public Transform cardYellowList;
    public Transform cardGreyList;
    public Transform cardGreenList;

    private void Awake(){ instance = this; }

    private void OnEnable()
    {
        if (GameManager.Instance.activePanel == ActivePanel.hidden)
        {
            CloseAllListPanel();
            OpenRedCard();
        }
        else if (GameManager.Instance.activePanel == ActivePanel.unlock)
        {
            CloseAllListPanel();
            OpenYellowCard();
        }
       else if (GameManager.Instance.activePanel == ActivePanel.machine)
        {
            CloseAllListPanel();
            OpenGreenCard();
        }
        else if (GameManager.Instance.activePanel == ActivePanel.combine)
        {
            if (GameManager.Instance.choiceCombineCard1 && !GameManager.Instance.choiceCombineCard2)
            {
                CloseAllListPanel();
                OpenRedCard();
            }
            if (GameManager.Instance.choiceCombineCard2 && !GameManager.Instance.choiceCombineCard1)
            {
                CloseAllListPanel();
                OpenBlueCard();
            }
        }

        else
        {
            CloseAllListPanel();
            OpenRedCard();
        }


    }

    public void CloseAllListPanel()
    {
        redList.SetActive(false);
        blueList.SetActive(false);
        yellowList.SetActive(false);
        greyList.SetActive(false);
        greenList.SetActive(false);
    }

    public void OpenRedCard()
    {
        CloseAllListPanel();
        redList.SetActive(true);
    }

    public void OpenBlueCard()
    {
        CloseAllListPanel();
        blueList.SetActive(true);
    }

    public void OpenYellowCard()
    {
        CloseAllListPanel();
        yellowList.SetActive(true);
    }

    public void OpenGreyCard()
    {
        CloseAllListPanel();
        greyList.SetActive(true);
    }

    public void OpenGreenCard()
    {
        CloseAllListPanel();
        greenList.SetActive(true);
    }

    public void AddCardToList(string id)
    {
        CardDetailSO cardDetail = GameManager.Instance.GetCardDetailByID(id);
        GameObject card = null;
        switch (cardDetail.cardType)
        {
            case CardType.red:
                card = Instantiate(GameResource.Instance.cardChoice, cardRedList);
                break;
            case CardType.blue:
                card = Instantiate(GameResource.Instance.cardChoice, cardBlueList);
                break;
            case CardType.yellow:
                card = Instantiate(GameResource.Instance.cardChoice, cardYellowList);
                break;
            case CardType.grey:
                card = Instantiate(GameResource.Instance.cardChoice, cardGreyList);
                break;
            case CardType.green:
                card = Instantiate(GameResource.Instance.cardChoice, cardGreenList);
                break;
        }
        if (cardDetail.cardType != CardType.map && card != null)
        {
            card.GetComponent<CardChoice>().cardDetail = cardDetail;
            card.GetComponent<Image>().sprite = cardDetail.cardSprite;
        }
    }

    public void DeleteCardFromList(string id)
    {
        CardDetailSO cardDetail = GameManager.Instance.GetCardDetailByID(id);
        switch (cardDetail.cardType)
        {
            case CardType.red:
                GameObject findRedCard = GameManager.Instance.GetCardListByID(id, cardRedList);
                Destroy(findRedCard);
                break;
            case CardType.blue:
                GameObject findBlueCard = GameManager.Instance.GetCardListByID(id, cardBlueList);
                Destroy(findBlueCard);
                break;
            case CardType.yellow:
                GameObject findYellowCard = GameManager.Instance.GetCardListByID(id, cardYellowList);
                Destroy(findYellowCard);
                break;
            case CardType.grey:
                GameObject findGreyCard = GameManager.Instance.GetCardListByID(id, cardGreyList);
                Destroy(findGreyCard);
                break;
            case CardType.green:
                GameObject findGreenCard = GameManager.Instance.GetCardListByID(id, cardGreenList);
                Destroy(findGreenCard);
                break;
        }
    }
}
