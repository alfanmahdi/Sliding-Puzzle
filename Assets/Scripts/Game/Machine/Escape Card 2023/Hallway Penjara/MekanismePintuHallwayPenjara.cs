using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MekanismePintuHallwayPenjara : MonoBehaviour
{


    private CardDetailSO produceCardDetail;
    MapCardPanel cardPanel;
    
    public GameObject penaltyPanel;
    public Lamp[] lamps;
    public Stone[] switches;

    private string sceneName;

    public void Awake(){
        ShutAllLamps();
        cardPanel = GameManager.Instance.mapPanel.GetComponent<MapCardPanel>();
        sceneName = "Treasure_Room_Scene";
    }

    public void Submit(){
        int liveCount=0;
        foreach (var lamp in lamps)
        {
            if(lamps[0].CheckState()){
                liveCount++;
            }
        }
        if(liveCount==4){
            RightCombination();
        }
        else{
            FalseCombination();
        }
    }

    private void RightCombination(){
        // Debug.Log("Right Combination!!!");
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
            GameManager.Instance.ChangeScene(sceneName);
    }
    private void FalseCombination(){
        // Debug.Log("Wrong Combination");
    }

    private void ShutAllLamps(){
        foreach (var lamp in lamps)
        {
            lamp.SetLampState(false);
        }
    }

    public void HandleSwitchChanges(int index, bool isOn){
        // Debug.Log("Switch state changed. IsOn: " + isOn);
        if(index==0){
            // Debug.Log("called index 0");
            lamps[1].SetLampState(!isOn);
            lamps[2].SetLampState(isOn);
            lamps[3].SetLampState(isOn);
        }
        else if(index==1){
            // Debug.Log("called index 1");
            lamps[0].SetLampState(!isOn);
            lamps[1].SetLampState(isOn);
        }
        else if(index==2){
            // Debug.Log("called index 2");
            foreach (var lamp in lamps)
            {
                lamp.SetLampState(false);
            }
        }
        else if(index==3){
            // Debug.Log("called index 3");
            lamps[0].SetLampState(isOn);
        }
    }

    private void SetSwitchesOff(){
        foreach (var stone in switches)
        {
            stone.SetStateOff();
        }
    }

    public void Reset(){
        ShutAllLamps();
        SetSwitchesOff();
    }
}
