using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeCutScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
