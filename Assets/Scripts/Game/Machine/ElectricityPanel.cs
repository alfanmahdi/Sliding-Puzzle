using UnityEngine;
using UnityEngine.UI;

public class ElectricityPanel : MonoBehaviour
{
    public GameObject onRedButton;
    public GameObject onBrownButton;
    public GameObject onBlueButton;
    public GameObject onBlackButton;
    public GameObject onGreenButton;
    public GameObject onYellowButton;
    public GameObject infoPanel;
    public GameObject penaltyPanel;
    private CardDetailSO produceCardDetail;
    MapCardPanel cardPanel;

    private void Awake()
    {
        cardPanel = GameManager.Instance.mapPanel.GetComponent<MapCardPanel>();
    }

    public void OnAndOffRedButton()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!onRedButton.activeInHierarchy)
            onRedButton.SetActive(true);
        else
            onRedButton.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void OnAndOffBrownButton()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!onBrownButton.activeInHierarchy)
            onBrownButton.SetActive(true);
        else
            onBrownButton.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void OnAndOffBlueButton()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!onBlueButton.activeInHierarchy)
            onBlueButton.SetActive(true);
        else
            onBlueButton.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void OnAndOffBlackButton()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!onBlackButton.activeInHierarchy)
            onBlackButton.SetActive(true);
        else
            onBlackButton.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void OnAndOffGreenButton()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!onGreenButton.activeInHierarchy)
            onGreenButton.SetActive(true);
        else
            onGreenButton.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void OnAndOffYellowButton()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!onYellowButton.activeInHierarchy)
            onYellowButton.SetActive(true);
        else
            onYellowButton.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void Submit()
    {
        if (onRedButton.activeInHierarchy && !onBrownButton.activeInHierarchy &&
            onBlueButton.activeInHierarchy && !onBlackButton.activeInHierarchy &&
            onGreenButton.activeInHierarchy && onYellowButton.activeInHierarchy)
        {
            GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
            produceCardDetail = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedMachineCard.unlockCardProducesID[0]);

            foreach (string id in GameManager.Instance.selectedMachineCard.unlockCardProducesID)
            {
                produceCardDetail = GameManager.Instance.GetCardDetailByID(id);
                var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.deckCardHolder.transform);
                generatedCard.transform.GetComponent<Card>().cardDetail = produceCardDetail;
                generatedCard.transform.GetComponent<Image>().sprite = produceCardDetail.cardSprite;

                if (produceCardDetail.cardType == CardType.map)
                {
                    cardPanel.ChangePanel(produceCardDetail.mapIndex);
                    Destroy(generatedCard);
                }
                else
                {
                    Player.instance.ownedCardId.Add(generatedCard.transform.GetComponent<Card>().cardDetail.cardID);
                    DBManager.ownedCards.Add(id);
                    GameManager.Instance.listCardHolder.GetComponent<ListCard>().AddCardToList(produceCardDetail.cardID);
                }
            }

            GameManager.Instance.machineCardPanel.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            foreach (string id in produceCardDetail.destroyedCardID)
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
            GameManager.Instance.machineCardPanel.GetComponent<MachineCardPanel>().RemoveCardFromHolder();
            ResetButton();
        }
        else
        {
            GameManager.Instance.player.getPenalty(180);
            if(DBManager.remaining_hours>0)
                penaltyPanel.SetActive(true);
            ResetButton();
        }
    }
    public void ResetButton()
    {
        onRedButton.SetActive(false);
        onBrownButton.SetActive(false);
        onBlueButton.SetActive(false);
        onBlackButton.SetActive(false);
        onGreenButton.SetActive(false);
        onYellowButton.SetActive(false);
        infoPanel.SetActive(true);
    }
}
