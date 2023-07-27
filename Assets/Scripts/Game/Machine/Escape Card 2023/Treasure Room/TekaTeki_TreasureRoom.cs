using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class TekaTeki_TreasureRoom : MonoBehaviour
{ 
    private string digitalText = null;
    private const string code = "14";
    public TextMeshProUGUI uiText = null;

    private CardDetailSO produceCardDetail;
    MapCardPanel cardPanel;
    
    public GameObject penaltyPanel;

    public void EnterButton(string text) {
      // Debug.Log(text);
      if(digitalText == null) {
        digitalText = text;
      }
      else if (digitalText != null && digitalText.Length < 2) {
        digitalText += text;
      }
      uiText.text = digitalText;
      // Debug.Log(uiText.text);
    } 

    public void Reset() {
      digitalText = null;
      uiText.text = digitalText;
    }

    public void Submit() {
      // Debug.Log(digitalText);
      if(digitalText == code) {
        // Debug.Log("Benar");
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
      }
      else {
        // Debug.Log("Salah");
        GameManager.Instance.player.getPenalty(180);
        if(DBManager.remaining_hours>0)
          penaltyPanel.SetActive(true);
        Reset();
      }
    }

    public void Clicky()
    {
      AudioSource audioSource = GetComponent<AudioSource>();
      if (audioSource != null)
      {
          audioSource.Play();
      }
	  }
}
