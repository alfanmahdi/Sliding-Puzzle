using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buletan : MonoBehaviour
{
    public bool isMuter = false;
    float rotationAngle = 180f;
    public int index;
    public CircleCollider2D thisCollider;
    // private float initialDuration = 1.5f;


    public void Rotate()
    {
        if(!isMuter)
        {
            // Debug.Log("Bulet "+index+" muter kanan");
            transform.Rotate(Vector3.back, rotationAngle);
            // StartRotation(-rotationAngle);
            // StartCoroutine(RotateCoroutine(-rotationAngle));
            isMuter = true;
        }
        else
        {
            // Debug.Log("Bulet "+index+" muter kiri");
            transform.Rotate(Vector3.forward, rotationAngle);
            // StartRotation(rotationAngle);
            // StartCoroutine(RotateCoroutine(rotationAngle));
            isMuter = false;
        }
    }

    public void OnMouseDown()
    {
        // Debug.Log(index +" kepencet!!!");
        // thisCollider.enabled = false;
        Rotate();
        
    }

    // private float rotationDuration;
    void Start()
    {
        // rotationDuration = initialDuration;

    }
    // Duration of rotation in seconds
    public void BuletReset()
    {
        // rotationDuration=0f;
        if(isMuter)
        {
            Rotate();
            // isMuter=false;
            // transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
        }
        // rotationDuration=initialDuration;
    }

    // private IEnumerator RotateCoroutine(float target)
    // {
    //     float targetAngle = transform.eulerAngles.z + target;
    //     float initialAngle = transform.eulerAngles.z;
    //     float elapsedTime = 0f;

    //     while (elapsedTime < rotationDuration)
    //     {
    //         float t = elapsedTime / rotationDuration;
    //         float angle = Mathf.Lerp(initialAngle, targetAngle, t);
    //         transform.rotation = Quaternion.Euler(0f, 0f, angle);
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
    //     thisCollider.enabled = true;
    //     transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
    // }
    
}
