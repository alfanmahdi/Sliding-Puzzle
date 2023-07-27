using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WinPanel : MonoBehaviour
{
    
    private string uri = "https://api.schematics-its.com/api/escapecard/savedata?id=" + DBManager.id;
    private string uriTotalTeam = "https://api.schematics-its.com/api/count_status";
    private string sceneName;

    private void OnEnable()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().winSoundPlay();
        DBManager.remaining_coins = 0;
        DBManager.status = 1;
        DBManager.remaining_hours = 0;
        StartCoroutine(GetTotalWinTeam());
    }

    public void toMainMenu()
    {
        if (DBManager.isTutorial)
            GameManager.Instance.winPanel.SetActive(true);
        else
        {
            sceneName = "Main Menu";
            GameManager.Instance.ChangeScene(sceneName);
        }
    }
    IEnumerator PostWin()
    {
        WWWForm form = new();

        form.AddField("team_name", DBManager.team_name);
        form.AddField("remaining_coin", DBManager.remaining_coins);
        form.AddField("remaining_time", (int)DBManager.remaining_hours);
        form.AddField("discard_card_count", DBManager.discardCardsCount);
        form.AddField("scores", DBManager.scores);
        form.AddField("map_id", DBManager.mapID);

        /*form.AddField("status", DBManager.status);*/
        form.AddField("status", 1);

        string ownedCards = "";
        form.AddField("ownedCards", ownedCards);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            webRequest.method = "PATCH";
            webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                // Debug.Log("Error saving data");
            }
            else
            {
                webRequest.Dispose();
            }
        }
    }

    IEnumerator GetTotalWinTeam()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uriTotalTeam))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    break;
                case UnityWebRequest.Result.Success:
                    string rawResponse = webRequest.downloadHandler.text;
                    GameAPIDTO.bodyCountStatus response = JsonUtility.FromJson<GameAPIDTO.bodyCountStatus>(rawResponse);

                    if (Player.instance.isFirstWinPanel)
                    {
                        int tambahanPoin = 160 - (2 * response.data.win);
                        if (tambahanPoin < 0)
                        {
                            tambahanPoin = 2;
                        }


                        DBManager.scores += tambahanPoin;
                        Player.instance.isFirstWinPanel = false;
                    }

                    break;
            }

            StartCoroutine(PostWin());
        }
    }

}
