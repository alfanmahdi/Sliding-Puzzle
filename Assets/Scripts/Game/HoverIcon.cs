using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverIcon : MonoBehaviour
{
    [SerializeField] private RawImage myImage;
    [SerializeField] private Color EnterColor;
    [SerializeField] private Color ExitColor;
    public GameObject Text;

    private void Start()
    {
        EnterColor.a = ExitColor.a = 1;
        Text.SetActive(false);
    }

    public void EnterMouseOver()
    {
        myImage.color = EnterColor;
        Text.SetActive(true);
    }
    
    public void ExitMouseOver()
    {
        myImage.color = ExitColor;
        Text.SetActive(false);
    }

}
