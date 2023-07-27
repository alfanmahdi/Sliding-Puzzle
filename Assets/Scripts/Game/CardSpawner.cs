using UnityEngine;
using UnityEngine.UI;

public class CardSpawner : MonoBehaviour
{
    public static CardSpawner instance;
    void Awake() { instance = this; }
    public Transform spawnRoots;
    public Transform discardRoots;
    public Transform redList;
    public Transform blueList;
    public Transform yellowList;
    public Transform greyList;
    public Transform greenList;
    public void SetSpawn(GameObject objToSpawn)
    {
        if (objToSpawn.GetComponent<Card>().cardDetail.cardType != CardType.map)
        {
            GameObject card = Instantiate(objToSpawn, spawnRoots);
            card.GetComponent<Button>().enabled = false;
        }
    }
    public void DestroyCard(string id)
    {
        GameObject objToDestroy = GetCardByID(id, spawnRoots);
        if(objToDestroy != null)
        {
            Instantiate(objToDestroy, discardRoots);
            Destroy(objToDestroy);
        }
        else
        {

        }
    }

    public GameObject GetCardByID(string cardID, Transform transform)
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Card>().cardDetail.cardID == cardID)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public GameObject GetCardListByID(string cardID, Transform transform)
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<CardChoice>().cardDetail.cardID == cardID)
            {
                return child.gameObject;
            }
        }
        return null;
    }
}
