using UnityEngine;
using UnityEngine.UI;
public class MouseHover : MonoBehaviour
{
    private Color originalColor;
    private Color hoverColor;
    private Image image;

    void Start()
    {
        image = transform.gameObject.GetComponent<Image>();
        originalColor = image.color;
        hoverColor = new Color(0f, 1f, 0.4f, 0.17f);
    }

    public void ChangeColorEnter()
    {
        image.color = hoverColor;
    }

    public void ChangeColorExit()
    {
        image.color = originalColor;
    }
}
