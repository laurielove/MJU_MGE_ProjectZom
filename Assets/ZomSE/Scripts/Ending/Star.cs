using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
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
        if( (this.gameObject.name == "Star_1") && (DataManager.sumStar <= 7))
        {
            steakRenderer.sprite = steakSprites[1];
        }
        if( (this.gameObject.name == "Star_2") && (DataManager.sumStar >= 8) && (DataManager.sumStar <= 10))
        {
            steakRenderer.sprite = steakSprites[1];
        }
        if( (this.gameObject.name == "Star_3") && (DataManager.sumStar >= 11) && (DataManager.sumStar <= 12))
        {
            steakRenderer.sprite = steakSprites[1];
        }

    }

}
