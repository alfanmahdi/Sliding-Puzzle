using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image cards;
    public CardDetailSO cardDetail;
    public Image imageDetail;
    private RectTransform rectTransform;
    private bool isDraged = false;
    private Vector3 originPosition;
    private CanvasGroup canvasGroup;

    public GameObject panelCard;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        transform.GetComponent<Image>().sprite = cardDetail.cardSprite;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().hoverSoundPlay();
        transform.localScale += new Vector3(0.1f, 0.1f, 0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        isDraged = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GameManager.Instance.canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDraged = false;
        originPosition = rectTransform.anchoredPosition;
    }
    

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isDraged)
        {
            GameManager.Instance.audioManager.GetComponent<SoundManager>().clickSoundPlay();
            var cardPanel = Instantiate(GameResource.Instance.detailPanel, GameManager.Instance.panelTransform);
            cardPanel.transform.GetChild(1).GetComponent<Image>().sprite = cardDetail.cardSprite;
            if (cardDetail.haveText)
            {
                cardPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = cardDetail.text;
                cardPanel.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
        else
        {
            if (true)
            {
                rectTransform.anchoredPosition = originPosition;
            }
        }
    }
    
}
