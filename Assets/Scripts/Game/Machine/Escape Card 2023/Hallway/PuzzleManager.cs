using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public GameObject[] tiles;

    private int[,] start = new int[,] { { 0,  1,  2,  3 }, 
                                        { 4,  5,  6,  7 }, 
                                        { 8,  9,  10, 11 },
                                        { 12, 13, 14, 15 } };
    private int[,] curr = new int[,] { { 0,  1,  2,  3 }, 
                                       { 4,  5,  6,  7 }, 
                                       { 8,  9,  10, 11 },
                                       { 12, 13, 14, 15 } };
    private int[,] goal = new int[,] { { 2,  6,  12, 5 }, 
                                       { 11, 14, 7,  9 }, 
                                       { 4,  10, 1,  0 },
                                       { 3,  13, 8,  15 } };
    private Vector3[] initialPositions;
    private int emptyTileIndex = 15;

    private void Start() {
        initialPositions = new Vector3[tiles.Length];
        for (int i = 0; i < tiles.Length; i++){
            RectTransform tileRectTransform = tiles[i].GetComponent<RectTransform>();
            initialPositions[i] = tileRectTransform.anchoredPosition;
        }

    }

    private int GetTilePosition(int tileIndex){
        int sum = 0;
        for (int i = 0; i < 4; i++){
            for (int j = 0; j < 4; j++){
                if (curr[i, j] == tileIndex) return sum;
                else sum++;
            }
        }
        return sum;
    }

    public void SwapTiles(int tileIndex)
    {
        // Check if the clicked tile can be swapped with the empty tile
        if (IsAdjacent(GetTilePosition(tileIndex), GetTilePosition(emptyTileIndex)))
        {
            // Swap the positions of the clicked tile and the empty tile
            Vector3 tempPosition = tiles[tileIndex].transform.position;
            tiles[tileIndex].transform.position = tiles[emptyTileIndex].transform.position;
            tiles[emptyTileIndex].transform.position = tempPosition;

            // Update the tile indices
            int tileIndex1 = GetTilePosition(tileIndex);
            int row1 = tileIndex1 / 4;
            int col1 = tileIndex1 % 4;


            int tileIndex2 = GetTilePosition(emptyTileIndex);
            int row2 = tileIndex2 / 4;
            int col2 = tileIndex2 % 4;

            int temp = curr[row1, col1];
            curr[row1, col1] = curr[row2, col2];
            curr[row2, col2] = temp;
        }
    }

    private bool IsAdjacent(int index1, int index2){
        // Calculate row and column for each index
        int row1 = index1 / 4;
        int col1 = index1 % 4;

        int row2 = index2 / 4;
        int col2 = index2 % 4;

        // Check if the tiles are in adjacent rows and the same column or adjacent columns and the same row
        bool isAdjacentRow = (Mathf.Abs(row1 - row2) == 1) && (col1 == col2);
        bool isAdjacentColumn = (Mathf.Abs(col1 - col2) == 1) && (row1 == row2);

        return isAdjacentRow || isAdjacentColumn;
    }

    private bool IsCorrect(){
        for (int i = 0; i < 4; i++){
            for (int j = 0; j < 4; j++){
                if (curr[i, j] != goal[i, j]) return false;
            }
        }
        return true;
    }

    public void Submit(){

        if(IsCorrect()) {
            Debug.Log("BENER");
        } else {
            Debug.Log("SALAH");
        }

        /*
        if(IsCorrect()) {
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
        
        }
        else
        {
            GameManager.Instance.player.getPenalty(180);
            if(DBManager.remaining_hours>0)
                penaltyPanel.SetActive(true);
        } */
    }

    public void Reset(){
        // GameManager.Instance.audioManager.GetComponent<SoundManager>().switchMachineSoundPlay();

        for (int i = 0; i < goal.GetLength(0); i++){
            for (int j = 0; j < goal.GetLength(1); j++){
                curr[i, j] = start[i, j];
            }
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
