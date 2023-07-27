using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResource : MonoBehaviour
{
    private static GameResource instance;

    public static GameResource Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResource>("GameResource");
            }
            return instance;
        }
    }

    

    #region Header UI
    [Space(10)]
    [Header("UI")]
    #endregion
    #region Tooltip
    [Tooltip("Card Detail Panel")]
    #endregion
    public GameObject detailPanel;

    public GameObject cardChoice;

    public GameObject card;

}
