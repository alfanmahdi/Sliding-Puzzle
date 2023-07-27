using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Safebox : MonoBehaviour
{
    public TextMeshProUGUI digitalText;
    public GameObject penaltyPanel;
    private CardDetailSO produceCardDetail;
    MapCardPanel cardPanel;
    private void Awake()
    {
        cardPanel = GameManager.Instance.mapPanel.GetComponent<MapCardPanel>();
    }
    public void EnterButton(string text)
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().mapMachineSoundPlay();
        if (digitalText.text.Length < 4)
            digitalText.text += text;
    }
    public void Submit()
    {
        if(digitalText.text == "2022")
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
            GameManager.Instance.machineCardPanel.GetComponent<MachineCardPanelTutor>().RemoveCardFromHolder();
        }
        else
        {
            GameManager.Instance.player.getPenalty(180);
            if(DBManager.remaining_hours>0)
                penaltyPanel.SetActive(true);
            ResetButton();
        }
    }
    public void DeleteButton()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().mapMachineSoundPlay();
        if (digitalText.text.Length > 0)
        {
            string text = digitalText.text.ToString();
            string finalText = "";
            for(int i = 0; i < text.Length - 1; i++)
                finalText += text[i];
            digitalText.text = finalText;
        }    
    }

    public void ResetButton()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().mapMachineSoundPlay();
        digitalText.text = "";
    }
}
