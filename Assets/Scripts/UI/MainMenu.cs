using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject logo;
    public TextMeshProUGUI text;
    public GameObject panel;
    public GameObject mainMenu;
    public LogoutScript logoutScript;

    public void HoverText()
    {
        text.fontSize = 75;
        logo.SetActive(true);
    }

    public void ExitHoverText()
    {
        text.fontSize = 64;
        logo.SetActive(false);
    }

    public void GoToPlayGames()
    {
        if (!DBManager.isTutorial)
        {
            if (DBManager.status != 0)
            {
                SceneManager.LoadScene("GameScene2023");
            }
            else
            {
                SceneManager.LoadScene("Hallway Scene");
            }
        }
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial Study");
    }

    public void PanelPopUp()
    {
        panel.SetActive(true);
    }

    public void QuitGame()
    {
        //handle logout
        logoutScript.CallLogout();
    }

    public void BackFromPanel()
    {
        logo.SetActive(false);
        panel.SetActive(false);
        mainMenu.SetActive(true);
    }

}
