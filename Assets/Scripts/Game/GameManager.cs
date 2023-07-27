using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : SingletonMonobehaviour<GameManager>
{
    // Dependencies References

    // General reference
    #region Header General References
    [Space(10)]
    [Header("General References")]
    #endregion
    public Player player;
    public Canvas canvas;
    public Sprite cardHolder;
    public Sprite resultCardHolder;
    public List<CardDetailSO> allCardDetailList;
    public Transform panelTransform;
    public GameObject audioManager;
    public bool isTutorial;
    public bool isPenjelasan = false;

    [HideInInspector] public CardDetailSO selectedCardHidden;
    [HideInInspector] public CardDetailSO selectedCardHint;
    [HideInInspector] public CardDetailSO selectedCardUnlock;
    [HideInInspector] public CardDetailSO selectedCombineCard1;
    [HideInInspector] public CardDetailSO selectedCombineCard2;
    [HideInInspector] public CardDetailSO selectedMachineCard;
    [HideInInspector] public bool choiceCombineCard1 = false;
    [HideInInspector] public bool choiceCombineCard2 = false;

    // Keeps track which panel open
    [HideInInspector] public ActivePanel activePanel = ActivePanel.main;
    [HideInInspector] public ActiveMap activeMap = ActiveMap.Hallway;

    // Each Panel reference
    #region Header Panel References
    [Space(10)]
    [Header("Panels References")]
    #endregion
    public GameObject hiddenCardPanel;
    public GameObject unlockCardPanel;
    public GameObject hintCardPanel;
    public GameObject combineCardPanel;
    public GameObject mapPanel;
    public GameObject machineCardPanel;
    public GameObject cardDetailPanel;

    public Sprite backGroundPanel;
    public GameObject penaltyPanel;
    public GameObject winPanel;

    #region Selected Card References
    [Space(10)]
    [Header("Selected Card References")]
    #endregion
    public GameObject hiddenCardImageSelected;
    public GameObject unlockCardImageSelected;
    public GameObject combineCardImageSelectedRed;
    public GameObject combineCardImageSelectedBlue;
    public GameObject combineCardProducedImage;
    public GameObject hintCardImageSelected;
    public GameObject machineCardImageSelected;

    #region Header Placeholder Card References
    [Space(10)]
    [Header("Placeholder Card References")]
    #endregion
    public GameObject deckCardHolder;
    public GameObject listCardHolder;

    #region Warning References
    [Space(10)]
    [Header("Warning References")]
    #endregion
    public GameObject warningCombine;
    public GameObject warningUnlock;
    public GameObject warningHidden;

    #region Header Card Type Panel Settings
    [Space(10)]
    [Header("Card Type Per Panel")]
    #endregion
    public CardType hiddenCardType;
    public CardType unlockCardType;
    public CardType combineCardType1;
    public CardType combineCardType2;
    public CardType machineCardType;

    public void Start()
    {
        player.Init();
    }

    public CardDetailSO GetCardDetailByID(string cardID)
    {
        foreach(CardDetailSO cardDetail in allCardDetailList)
        {
            if(cardDetail.cardID == cardID)
            {
                return cardDetail;
            }
        }

        return null;
    }

    // Existing Card
    public GameObject GetCardByID(string cardID)
    {
        foreach(Transform child in deckCardHolder.transform)
        {
            if(child.GetComponent<Card>().cardDetail.cardID == cardID)
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

    public void CloseAllPanel()
    {
        hiddenCardPanel.SetActive(false);
        unlockCardPanel.SetActive(false);
        combineCardPanel.SetActive(false);
        machineCardPanel.SetActive(false);
        mapPanel.SetActive(false);
    }

    public void ChangeScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
