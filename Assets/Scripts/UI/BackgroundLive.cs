using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLive : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer bg;
    float c = 0f;
    int time = 0;
    private void Update()
    {
        time++;
        if(time%90 == 0)
        {
            c += 0.4f;
            bg.color = new Color(c, c, c);
            if (c > 1f) c = 0f;
;        }
    }
}
