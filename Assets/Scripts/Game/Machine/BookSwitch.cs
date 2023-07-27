using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookSwitch : MonoBehaviour
{
    public GameObject on;
    public GameObject off;
    public TextMeshProUGUI sign;
    public GameObject penaltyPanel;
    private CardDetailSO produceCardDetail;
    MapCardPanel cardPanel;
    private bool isChangeScene;
    private string sceneName;
    private void Awake()
    {
        cardPanel = GameManager.Instance.mapPanel.GetComponent<MapCardPanel>();
    }
    public void OnAndOff()
    {
        if(on.activeInHierarchy)
        {
            sign.text = "OFF";

            on.SetActive(false);
            off.SetActive(true);
            GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        }
        else
        {
            GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
            sign.text = "ON";
            off.SetActive(false);
            on.SetActive(true);
        }
    }
    public void Submit()
    {
        if(on.activeInHierarchy && !off.activeInHierarchy)
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
                    if (produceCardDetail.cardID.Equals("A"))
                    {
                        sceneName = "Kidnap Scene";
                        isChangeScene = true;
                    }
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

            if (isChangeScene)
                GameManager.Instance.ChangeScene(sceneName);
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
        sign.text = "OFF";
        off.SetActive(true);
        on.SetActive(false);
    }
}
