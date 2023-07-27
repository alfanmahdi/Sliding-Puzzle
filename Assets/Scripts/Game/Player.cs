using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    [SerializeField] private CoinSystem coinUI;
    [SerializeField] private TimeSystem timeUI;
    [SerializeField] public DiscardSystem discUI;
    [SerializeField] private TeamUI teamUI;
    [SerializeField] private ListCard listCard;

    public List<string> ownedCardId;

    public bool isFirstWinPanel = false;

    private string teamName;
    public int mapIndex;
    public int currentCoin { get; set; }
    public float currentTime { get; set; }
    public int currentDiscard;
    public int status { get; set; }

    public GameObject timeOut;
    public int score { get; set; }
    public GameObject map;
    MapCardPanel mapCardPanel;
    public void Awake(){
        instance = this;
    }
    public void Init()
    {
        if(DBManager.status == 1 && !GameManager.Instance.isPenjelasan)
        {
            GameManager.Instance.winPanel.SetActive(true);
        }else if (DBManager.status == -1 && !GameManager.Instance.isPenjelasan)
        {
            timeOut.SetActive(true);
        }
        else
        {
            teamName = DBManager.team_name;
            mapIndex = DBManager.mapID;
            score = DBManager.scores;
            
            currentCoin = DBManager.remaining_coins;
            currentDiscard = DBManager.discardCardsCount;
            currentTime = DBManager.remaining_hours;

            mapCardPanel = map.GetComponent<MapCardPanel>();
            mapCardPanel.ChangePanel(mapIndex);
            
            teamUI.SetName(teamName);
            timeUI.SetTime(currentTime);
            coinUI.SetCoin(currentCoin);
            discUI.SetDiscard(currentDiscard);
            if(GameManager.Instance.isPenjelasan)
            {
                ownedCardId.Add("16");
                ownedCardId.Add("32");
            }
            else
            {
                foreach (string id in DBManager.ownedCards)
                    ownedCardId.Add(id);
            }
            if (currentTime > 0)
            {
                foreach (string id in ownedCardId)
                {
                    var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.deckCardHolder.transform);
                    generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(id);
                    GameManager.Instance.listCardHolder.GetComponent<ListCard>().AddCardToList(id);
                }
            }
            else if(!GameManager.Instance.isPenjelasan)
            {
                if (GameManager.Instance.penaltyPanel.activeInHierarchy)
                    GameManager.Instance.penaltyPanel.SetActive(false);
                timeOut.SetActive(true);
                currentTime = 0;
                DBManager.remaining_hours = currentTime;
            }
            else
            {
                ownedCardId.Add("16");
                ownedCardId.Add("32");

                foreach (string id in ownedCardId)
                {
                    var generatedCard = Instantiate(GameResource.Instance.card, GameManager.Instance.deckCardHolder.transform);
                    generatedCard.transform.GetComponent<Card>().cardDetail = GameManager.Instance.GetCardDetailByID(id);
                    GameManager.Instance.listCardHolder.GetComponent<ListCard>().AddCardToList(id);
                }
            }
        }
    }
    void Update()
    {

        if (DBManager.status == 0 || this.status == 0 && currentTime > 0 && !GameManager.Instance.isPenjelasan)
        {
            if(currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                DBManager.remaining_hours = currentTime;
                timeUI.SetTime(currentTime);
            }

            //penalty
            if (currentTime <= 0)
            {
                if (GameManager.Instance.penaltyPanel.activeInHierarchy)
                    GameManager.Instance.penaltyPanel.SetActive(false);
                currentTime = 0;
                DBManager.remaining_hours = currentTime;
                timeOut.SetActive(true);
            }
        }
        else if(this.status == 1 && !GameManager.Instance.isPenjelasan)
        {
            GameManager.Instance.winPanel.SetActive(true);
        }
    }

    public bool getPenalty(int time)
    {
        currentTime -= time;
        if(currentTime <= 0)
        {
            timeOut.SetActive(true);
            currentTime = 0;
            DBManager.remaining_hours = currentTime;
            return true;
        }
        timeUI.SetTime(currentTime);
        return false;
    }

    public bool UseCoin(int coin)
    {
        if(coin <= currentCoin)
        {
            currentCoin -= coin;
            DBManager.remaining_coins = currentCoin;
            coinUI.SetCoin(currentCoin);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetCoin(int coin)
    {
        currentCoin += coin;
        coinUI.SetCoin(currentCoin);
    }
}
