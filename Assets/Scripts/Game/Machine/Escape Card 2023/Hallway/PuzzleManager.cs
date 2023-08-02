using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public GameObject[] tiles;
    public TMPro.TextMeshProUGUI serialNumberText;

    private int[,] start_6a0h_1 = new int[,] { {  8,  2, 12,  9 }, 
                                               { 10,  6,  7,  0 }, 
                                               { 13,  1, 14,  5 },
                                               {  4,  3, 11, 15 } };

    private int[,] goal_6a0h_1 = new int[,] { { 5, 12, 9, 14 }, 
                                              { 8, 10, 0, 11 }, 
                                              { 6, 13, 7,  3 },
                                              { 1,  2, 4, 15 } };

    private int[,] start_6a0h_2 = new int[,] { {  5, 13, 12,  2 }, 
                                               {  3,  9, 10, 14 }, 
                                               {  4,  7,  0,  1 },
                                               { 11,  6,  8, 15 } };

    private int[,] goal_6a0h_2 = new int[,] { {  3, 10, 12, 14 }, 
                                              { 13,  0,  9,  1 }, 
                                              {  7,  4,  6,  8 },
                                              {  2, 11,  5, 15 } };

    private int[,] start_5a1h_1 = new int[,] { {  3, 13,  6,  7 }, 
                                               { 14, 11,  4,  9 }, 
                                               {  1, 12,  5,  8 },
                                               {  0, 10,  2, 15 } };

    private int[,] goal_5a1h_1 = new int[,] { {  0,  3,  2,  5 }, 
                                              {  4,  7,  6,  9 }, 
                                              {  8, 11, 10, 13 },
                                              { 12,  1, 14, 15 } };

    private int[,] start_5a1h_2 = new int[,] { {  9,  2, 12,  6 }, 
                                               {  8,  1, 14,  4 }, 
                                               {  5,  7, 13,  3 },
                                               {  0, 10, 11, 15 } };

    private int[,] goal_5a1h_2 = new int[,] { {  3,  4,  5,  6 }, 
                                              { 11, 12, 13, 14 }, 
                                              {  7,  8,  9, 10 },
                                              {  0,  1,  2, 15 } };

    private int[,] start_5a1h_3 = new int[,] { {  1,  0,  6,  5 }, 
                                               { 14,  9,  7,  8 }, 
                                               { 12,  4,  2, 13 },
                                               {  3, 11, 10, 15 } };

    private int[,] goal_5a1h_3 = new int[,] { {  6,  9, 12, 14 }, 
                                              {  3,  7, 10, 13 }, 
                                              {  1,  4, 11,  8 },
                                              {  0,  2,  5, 15 } };

    private int[,] start_4a2h_1 = new int[,] { { 12,  3, 14,  9 }, 
                                               {  2, 11,  5,  4 }, 
                                               { 13,  0,  6, 10 },
                                               {  7,  8,  1, 15 } };

    private int[,] goal_4a2h_1 = new int[,] { {  1,  6, 11,  7 }, 
                                              { 14, 13,  5,  4 }, 
                                              {  2,  3,  0,  8 },
                                              {  9, 10, 12, 15 } };

    private int[,] start_4a2h_2 = new int[,] { { 14, 11, 12,  1 }, 
                                               { 13,  2,  5,  6 }, 
                                               {  3,  0,  4,  8 },
                                               {  9, 10,  7, 15 } };

    private int[,] goal_4a2h_2 = new int[,] { { 12, 14,  2,  4 }, 
                                              { 10,  1, 13,  7 }, 
                                              { 11,  9,  5,  0 },
                                              {  6,  8,  3, 15 } };

    private int[,] start_4a2h_3 = new int[,] { { 12,  7,  8,  4 }, 
                                               {  2, 14,  9, 13 }, 
                                               {  5,  1,  6,  0 },
                                               { 10,  3, 11, 15 } };

    private int[,] goal_4a2h_3 = new int[,] { { 14,  4,  2,  8 }, 
                                              { 11,  5,  9,  6 }, 
                                              {  1,  3,  7, 10 },
                                              {  0, 13, 12, 15 } };

    private int[,] start_3a3h_1 = new int[,] { { 10, 14,  7,  5 }, 
                                               {  9,  3, 13,  2 }, 
                                               {  4, 11, 12,  1 },
                                               {  6,  8,  0, 15 } };

    private int[,] goal_3a3h_1 = new int[,] { {  9,  1, 10,  2 }, 
                                              {  5, 13,  7,  0 }, 
                                              {  6, 11,  4, 14 },
                                              {  8,  3, 12, 15 } };

    private int[,] start_3a3h_2 = new int[,] { {  7,  5,  6, 11 }, 
                                               { 10,  8,  0, 14 }, 
                                               {  2, 13,  1,  9 },
                                               {  4, 12,  3, 15 } };

    private int[,] goal_3a3h_2 = new int[,] { {  6,  7,  8,  9 }, 
                                              {  5,  4, 11, 10 }, 
                                              {  2,  3, 12, 14 },
                                              {  1,  0, 13, 15 } };
    
    private int[,] start_3a3h_3 = new int[,] { {  7, 14,  1,  0 }, 
                                               {  6, 13,  8,  2 }, 
                                               {  9, 10,  5, 12 },
                                               { 11,  4,  3, 15 } };

    private int[,] goal_3a3h_3 = new int[,] { {  5,  6,  7,  8 }, 
                                              {  4, 13, 14,  9 }, 
                                              {  3, 12, 11, 10 },
                                              {  2,  1,  0, 15 } };

    private Vector3[] initialPositions;
    private int emptyTileIndex = 15;
    private int[,] activeStart;
    private int[,] curr = new int[,] { {  0,  1,  2,  3 }, 
                                       {  4,  5,  6,  7 }, 
                                       {  8,  9, 10, 11 },
                                       { 12, 13, 14, 15 } };
    private int[,] activeGoal;

    private void Start() {
        // Generate serial number acak
        string generatedSerialNumber = GenerateRandomSerialNumber();

        // Tampilkan serial number di UI Text
        serialNumberText.text = "Serial Number: " + generatedSerialNumber;

        int digit = CountDigitsInString(generatedSerialNumber);

        if (digit == 6)
        {
            int lastDigit = int.Parse(generatedSerialNumber[generatedSerialNumber.Length - 1].ToString());

            // Angka paling belakang ganjil
            if (lastDigit % 2 == 1)
            {
                activeGoal = goal_6a0h_1;
                activeStart = start_6a0h_1;
            }
            // Angka paling belakang genap
            else
            {
                activeGoal = goal_6a0h_2;
                activeStart = start_6a0h_2;
            }
        } else if (digit == 5)
        {
            // Huruf berada paling depan
            if (char.IsLetter(generatedSerialNumber[0]))
            {
                activeGoal = goal_5a1h_1;
                activeStart = start_5a1h_1;
            }
            // Huruf berada paling belakang
            else if (char.IsLetter(generatedSerialNumber[5]))
            {
                activeGoal = goal_5a1h_2;
                activeStart = start_5a1h_2;
            }
            // Huruf tidak di paling depan atau paling belakang
            else
            {
                activeGoal = goal_5a1h_3;
                activeStart = start_5a1h_3;
            }
        } else if (digit == 4)
        {
            // Huruf berada paling belakang
            if (char.IsLetter(generatedSerialNumber[5]))
            {
                activeGoal = goal_4a2h_1;
                activeStart = start_4a2h_1;
            }
            else
            {
                int lastDigit = int.Parse(generatedSerialNumber[generatedSerialNumber.Length - 1].ToString());

                // Angka paling belakang ganjil
                if (lastDigit % 2 == 1)
                {
                    activeGoal = goal_4a2h_2;
                    activeStart = start_4a2h_2;
                }
                // Angka paling belakang genap
                else
                {
                    activeGoal = goal_4a2h_3;
                    activeStart = start_4a2h_3;
                }
            }
        } else if (digit == 3)
        {
            // Huruf berada paling belakang
            if (char.IsLetter(generatedSerialNumber[5]))
            {
                activeGoal = goal_3a3h_1;
                activeStart = start_3a3h_1;
            }
            else
            {
                int lastDigit = int.Parse(generatedSerialNumber[generatedSerialNumber.Length - 1].ToString());

                // Angka paling belakang ganjil
                if (lastDigit % 2 == 1)
                {
                    activeGoal = goal_3a3h_2;
                    activeStart = start_3a3h_2;
                }
                // Angka paling belakang genap
                else
                {
                    activeGoal = goal_3a3h_3;
                    activeStart = start_3a3h_3;
                }
            }
        }

        initialPositions = new Vector3[tiles.Length];
        for (int i = 0; i < tiles.Length; i++){
            RectTransform tileRectTransform = tiles[i].GetComponent<RectTransform>();
            initialPositions[i] = tileRectTransform.anchoredPosition;
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                SwapTilesByIndex(curr[i, j], activeStart[i, j]);
            }
        }

        SetAllTilesActive(true);
    }

    private string GenerateRandomSerialNumber()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        char[] serialNumberArray = new char[6];
        System.Random random = new System.Random();

        int digitCount = 0;
        while (digitCount < 3) // Pastikan setidaknya 3 angka ada di dalam serial number
        {
            for (int i = 0; i < serialNumberArray.Length; i++)
            {
                serialNumberArray[i] = chars[random.Next(chars.Length)];
            }
            digitCount = CountDigitsInString(new string(serialNumberArray));
        }

        return new string(serialNumberArray);
    }

    private int CountDigitsInString(string input)
    {
        int count = 0;
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                count++;
            }
        }
        return count;
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

    private bool IsAdjacent(int index1, int index2)
    {
        // Calculate row and column for each index
        int row1 = index1 / 4;
        int col1 = index1 % 4;

        int row2 = index2 / 4;
        int col2 = index2 % 4;

        // Check if the tiles are in adjacent rows and the same column or adjacent columns and the same row
        bool isAdjacentRow = (Mathf.Abs(row1 - row2) == 1) && (col1 == col2);
        bool isAdjacentColumn = (Mathf.Abs(col1 - col2) == 1) && (row1 == row2);

        // Check if the tiles are exactly one step away from each other (no diagonals)
        return (isAdjacentRow && !isAdjacentColumn) || (!isAdjacentRow && isAdjacentColumn);
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

    public void SwapTilesByIndex(int indexTile1, int indexTile2)
    {
        if (indexTile1 >= 0 && indexTile1 < gridLayoutGroup.transform.childCount &&
            indexTile2 >= 0 && indexTile2 < gridLayoutGroup.transform.childCount)
        {
            int pos1 = FindSiblingIndexInGrid(tiles[indexTile1]);
            int pos2 = FindSiblingIndexInGrid(tiles[indexTile2]);

            Transform tileTransform1 = gridLayoutGroup.transform.GetChild(pos1);
            Transform tileTransform2 = gridLayoutGroup.transform.GetChild(pos2);

            // Swap the positions of the two tiles using GridLayoutGroup
            int index1 = tileTransform1.GetSiblingIndex();
            int index2 = tileTransform2.GetSiblingIndex();

            tileTransform1.SetSiblingIndex(index2);
            tileTransform2.SetSiblingIndex(index1);

            int index3 = tileTransform1.GetSiblingIndex();
            int index4 = tileTransform2.GetSiblingIndex();

            // Update the tile indices in curr based on the new positions in the gridLayoutGroup
            int tileIndex1 = GetTilePosition(indexTile1);
            int row1 = tileIndex1 / 4;
            int col1 = tileIndex1 % 4;

            int tileIndex2 = GetTilePosition(indexTile2);
            int row2 = tileIndex2 / 4;
            int col2 = tileIndex2 % 4;

            int temp = curr[row1, col1];
            curr[row1, col1] = curr[row2, col2];
            curr[row2, col2] = temp;
        }
    }

    public int FindSiblingIndexInGrid(GameObject tile)
    {
        for (int i = 0; i < gridLayoutGroup.transform.childCount; i++)
        {
            if (gridLayoutGroup.transform.GetChild(i).gameObject == tile)
            {
                return i;
            }
        }
        return -1; // Jika tile tidak ditemukan, mengembalikan nilai -1 sebagai penanda kesalahan.
    }

    private bool CheckGoal()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (curr[i, j] != activeGoal[i, j])
                    return false;
            }
        }
        return true;
    }

    public void Submit(){

        if(CheckGoal()) {
            Debug.Log("BENER");
        } else {
            Debug.Log("SALAH");
        }
    }

    public void Reset(){
        // Reset keadaan curr berdasarkan start yang aktif
        for (int i = 0; i < activeGoal.GetLength(0); i++) {
            for (int j = 0; j < activeGoal.GetLength(1); j++) {
                curr[i, j] = activeStart[i, j];
            }
        }

        // Reset posisi tile ke posisi awal
        for (int i = 0; i < tiles.Length; i++) {
            tiles[i].transform.position = initialPositions[i];
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        SetAllTilesActive(false);
    }

    public void SetAllTilesActive(bool isActive)
    {
        foreach (GameObject tile in tiles)
        {
            tile.SetActive(isActive);
        }
    }
}