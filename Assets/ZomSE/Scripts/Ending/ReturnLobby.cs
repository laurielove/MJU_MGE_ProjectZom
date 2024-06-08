using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ReturnLobby : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Sprite[] steakSprites;
    private SpriteRenderer steakRenderer;

    // Start is called before the first frame update
    void Start()
    {
        steakRenderer = GetComponent<SpriteRenderer>();
        steakRenderer.sprite = steakSprites[0];
        ChangeImg();
    }

    private void ChangeImg()
    {
        if(DataManager.sumStar <= 7)
        {
            steakRenderer.sprite = steakSprites[0];
        }
        if((DataManager.sumStar >= 8) && (DataManager.sumStar <= 10))
        {
            steakRenderer.sprite = steakSprites[1];
        }
        if((DataManager.sumStar >= 11) && (DataManager.sumStar <= 12))
        {
            steakRenderer.sprite = steakSprites[2];
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("누름");
        SceneManager.LoadScene("LobbyScene");  
    }
}
