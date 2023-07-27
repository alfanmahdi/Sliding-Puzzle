using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintPanel : MonoBehaviour
{
    public static HintPanel instance;
    public GameObject hintPanel;
    public GameObject cardHint;
    public GameObject confirmationPanel;
    public TextMeshProUGUI cardHintText;
    public TextMeshProUGUI hintCost;
    public Sprite placHolder;
    void Awake() { instance = this;  }
    ActivePanel previousPanel;
    public void OnEnable()
    {
        previousPanel = GameManager.Instance.activePanel;
        GameManager.Instance.activePanel = ActivePanel.hint;
        hintPanel.SetActive(true);
    }

    public void OnDisable()
    {
        confirmationPanel.SetActive(false);
        GameManager.Instance.activePanel = previousPanel;
        GameManager.Instance.selectedCardHint = null;
        GetSetCardHintText("");
        GameManager.Instance.hintCardImageSelected.GetComponent<Image>().sprite = placHolder;
        hintPanel.SetActive(false);
    }

    public void CardHintShow()
    {
        if(GameManager.Instance.selectedCardHint != null){
            confirmationPanel.SetActive(true);
        }else{
            cardHint.SetActive(true);
            GetSetCardHintText("Tidak Ada Kartu!!!");
        }
    }
    public void SelectCardChoice()
    {
         GameManager.Instance.listCardHolder.SetActive(true);
    } 
    public void ConfirmHint()
    {
        confirmationPanel.SetActive(false);
        if(GameManager.Instance.player.UseCoin(5)){
            GetSetCardHintText(GameManager.Instance.selectedCardHint.cardDescription);
        }else{
            GetSetCardHintText("Neleci Coin Tidak Cukup");
        }
        cardHint.SetActive(true);
    }
    
    public void DeclineHint()
    {
        confirmationPanel.SetActive(false);
    }

    public void GetSetCardHintText(string text)
    {
        cardHintText.text = text;
    }
}
