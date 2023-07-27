using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    public Button button;
    public int tileIndex;
    public PuzzleManager puzzleManager;

    private void Start()
    {
        button.onClick.AddListener(SwapTile);
    }

    private void SwapTile()
    {
        puzzleManager.SwapTiles(tileIndex);
    }
}
