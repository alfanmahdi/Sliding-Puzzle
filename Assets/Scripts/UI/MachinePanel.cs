using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinePanel : MonoBehaviour
{
    // Start is called before the first frame update
    public void OpenOptionPanel()
    {
        this.gameObject.SetActive(true);
    }

    public void CloseOptionPanel()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(false);
    }
}
