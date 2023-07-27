using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;

public class R_BarisanBuku_PerpustakaanRahasia : MonoBehaviour
{
    [System.Serializable]
    public class ButtonPosition
    {
        public Button button;
        public Transform slot;
    }

    [System.Serializable]
    public class ButtonAnswer
    {
        public string buttonName;
        public Transform correctSlot;
    }

    public ButtonPosition[] startingButtonPositions;
    public Button[] buttons;
    public ButtonAnswer[] buttonAnswers;
    public GridLayoutGroup grid;
    public Button checkButton; // The button to trigger the answer check
    public Button resetButton; // The button to reset the books

    private Button selectedButton;
    private Transform selectedButtonParent;
    private Dictionary<Transform, string> slotToButtonNameMap;

    private CardDetailSO produceCardDetail;
    MapCardPanel cardPanel;
    
    public GameObject penaltyPanel;

    private void Awake()
    {
        SetStartingButtonPositions();
        CreateSlotToButtonNameMap();
        SetButtonAnswers();
        cardPanel = GameManager.Instance.mapPanel.GetComponent<MapCardPanel>();

        // Attach OnClick event handlers to each button
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Store the index in a local variable to avoid closure issues
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }

        // Attach OnClick event handler to the checkButton
        checkButton.onClick.AddListener(CheckAnswer);

        // Attach OnClick event handler to the resetButton
        resetButton.onClick.AddListener(ResetButtons);
    }

    private void SetStartingButtonPositions()
    {
        foreach (ButtonPosition buttonPosition in startingButtonPositions)
        {
            buttonPosition.button.transform.SetParent(buttonPosition.slot);
            buttonPosition.button.transform.localPosition = Vector3.zero;
        }
    }

    private void CreateSlotToButtonNameMap()
    {
        slotToButtonNameMap = new Dictionary<Transform, string>();

        foreach (ButtonAnswer buttonAnswer in buttonAnswers)
        {
            if (!slotToButtonNameMap.ContainsKey(buttonAnswer.correctSlot))
            {
                slotToButtonNameMap.Add(buttonAnswer.correctSlot, buttonAnswer.buttonName);
            }
        }
    }

    private void SetButtonAnswers()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => CheckButtonAnswer(button));
        }
    }

    private void CheckButtonAnswer(Button button)
    {
        string buttonName = button.name;

        foreach (KeyValuePair<Transform, string> pair in slotToButtonNameMap)
        {
            Transform correctSlot = pair.Key;
            string correctButtonName = pair.Value;

            if (IsButtonNameMatch(buttonName, correctButtonName) && correctSlot.childCount == 0)
            {
                button.transform.SetParent(correctSlot, false);
                // Debug.Log("Winner!");
                return;
            }
        }
    }

    private bool IsButtonNameMatch(string buttonName, string correctButtonName)
    {
        // Compare button names ignoring case and ignoring spaces
        return buttonName.Equals(correctButtonName, System.StringComparison.OrdinalIgnoreCase)
            || buttonName.Replace(" ", "").Equals(correctButtonName.Replace(" ", ""), System.StringComparison.OrdinalIgnoreCase);
    }

    private void OnButtonClick(int buttonIndex)
    {
        if (selectedButton == null)
        {
            // First click: Select the button
            selectedButton = buttons[buttonIndex];
            selectedButtonParent = selectedButton.transform.parent;
            selectedButton.interactable = false; // Disable the selected button temporarily

            // TODO: Apply a visual indicator or feedback to show the selected button
        }
        else if (selectedButton != buttons[buttonIndex])
        {
            // Second click on a different button: Swap the button positions
            Transform buttonParent = buttons[buttonIndex].transform.parent;
            Vector3 buttonPosition = buttons[buttonIndex].transform.localPosition;

            // Swap parents
            selectedButton.transform.SetParent(buttonParent);
            selectedButton.transform.localPosition = buttonPosition;

            buttons[buttonIndex].transform.SetParent(selectedButtonParent);
            buttons[buttonIndex].transform.localPosition = Vector3.zero;

            selectedButton.interactable = true; // Enable the previously selected button
            selectedButton = null;

            // Update button positions in the grid layout
            LayoutRebuilder.MarkLayoutForRebuild(selectedButtonParent.GetComponent<RectTransform>());
            LayoutRebuilder.MarkLayoutForRebuild(buttonParent.GetComponent<RectTransform>());

            // TODO: Update the visual indicators or feedback for the swapped buttons
        }
    }

    private void CheckAnswer()
    {
        // Create a list to store the names of the buttons in the correct order
        List<string> orderedButtonNames = new List<string>();

        // Iterate over the buttonAnswers array to get the correct order of button names
        foreach (ButtonAnswer buttonAnswer in buttonAnswers)
        {
            orderedButtonNames.Add(buttonAnswer.buttonName);
        }

        // Check if the buttons are in the correct order
        for (int i = 0; i < orderedButtonNames.Count; i++)
        {
            Transform correctSlot = buttonAnswers[i].correctSlot;
            string correctButtonName = orderedButtonNames[i];

            if (correctSlot.childCount > 0)
            {
                Button button = correctSlot.GetChild(0).GetComponent<Button>();
                string buttonName = button.name;

                if (!IsButtonNameMatch(buttonName, correctButtonName))
                {
                    // Debug.Log("Incorrect order!");
                    GameManager.Instance.player.getPenalty(180);
                    if(DBManager.remaining_hours>0)
                    penaltyPanel.SetActive(true);
                    ResetButtons();
                    return;
                }
            }
            else
            {
                // Debug.Log("Incomplete answer!");
                GameManager.Instance.player.getPenalty(180);
                if(DBManager.remaining_hours>0)
                penaltyPanel.SetActive(true);
                ResetButtons();
                return;
            }
        }

        // If all buttons are in the correct order, print "Winner!"
        // Debug.Log("Winner!");
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

    private void ResetButtons()
    {
        // Reset each button to its starting position
        foreach (ButtonPosition buttonPosition in startingButtonPositions)
        {
            buttonPosition.button.transform.SetParent(buttonPosition.slot);
            buttonPosition.button.transform.localPosition = Vector3.zero;
            buttonPosition.button.interactable = true;
        }

        // Clear the selected button
        selectedButton = null;
        selectedButtonParent = null;
    }
}


