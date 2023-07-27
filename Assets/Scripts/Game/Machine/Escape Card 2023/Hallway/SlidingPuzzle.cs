using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlidingPuzzle : MonoBehaviour
{
    [SerializeField] private Transform emptySpace = null;
    private Camera _camera;
    [SerializeField] private TilesScript[] tiles;
    private TilesScript[] firstPositions = new TilesScript[16];
    public GameObject penaltyPanel;
    private CardDetailSO produceCardDetail;
    MapCardPanel cardPanel;

    void Awake()
    {
        _camera = Camera.main;

        cardPanel = GameManager.Instance.mapPanel.GetComponent<MapCardPanel>();

        for (int i = 0; i < tiles.Length; i++)
        {
            firstPositions[i] = tiles[i];
            firstPositions[i].transform.position = tiles[i].transform.position;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

                if (Vector3.Distance(clickedObject.transform.position, emptySpace.position) < 100f)
                {
                    // find the exact index tile that was clicked and index tile that is empty
                    int clickedIndex = 0;
                    int emptyIndex = 0;

                    for (int i = 0; i < tiles.Length; i++)
                    {
                        if (clickedObject.transform.position == tiles[i].transform.position)
                        {
                            clickedIndex = i;
                        }

                        if (emptySpace.position == tiles[i].transform.position)
                        {
                            emptyIndex = i;
                        }
                    }

                    // swap the target position of the clicked tile and the empty tile
                    Vector3 temp = tiles[clickedIndex].targetPosition;
                    tiles[clickedIndex].targetPosition = tiles[emptyIndex].targetPosition;
                    tiles[emptyIndex].targetPosition = temp;
                    var temp2 = tiles[clickedIndex];
                    tiles[clickedIndex] = tiles[emptyIndex];
                    tiles[emptyIndex] = temp2;
                }
            }
        }
    }


    public void Submit()
    {

        if(tiles[0] == firstPositions[2] &&
            tiles[1] == firstPositions[6] &&
            tiles[2] == firstPositions[12] &&
            tiles[3] == firstPositions[5] &&
            tiles[4] == firstPositions[11] &&
            tiles[5] == firstPositions[14] &&
            tiles[6] == firstPositions[7] &&
            tiles[7] == firstPositions[9] &&
            tiles[8] == firstPositions[4] &&
            tiles[9] == firstPositions[10] &&
            tiles[10] == firstPositions[1] &&
            tiles[11] == firstPositions[0] &&
            tiles[12] == firstPositions[3] &&
            tiles[13] == firstPositions[13] &&
            tiles[14] == firstPositions[8])
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
            Reset();
        
        }
        else
        {
            GameManager.Instance.player.getPenalty(180);
            if(DBManager.remaining_hours>0)
                penaltyPanel.SetActive(true);
            // Reset();
        }
    }

    public void Reset()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();
        for (int i = 0; i < tiles.Length; i++)
        {
            while(tiles[i] != firstPositions[i])
            {
                for(int j = 0; j < tiles.Length; j++)
                {
                    if(tiles[j] == firstPositions[i])
                    {
                        Vector3 temp = tiles[i].targetPosition;
                        tiles[i].targetPosition = tiles[j].targetPosition;
                        tiles[j].targetPosition = temp;
                        var temp2 = tiles[i];
                        tiles[i] = tiles[j];
                        tiles[j] = temp2;
                    }
                }
            }
        }
    }
}
