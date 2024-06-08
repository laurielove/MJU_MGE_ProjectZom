using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
    [SerializeField]
    private float cookTime = 0f;
    [SerializeField]
    private float maxCookTime = 7f;
    [SerializeField]
    private Sprite[] steakSprites;
    private SpriteRenderer steakRenderer;
    // private enum CookingState { Raw, Rare, Medium, WellDone, Burnt };
    // private CookingState currentCookingState = CookingState.Raw;
    public string cookingState;

    private bool cooking = false; 

    [SerializeField]
    private GameObject GrillGaugeUI;
    [SerializeField]
    private GameObject GaugeArrowUI;

    private RectTransform gaugeBarRect;
    private RectTransform arrowRect;
    

    void Awake()
    {
        gaugeBarRect = GrillGaugeUI.GetComponent<RectTransform>();
        arrowRect = GaugeArrowUI.GetComponent<RectTransform>();
    }

    void Start()
    {
        steakRenderer = GetComponent<SpriteRenderer>();
        // currentCookingState = CookingState.Raw;
        steakRenderer.sprite = steakSprites[0]; 
        cookingState = "Raw";
        cookTime = 0f;

        GrillGaugeUI.SetActive(false);
        GaugeArrowUI.SetActive(false);
    }

    void Update()
    {
        if ((cookTime <= maxCookTime) && (cooking == true) && (StateManger.grillState == true))
        {
            cookTime += Time.deltaTime;

            UpdateArrowPosition();
            UpdateCookingState();
            if ((!GrillGaugeUI.activeSelf) && (!GaugeArrowUI.activeSelf))
            {
                GrillGaugeUI.SetActive(true);
                GaugeArrowUI.SetActive(true);
            }

        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Fire")
        {
            cooking = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Fire")
        {
            cooking = false;
        if (GrillGaugeUI != null && GaugeArrowUI != null)
        {
            GrillGaugeUI.SetActive(false);
            GaugeArrowUI.SetActive(false);
        }
        }
    }

    void UpdateCookingState()
    {
        if (cookTime < 1.0f)
        {
            // currentCookingState = CookingState.Raw;
            steakRenderer.sprite = steakSprites[0];
            cookingState = "Raw";
        }
        else if (cookTime < 3.0f)
        {
            // currentCookingState = CookingState.Rare;
            steakRenderer.sprite = steakSprites[1];
            cookingState = "Rare";
        }
        else if (cookTime < 5.0f)
        {
            // currentCookingState = CookingState.Medium;
            steakRenderer.sprite = steakSprites[2];
            cookingState = "Medium";
        }
        else if (cookTime < 7.0f)
        {
            // currentCookingState = CookingState.WellDone;
            steakRenderer.sprite = steakSprites[3];
            cookingState = "WellDone";
        }
        else
        {
            // currentCookingState = CookingState.Burnt;
            steakRenderer.sprite = steakSprites[4];
            cookingState = "Burnt";
            cooking = false;
            GrillGaugeUI.SetActive(false);
            GaugeArrowUI.SetActive(false);
            cookTime = 0;
        }
    }

    void UpdateArrowPosition()
    {
        float normalizedCookTime = cookTime / maxCookTime;
        float arrowPositionX = gaugeBarRect.anchoredPosition.x + (gaugeBarRect.rect.width * normalizedCookTime);

        arrowRect.anchoredPosition = new Vector3 (arrowPositionX - 128 , arrowRect.anchoredPosition.y, 0);
        
    }



    // void CalculateScore()
    // {
    //     // 예시로 각 상태에 따른 점수를 설정합니다. 실제로는 게임에 맞게 조절해야 합니다.
    //     int score = 0;
    //     switch (currentCookingState)
    //     {
    //         case CookingState.Raw:
    //             score = 10;
    //             break;
    //         case CookingState.Rare:
    //             score = 30;
    //             break;
    //         case CookingState.Medium:
    //             score = 50;
    //             break;
    //         case CookingState.WellDone:
    //             score = 70;
    //             break;
    //         case CookingState.Burnt:
    //             score = 20; // 너무 탔으면 점수가 낮아질 수 있습니다.
    //             break;
    //     }

    //     // 점수를 출력하거나 다른 곳에 사용할 수 있습니다.
    //     Debug.Log("현재 점수: " + score);
    // }
}