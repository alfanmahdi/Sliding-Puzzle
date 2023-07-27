using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCardPanel : MonoBehaviour
{
    public GameObject[] listmap;

    public void OpenPanelMap()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }
    public void ChangePanel(int index)
    {
        listmap[Player.instance.mapIndex].SetActive(false);
        DBManager.mapID = index;
        Player.instance.mapIndex = index;

        listmap[Player.instance.mapIndex].SetActive(true);
        switch (Player.instance.mapIndex)
        {
            case 0:
                GameManager.Instance.activeMap = ActiveMap.Hallway;
                break;
            case 1:
                GameManager.Instance.activeMap = ActiveMap.Gudang;
                break;
            case 2:
                GameManager.Instance.activeMap = ActiveMap.HerbRoom;
                break;
            case 3:
                GameManager.Instance.activeMap = ActiveMap.PerpustakaanRahasia;
                break;
            case 4:
                GameManager.Instance.activeMap = ActiveMap.Penjara;
                break;
            case 5:
                GameManager.Instance.activeMap = ActiveMap.HallwayPenjara;
                break;
            case 6:
                GameManager.Instance.activeMap = ActiveMap.TreasureRoom;
                break;
            case 7:
                GameManager.Instance.activeMap = ActiveMap.KamarElsi;
                break;
        }
    }
}
