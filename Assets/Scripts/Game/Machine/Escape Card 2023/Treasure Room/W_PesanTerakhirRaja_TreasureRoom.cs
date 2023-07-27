using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class W_PesanTerakhirRaja_TreasureRoom : MonoBehaviour
{
    public Button[] numberButtons;
    public Button buttonSubmit;
    public AudioSource buttonClick;
    public TextMeshProUGUI displayNumber;
    public string inputtedNumber = "";
    private CardDetailSO produceCardDetail;
    MapCardPanel cardPanel;
    public GameObject penaltyPanel;

    void Start()
    {
        for (int i = 0; i < numberButtons.Length; i++)
        {
            Button button = numberButtons[i];
            button.interactable = true;
            int number = i;
            button.onClick.AddListener(() => AppendNumber(number));
        }

        buttonSubmit.interactable = true;
        buttonSubmit.onClick.AddListener(SubmitAnswer);
    }

    void AppendNumber(int number)
    {
        ButtonClickSoundEffect();
        
        if (inputtedNumber.Length == 0 && number == 0)
        {
            return;
        }

        if (inputtedNumber.Length < 2)
        {
            inputtedNumber += number.ToString();
        }
        else if (inputtedNumber.Length == 2)
        {
            int tempNumber = int.Parse(inputtedNumber + number.ToString());
            if (tempNumber >= 1 && tempNumber <= 100)
            {
                inputtedNumber += number.ToString();
            }
        }
        
        DisplayInputtedNumber();
    }

    void SubmitAnswer()
    {
        ButtonClickSoundEffect();

        if (inputtedNumber == "64")
        {
            // Debug.Log("Correct!");

            foreach (Button button in numberButtons)
            {
                button.interactable = false;
            }
            buttonSubmit.interactable = false;
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
                if(id == "Y"){
                    GameManager.Instance.machineCardPanel.GetComponent<MachineCardPanel>().RemoveCardFromHolder();
                }
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
        }
        else
        {
            // Debug.Log("Wrong Answer!");
            GameManager.Instance.player.getPenalty(180);
            if(DBManager.remaining_hours>0)
                penaltyPanel.SetActive(true);
            ResetAnswer();
        }
    }

    void ResetAnswer()
    {
        inputtedNumber = "";
        DisplayInputtedNumber();
    }

    void ButtonClickSoundEffect()
    {
        buttonClick.Play();
    }

    void DisplayInputtedNumber()
    {
        displayNumber.text = inputtedNumber;
    }
}
