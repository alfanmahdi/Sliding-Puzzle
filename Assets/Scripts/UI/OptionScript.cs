using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionScript : MonoBehaviour
{
    public void OnMouseOver()
    {
        
    }
    public void OpenOptionPanel()
    {
        this.gameObject.SetActive(true);
    }

    public void CloseOptionPanel()
    {
        this.gameObject.SetActive(false);
    }
}
