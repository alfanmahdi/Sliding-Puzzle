using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardslotListcard : MonoBehaviour, IDropHandler
{
   public void OnDrop(PointerEventData eventData)
   {
       if (eventData.pointerDrag != null)
       {
           eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
       }
   }
}
