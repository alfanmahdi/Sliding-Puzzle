using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Y_PintuGudang_Gudang : MonoBehaviour
{
    public GameObject innerRing;
    public GameObject outerRing;
    private CardDetailSO produceCardDetail;
    MapCardPanel cardPanel;
    private bool isChangeScene;
    private string sceneName;
    public GameObject penaltyPanel;

    private void Awake()
    {
        cardPanel = GameManager.Instance.mapPanel.GetComponent<MapCardPanel>();
        sceneName = "HerbRoom_Scene";
        isChangeScene = true;
    }

    public void RotateInnerRing()
    {
        innerRing.transform.Rotate(Vector3.forward, 45f);
    }

    public void RotateOuterRing()
    {
        outerRing.transform.Rotate(Vector3.forward, 45f);
    }

    public void Submit()
    {
        if (Mathf.Round(innerRing.transform.localEulerAngles.z) == 90 && Mathf.Round(outerRing.transform.localEulerAngles.z) == 225)
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

            if (isChangeScene)
            {
                GameManager.Instance.ChangeScene(sceneName);
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
            
        }
        else
        {
            GameManager.Instance.player.getPenalty(180);
            if(DBManager.remaining_hours>0)
                penaltyPanel.SetActive(true);
            Reset();
        }
    }

    public void Reset()
    {
        innerRing.transform.rotation = Quaternion.identity;
        outerRing.transform.rotation = Quaternion.identity;
    }

}
