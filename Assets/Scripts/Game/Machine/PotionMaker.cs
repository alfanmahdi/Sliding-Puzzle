using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionMaker : MonoBehaviour
{
    public GameObject PotionOneIcon;
    public GameObject PotionTwoIcon;
    public GameObject PotionThreeIcon;
    public GameObject PotionFourIcon;
    public GameObject PotionFiveIcon;
    [SerializeField] private TMP_InputField PotionOneField;
    [SerializeField] private TMP_InputField PotionTwoField;
    [SerializeField] private TMP_InputField PotionThreeField;
    [SerializeField] private TMP_InputField PotionFourField;
    [SerializeField] private TMP_InputField PotionFiveField;
    public GameObject infoPanel;
    public GameObject penaltyPanel;
    private CardDetailSO produceCardDetail;
    
    public void OnAndOffPotionOneButton()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!PotionOneIcon.activeInHierarchy)
            PotionOneIcon.SetActive(true);
        else
            PotionOneIcon.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void OnAndOffPotionTwoButton()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!PotionTwoIcon.activeInHierarchy)
            PotionTwoIcon.SetActive(true);
        else
            PotionTwoIcon.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void OnAndOffPotionThreeButton()
    {
       GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!PotionThreeIcon.activeInHierarchy)
            PotionThreeIcon.SetActive(true);
        else
            PotionThreeIcon.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void OnAndOffPotionFourButton()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!PotionFourIcon.activeInHierarchy)
            PotionFourIcon.SetActive(true);
        else
            PotionFourIcon.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void OnAndOffPotionFiveButton()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        if (!PotionFiveIcon.activeInHierarchy)
            PotionFiveIcon.SetActive(true);
        else
            PotionFiveIcon.SetActive(false);
        infoPanel.SetActive(false);
    }
    public void Submit()
    {
        if(PotionOneIcon.activeInHierarchy && !PotionTwoIcon.activeInHierarchy &&
            PotionThreeIcon.activeInHierarchy && !PotionFourIcon.activeInHierarchy &&
            PotionFiveIcon.activeInHierarchy && PotionOneField.text.Equals("2 KG") &&
            PotionThreeField.text.Equals("2 KG") && PotionFiveField.text.Equals("3 KG"))
        {
            GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
            produceCardDetail = GameManager.Instance.GetCardDetailByID(GameManager.Instance.selectedMachineCard.unlockCardProducesID[0]);

            foreach (string id in GameManager.Instance.selectedMachineCard.unlockCardProducesID)
            {
                produceCardDetail = GameManager.Instance.GetCardDetailByID(id);
                var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.deckCardHolder.transform);
                generatedCard.transform.GetComponent<Card>().cardDetail = produceCardDetail;
                generatedCard.transform.GetComponent<Image>().sprite = produceCardDetail.cardSprite;
                Player.instance.ownedCardId.Add(generatedCard.transform.GetComponent<Card>().cardDetail.cardID);
                DBManager.ownedCards.Add(id);
                GameManager.Instance.listCardHolder.GetComponent<ListCard>().AddCardToList(produceCardDetail.cardID);
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
            if (DBManager.remaining_hours > 0)
                penaltyPanel.SetActive(true);
            ResetButton();
        }
    }

    public void ResetButton()
    {
        PotionOneIcon.SetActive(false);
        PotionTwoIcon.SetActive(false);
        PotionThreeIcon.SetActive(false);
        PotionFourIcon.SetActive(false);
        PotionFiveIcon.SetActive(false);
        PotionOneField.text = "";
        PotionTwoField.text = "";
        PotionThreeField.text = "";
        PotionFourField.text = "";
        PotionFiveField.text = "";
        infoPanel.SetActive(true);
    }
}
