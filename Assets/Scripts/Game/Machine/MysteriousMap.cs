using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MysteriousMap : MonoBehaviour
{
    public GameObject arrowE;
    public GameObject arrowV;
    public GameObject arrowT;
    public GameObject arrowP;
    public GameObject arrowL;
    public GameObject arrowA;
    public GameObject arrowQ;
    public GameObject arrowH;
    public GameObject infoPanel;
    public GameObject warningInfo;
    public GameObject penaltyPanel;
    private CardDetailSO produceCardDetail;
    private TextMeshProUGUI textWarning;
    MapCardPanel cardPanel;
    private bool isChangeScene;
    private string sceneName;

    public void Start()
    {
        textWarning = warningInfo.GetComponent<TextMeshProUGUI>();
    }
    private void Awake()
    {
        cardPanel = GameManager.Instance.mapPanel.GetComponent<MapCardPanel>();
    }

    public void OnAndOffArrowE()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().mapMachineSoundPlay();
        if (!arrowE.activeInHierarchy)
        {
            if (arrowL.activeInHierarchy || arrowT.activeInHierarchy)
            {
                if (arrowL.activeInHierarchy)
                    textWarning.text = "Anda sekarang berada di jalur L, anda harus kembali ke tempat semula";
                else if(arrowT.activeInHierarchy)
                    textWarning.text = "Anda sekarang berada di jalur T, anda harus kembali ke tempat semula";
                warningInfo.SetActive(true);
            }
            else
                arrowE.SetActive(true);
        }
        else
        {
            warningInfo.SetActive(false);
            arrowE.SetActive(false);
            arrowV.SetActive(false);
        }
        infoPanel.SetActive(false);
    }
    public void OnAndOffArrowV()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().mapMachineSoundPlay();
        if (!arrowV.activeInHierarchy && arrowE.activeInHierarchy)
        {
            if (!arrowE.activeInHierarchy)
            {
                textWarning.text = "Anda harus melewati jalur E terlebih dahulu";
                warningInfo.SetActive(true);
            }
            else
                arrowV.SetActive(true);
        }
        else
            arrowV.SetActive(false);
    }
    public void OnAndOffArrowT()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().mapMachineSoundPlay();
        if (!arrowT.activeInHierarchy)
        {
            if (arrowL.activeInHierarchy || arrowE.activeInHierarchy)
            {
                if (arrowL.activeInHierarchy)
                    textWarning.text = "Anda sekarang berada di jalur L, anda harus kembali ke tempat semula";
                else if (arrowE.activeInHierarchy)
                    textWarning.text = "Anda sekarang berada di jalur E, anda harus kembali ke tempat semula";
                warningInfo.SetActive(true);
            }
            else
                arrowT.SetActive(true);
        }
        else
        {
            warningInfo.SetActive(false);
            arrowT.SetActive(false);
            arrowP.SetActive(false);
        }
        infoPanel.SetActive(false);
    }
    public void OnAndOffArrowP()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().mapMachineSoundPlay();
        if (!arrowP.activeInHierarchy)
        {
            if (!arrowT.activeInHierarchy)
            {
                textWarning.text = "Anda harus melewati jalur T terlebih dahulu";
                warningInfo.SetActive(true);
            }
            else
                arrowP.SetActive(true);
        }
        else
            arrowP.SetActive(false);
    }
    public void OnAndOffArrowL()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().mapMachineSoundPlay();
        if (!arrowL.activeInHierarchy)
        {
            if (arrowT.activeInHierarchy || arrowE.activeInHierarchy)
            {
                if (arrowT.activeInHierarchy)
                    textWarning.text = "Anda sekarang berada di jalur T, anda harus kembali ke tempat semula";
                else if (arrowE.activeInHierarchy)
                    textWarning.text = "Anda sekarang berada di jalur E, anda harus kembali ke tempat semula";
                warningInfo.SetActive(true);
            }
            else
                arrowL.SetActive(true);
        }
        else
        {
            warningInfo.SetActive(false);
            arrowL.SetActive(false);
            arrowA.SetActive(false);
            arrowQ.SetActive(false);
            arrowH.SetActive(false);
        }
        infoPanel.SetActive(false);
    }
    public void OnAndOffArrowA()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().mapMachineSoundPlay();
        if (!arrowA.activeInHierarchy)
        {
            if (!arrowL.activeInHierarchy)
            {
                textWarning.text = "Anda harus melewati jalur L terlebih dahulu";
                warningInfo.SetActive(true);
            }
            else
                arrowA.SetActive(true);
        }
        else
        {
            arrowA.SetActive(false);
            arrowQ.SetActive(false);
            arrowH.SetActive(false);
        }
    }
    public void OnAndOffArrowQ()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().mapMachineSoundPlay();
        if (!arrowQ.activeInHierarchy)
        {
            if (!arrowA.activeInHierarchy)
            {
                textWarning.text = "Anda harus melewati jalur A terlebih dahulu";
                warningInfo.SetActive(true);
            }
            else
                arrowQ.SetActive(true);
        }
        else
        {
            arrowQ.SetActive(false);
            arrowH.SetActive(false);
        }
    }
    public void OnAndOffArrowH()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().mapMachineSoundPlay();
        if (!arrowH.activeInHierarchy)
        {
            if (!arrowQ.activeInHierarchy)
            {
                textWarning.text = "Anda harus melewati jalur Q terlebih dahulu";
                warningInfo.SetActive(true);
            }
            else
                arrowH.SetActive(true);
        }
        else
            arrowH.SetActive(false);
    }

    public void Submit()
    {
        if (arrowL.activeInHierarchy && arrowA.activeInHierarchy &&
            arrowQ.activeInHierarchy && arrowH.activeInHierarchy)
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
                    if (produceCardDetail.cardID.Equals("H"))
                    {
                        sceneName = "Basement Scene";
                        isChangeScene = true;
                    }
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
            ResetArrow();
            if (isChangeScene)
                GameManager.Instance.ChangeScene(sceneName);
        }
        else
        {
            GameManager.Instance.player.getPenalty(180);
            if(DBManager.remaining_hours > 0)
                penaltyPanel.SetActive(true);
            ResetArrow();
        }
    }

    public void ResetArrow()
    {
        warningInfo.SetActive(false);
        arrowE.SetActive(false);
        arrowV.SetActive(false);
        arrowT.SetActive(false);
        arrowP.SetActive(false);
        arrowL.SetActive(false);
        arrowA.SetActive(false);
        arrowQ.SetActive(false);
        arrowH.SetActive(false);
    }
}
