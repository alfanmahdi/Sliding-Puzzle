using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    private string uri = "https://api.schematics-its.com/api/escapecard/savedata?id=" + DBManager.id;

    private void OnEnable()
    {
        GameManager.Instance.audioManager.GetComponent<SoundManager>().loseSoundPlay();
        DBManager.scores += Player.instance.currentCoin * 2;
        DBManager.remaining_coins = 0;
        DBManager.status = -1;
        DBManager.remaining_hours = 0;
        StartCoroutine(PostLose());
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator PostLose()
    {
        WWWForm form = new();

        form.AddField("team_name", DBManager.team_name);
        form.AddField("remaining_coin", DBManager.remaining_coins);
        form.AddField("remaining_time", (int)DBManager.remaining_hours);
        form.AddField("discard_card_count", DBManager.discardCardsCount);
        form.AddField("scores", DBManager.scores);
        form.AddField("map_id", DBManager.mapID);

        /*form.AddField("status", DBManager.status);*/
        form.AddField("status", -1);

        string ownedCards = "";
        form.AddField("ownedCards", ownedCards);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            webRequest.method = "PATCH";
            webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                // Debug.Log(webRequest.error);
            }
            else
            {
                webRequest.Dispose();
                
            }
        }
    }
}
