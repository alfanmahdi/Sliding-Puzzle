using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class CardMapDetail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
{


    [SerializeField] private string id;

    // Update is called once per frame
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale += new Vector3(0.2f, 0.2f, 0f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale -= new Vector3(0.2f, 0.2f, 0f);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        CardDetailSO cardDetail = GameManager.Instance.GetCardDetailByID(id);
        var cardPanel = Instantiate(GameResource.Instance.detailPanel, GameManager.Instance.panelTransform);
        cardPanel.transform.GetChild(1).GetComponent<Image>().sprite = cardDetail.cardSprite;
    }
}
