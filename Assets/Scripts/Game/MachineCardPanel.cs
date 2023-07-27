using UnityEngine;
using UnityEngine.UI;

public class MachineCardPanel : MonoBehaviour
{
    public GameObject silangButton;
    public GameObject MachinePlaceholder;
    public GameObject SlidingPuzzle;
    public GameObject pintuGudang;
    public GameObject tulisanKuno;
    public GameObject barisanBuku;
    public GameObject pintu4Slot;
    public GameObject riddleMassage;
    public GameObject mazePuzzle;
    public GameObject PesanTerakhirRaja;
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
            case "P":
                SlidingPuzzle.SetActive(true);
                break;
            case "Y":
                pintuGudang.SetActive(true);
                break;
            case "B":
                tulisanKuno.SetActive(true);
                break;
            case "R":
                barisanBuku.SetActive(true);
                break;
            case "M":
                pintu4Slot.SetActive(true);
                break;
            case "Z":
                riddleMassage.SetActive(true);
                break;
            case "Q":
                mazePuzzle.SetActive(true);
                break;
            case "W":
                PesanTerakhirRaja.SetActive(true);
                break;
        }
    }

    public void DeactivateMachine()
    {
        MachinePlaceholder.SetActive(false);
        SlidingPuzzle.SetActive(false);
        pintuGudang.SetActive(false);
        tulisanKuno.SetActive(false);
        barisanBuku.SetActive(false);
        pintu4Slot.SetActive(false);
        riddleMassage.SetActive(false);
        mazePuzzle.SetActive(false);
        PesanTerakhirRaja.SetActive(false);
        // ElectricityMachine.SetActive(false);
        // MysteriousMapMachine.SetActive(false);
        // BookSwitchMachine.SetActive(false);
        // FloorMachine.SetActive(false);
        // PotionMakerMachine.SetActive(false);
    }
}
