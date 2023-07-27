using UnityEngine;
using UnityEngine.UI;

public class MachineCardPanelTutor : MonoBehaviour
{
    public GameObject silangButton;
    public GameObject MachinePlaceholder;
    public GameObject PintuKuil;

    public void OpenPanelMachine()
    {
        GameManager.Instance.CloseAllPanel();
        this.gameObject.SetActive(true);
    }

    private void OnEnable(){
        MachinePlaceholder.SetActive(true);
        GameManager.Instance.activePanel = ActivePanel.machine;
    }

    public void SelectCardChoice(){
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        GameManager.Instance.listCardHolder.SetActive(true);
    }
    private void Update(){
        if(GameManager.Instance.selectedMachineCard != null){
            silangButton.SetActive(true);
        }
    }

    public void RemoveCardFromHolder()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
        silangButton.SetActive(false);
        GameManager.Instance.selectedMachineCard = null;
        GameManager.Instance.machineCardImageSelected.GetComponent<Image>().sprite = GameManager.Instance.cardHolder;
        DeactivateMachine();
        MachinePlaceholder.SetActive(true);
    }

    public void OnDisable()
    {
        if (GameManager.Instance.selectedMachineCard != null)
            RemoveCardFromHolder();
    }

    public void ActiveMachine()
    {
        DeactivateMachine();
        switch (GameManager.Instance.selectedMachineCard.cardID)
        {
            case "54":
                PintuKuil.SetActive(true);
                break;
        }
    }

    public void DeactivateMachine()
    {
        MachinePlaceholder.SetActive(false);
        PintuKuil.SetActive(false);
    }
}
