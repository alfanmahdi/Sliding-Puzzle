using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButton : MonoBehaviour
{
    [SerializeField] GameObject testGameObject;
    // Start is called before the first frame update
    public void ActivatePanel()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        if (!testGameObject.activeInHierarchy)
            testGameObject.SetActive(true);
    }
}
