using UnityEngine;
using UnityEngine.EventSystems;

public class Drink : MonoBehaviour
{
    [SerializeField]
    private float cookTime = 0f;
    [SerializeField]
    private float maxCookTime = 1f;
    [SerializeField]
    private Sprite[] steakSprites;
    private SpriteRenderer steakRenderer;
    private enum CookingState { None, Done };
    private CookingState currentCookingState = CookingState.None;

    [SerializeField]
    private GameObject drink;

    private bool cooking = false;

    void Start()
    {
        steakRenderer = GetComponent<SpriteRenderer>();
        steakRenderer.sprite = steakSprites[0];
        cookTime = 0f;
    }

    void Update()
    {
        if ((cookTime <= maxCookTime) && (cooking == true))
        {
            cookTime += Time.deltaTime;
            UpdateCookingState();
        }
        if((this.gameObject.tag == "Untagged") && (steakRenderer.sprite == steakSprites[1]))
        {
            currentCookingState = CookingState.None;
            steakRenderer.sprite = steakSprites[0];
        }
    }

    public void StartCooking()
    {
        if(this.gameObject.tag == "Untagged")
        {
            drink.SetActive(true);
            cookTime = 0.0f;
            cooking = true;
            gameObject.tag = "cooking";
        }
        else if (this.gameObject.tag == "drink")
        {
            drink.SetActive(false);
        }
    }

    void UpdateCookingState()
    {
        if (cookTime < 1.0f)
        {
            currentCookingState = CookingState.None;
            steakRenderer.sprite = steakSprites[0];
        }
        else if (cookTime >= 1.0f)
        {
            currentCookingState = CookingState.Done;
            steakRenderer.sprite = steakSprites[1];
            cooking = false;
            gameObject.tag = "drink";
            drink.SetActive(false);
        }
    }
}