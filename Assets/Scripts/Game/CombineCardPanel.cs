using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombineCardPanel : MonoBehaviour
{
    public GameObject warning;
    public GameObject PenaltyPanel;
    public GameObject silangButton1;
    public GameObject silangButton2;
    public GameObject notification;

    private CardDetailSO combinedCardProducedDetails;
    private CardDetailSO selectedCardDetails;
    private bool cardCollected = true;
    public GameObject map;
    MapCardPanel cardPanel;
    private bool isChangeScene;
    private string sceneName;
    private string isGenerated;

    private void Awake()
    {
        cardPanel = map.GetComponent<MapCardPanel>();
    }

    private void OnEnable()
    {
        GameManager.Instance.activePanel = ActivePanel.combine;
    }

    public void RemoveCard1FromHolder()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        silangButton1.SetActive(false);
        GameManager.Instance.selectedCombineCard1 = null;
        GameManager.Instance.combineCardImageSelectedRed.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
    }

    public void RemoveCard2FromHolder()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        silangButton2.SetActive(false);
        GameManager.Instance.selectedCombineCard2 = null;
        GameManager.Instance.combineCardImageSelectedBlue.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
    }
    

    public void CombineCardSubmit()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        if (GameManager.Instance.selectedCombineCard1 == null || GameManager.Instance.selectedCombineCard2 == null)
        {
            warning.SetActive(true);
            return;
        }

        bool sameProduce = true;
        foreach(string id in GameManager.Instance.selectedCombineCard1.combineCardsProducesID)
        {
            if (!GameManager.Instance.selectedCombineCard2.combineCardsProducesID.Contains(id)){
                sameProduce = false;
                break;
            }
        }

        if (sameProduce && GameManager.Instance.selectedCombineCard1.combineCardsProducesID[0] != "0" 
                && GameManager.Instance.selectedCombineCard1.cardID != GameManager.Instance.selectedCombineCard2.cardID)
        {
            if (!cardCollected)
            {
                warning.SetActive(true);
                return;
            }

            selectedCardDetails = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedCombineCard1.cardID);
            combinedCardProducedDetails = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedCombineCard1.combineCardsProducesID[0]);
            GameManager.Instance.combineCardProducedImage.GetComponent<Image>().sprite = combinedCardProducedDetails.cardSprite;

            //Misal terunlock, maka kartu akan hilang
            silangButton1.SetActive(false);
            silangButton2.SetActive(false);

            foreach (string id in combinedCardProducedDetails.destroyedCardID)
            {
                DBManager.ownedCards.Remove(id);
                Player.instance.ownedCardId.Remove(id);
                Destroy(GameManager.Instance.GetCardByID(id));
                GameManager.Instance.listCardHolder.GetComponent<ListCard>().DeleteCardFromList(id);
                Player.instance.currentDiscard++;
                DBManager.discardCardsCount++;
                Player.instance.discUI.SetDiscard(Player.instance.currentDiscard);
                DBManager.scores += 5;
                Player.instance.score += 5;
            }

            GameManager.Instance.selectedCombineCard1 = null;
            GameManager.Instance.selectedCombineCard2 = null;
            GameManager.Instance.combineCardImageSelectedRed.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
            GameManager.Instance.combineCardImageSelectedBlue.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
            notification.SetActive(true);
            notification.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = selectedCardDetails.combineCardsProducesID.Count.ToString();

            cardCollected = false;
        }
        else
        {
            warning.SetActive(false);
            GameManager.Instance.player.getPenalty(180);
            if(DBManager.remaining_hours > 0)
                PenaltyPanel.SetActive(true);
            
        }
    }

    public void SelectCardChoice1() 
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        GameManager.Instance.choiceCombineCard1 = true;
        GameManager.Instance.choiceCombineCard2 = false;
        GameManager.Instance.listCardHolder.SetActive(true);
    }

    public void SelectCardChoice2()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        GameManager.Instance.choiceCombineCard2 = true;
        GameManager.Instance.choiceCombineCard1 = false;
        GameManager.Instance.listCardHolder.SetActive(true);
    }

    public void CombinedCardCollect()
    {
        if (!cardCollected)
        {
            GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
            notification.SetActive(false);
            foreach (string id in selectedCardDetails.combineCardsProducesID)
            {
                var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.deckCardHolder.transform);
                generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(id);
                if (generatedCard.transform.GetComponent<Card>().cardDetail.cardType == CardType.map)
                {
                    if (generatedCard.transform.GetComponent<Card>().cardDetail.cardID.Equals("D"))
                    {
                        sceneName = "Gudang_Scene";
                        isChangeScene = true;
                    }
                    if (generatedCard.transform.GetComponent<Card>().cardDetail.cardID.Equals("S"))
                    {
                        sceneName = "HallwayPenjara_Scene";
                        isChangeScene = true;
                    }
                    cardPanel.ChangePanel(generatedCard.GetComponent<Card>().cardDetail.mapIndex);
                    Destroy(generatedCard);
                }
                else
                {
                    GameManager.Instance.listCardHolder.GetComponent<ListCard>().AddCardToList(id);
                    DBManager.ownedCards.Add(id);
                    Player.instance.ownedCardId.Add(generatedCard.transform.GetComponent<Card>().cardDetail.cardID);
                }
            }
            cardCollected = true;
            GameManager.Instance.combineCardProducedImage.GetComponent<Image>().sprite = GameManager.Instance.resultCardHolder;
            if (isChangeScene)
                GameManager.Instance.ChangeScene(sceneName);
        }
    }

    public void SelectCardChoice()
    {
        GameManager.Instance.listCardHolder.SetActive(true);
    }

    public void OpenPanelCombine()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }

    public void OnDisable()
    {
        GameManager.Instance.warningCombine.SetActive(false);
        if(GameManager.Instance.selectedCombineCard2 != null)
            this.RemoveCard2FromHolder();
        if (GameManager.Instance.selectedCombineCard1 != null)
            this.RemoveCard1FromHolder();
    }
    private void Update()
    {
        if (GameManager.Instance.selectedCombineCard1 != null)
        {
            silangButton1.SetActive(true);
        }

        if (GameManager.Instance.selectedCombineCard2 != null)
        {
            silangButton2.SetActive(true);
        }
    }
}
