using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbySceneManager : MonoBehaviour
{
    public void OnClick_NewGameBtn()
    {
        Debug.Log("·Îºñ -> È¦ ¾À º¯°æ");
        SceneManager.LoadScene("HallScene");
    }

    public void OnClick_ExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
