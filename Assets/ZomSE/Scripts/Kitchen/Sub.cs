using UnityEngine;
using UnityEngine.UI;

public class Sub : MonoBehaviour
{
    [SerializeField]
    private float cookTime = 0f;
    [SerializeField]
    private float maxCookTime = 3f;
    [SerializeField]
    private Sprite[] steakSprites;
    private SpriteRenderer steakRenderer;
    // private enum CookingState { Raw, Cooked};
    // private CookingState currentCookingState = CookingState.Raw;
    public string cookingState;

    private bool cooking = false; 
    // private Vector3 cookingPos;

    void Start()
    {
        steakRenderer = GetComponent<SpriteRenderer>();
        steakRenderer.sprite = steakSprites[0];
        cookingState = "Raw";
        cookTime = 0f;

        Button panButton = GameObject.Find("PanButton").GetComponent<Button>();
        panButton.onClick.AddListener(StartCooking);
    }

    void Update()
    {
        if ((cookTime <= maxCookTime) && (cooking == true) && ((StateManger.pan01State == true) || (StateManger.pan02State == true) || (StateManger.pan03State == true)))
        {
            cookTime += Time.deltaTime;           
            UpdateCookingState();
        }
        if ((StateManger.Pan01Object == null) && (StateManger.Pan02Object == null) && (StateManger.Pan03Object == null))
        {
            StateManger.pan01State = false;
            StateManger.pan02State = false;
            StateManger.pan03State = false;
        }
    }
    
    void StartCooking()
    {
        if((cookTime == 0f) && ((this.gameObject == StateManger.Pan01Object) || (this.gameObject == StateManger.Pan02Object) || (this.gameObject == StateManger.Pan03Object)))
        {
            cooking = true;
            StateManger.pan01State = true;
            StateManger.pan02State = true;
            StateManger.pan03State = true;
            gameObject.tag = "cooking";
        }
    }

    void UpdateCookingState()
    {
        if (cookTime < 3.0f)
        {
            // currentCookingState = CookingState.Raw;
            steakRenderer.sprite = steakSprites[0];
            cookingState = "Raw";
        }
        else if (cookTime >= 3.0f)
        {
            // currentCookingState = CookingState.Cooked;
            steakRenderer.sprite = steakSprites[1];
            cookingState = "Cooked";
            cooking = false;
            gameObject.tag = "cooked";
        }
    }

}