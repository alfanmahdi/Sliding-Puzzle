using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHoverClick : MonoBehaviour
{
    Vector3 temp = new Vector3(100f, 0, 0);
    public void Hover()
    {
        this.transform.position -= temp;
    }

    public void ExitHover()
    {
        this.transform.position += temp;
    }
}
