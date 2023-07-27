using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class slidingPuzzleScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Transform emptySpace;

    bool buttonPressed;
    void Start()
    {

    }

    void Update()
    {
        if (buttonPressed){
            Debug.Log("lol");
        }
    }
    
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData){ 
        buttonPressed = true;
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData) {
        buttonPressed = false;
    }

    public void Submit()
    {

    }

    public void Reset()
    {

    }
}
