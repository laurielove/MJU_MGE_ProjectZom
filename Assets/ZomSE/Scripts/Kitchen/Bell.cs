using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class Bell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Sprite[] steakSprites;
    private SpriteRenderer steakRenderer;

    [SerializeField]
    private GameObject Plate;
    [SerializeField]
    private GameObject Reaction;

    [SerializeField]
    private Sprite[] ReactionSprites;
    private SpriteRenderer ReactionRendere;

    // [SerializeField]
    // private TextMeshProUGUI SatisPerText;

    private Vector3 targetPosition_p;
    private Vector3 targetPosition_d;

    Main main;
    Sub sub_1, sub_2, sub_3;
    


    private void Awake()
    {
        targetPosition_p = new Vector3(7.5f, -6, 0);
        targetPosition_d = new Vector3(9f, -6, 0);
    }

    void Start()
    {
        steakRenderer = GetComponent<SpriteRenderer>();
        steakRenderer.sprite = steakSprites[0];
        ReactionRendere = Reaction.GetComponent<SpriteRenderer>();
        ReactionRendere.sprite = ReactionSprites[0];
        Reaction.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        steakRenderer.sprite = steakSprites[1];
        if ((StateManger.p_mainObject != null)/* && (StateManger.p_sub01Object != null) */&& (StateManger.p_drinkObject != null))
        {
            StartCoroutine(MoveToTargetPosition());
            CalculateSatisPer();
            StartCoroutine(AppearReaction());
        }
        else
        {
            Debug.Log("요리를 완성해주세요!");
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        steakRenderer.sprite = steakSprites[0];
    }

    private IEnumerator MoveToTargetPosition()
    {
        float duration = 1.5f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            Plate.transform.position = Vector3.Lerp(Plate.transform.position, targetPosition_p, elapsedTime / duration);
            StateManger.p_drinkObject.transform.position = Vector3.Lerp(StateManger.p_drinkObject.transform.position, targetPosition_d, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }    
    }

    private void CalculateSatisPer()
    {
        main = StateManger.p_mainObject.GetComponent<Main>();
        if(StateManger.p_sub01Object != null)
        {
            sub_1 = StateManger.p_sub01Object.GetComponent<Sub>();
        }
        // sub_1 = StateManger.p_sub01Object.GetComponent<Sub>();
        if(StateManger.p_sub02Object != null)
        {
            sub_2 = StateManger.p_sub02Object.GetComponent<Sub>();
        }
        if(StateManger.p_sub03Object != null)
        {
            sub_3 = StateManger.p_sub03Object.GetComponent<Sub>();
        }


        //메인
        if ((StateManger.p_mainObject.name == DataManager.orderMain) && (main.cookingState == DataManager.orderCook))
        {
            if (DataManager.orderSub_1 == null)
            {
                DataManager.satisPer_Accuracy += 30; 
            }
            else
            {
                DataManager.satisPer_Accuracy += 20; 
            }
            
            Debug.Log("메인 굿");
          
        }
        //서브
        if (DataManager.orderSub_1 != null)
        {
            if ((sub_1.cookingState == "Cooked") && (sub_2.cookingState == "Cooked") && (sub_3.cookingState == "Cooked"))
            {
                List<string> orderList_Sub = new List<string> { DataManager.orderSub_1, DataManager.orderSub_2, DataManager.orderSub_3 };
                List<string> plateList_Sub = new List<string> { StateManger.p_sub01Object.name, StateManger.p_sub02Object.name, StateManger.p_sub03Object.name };

                orderList_Sub.Sort();
                plateList_Sub.Sort();

                bool allMatch = true;
                for (int i = 0; i < orderList_Sub.Count; i++)
                {
                    if (orderList_Sub[i] != plateList_Sub[i])
                    {
                        allMatch = false;
                        break;
                    }
                }

                if (allMatch)
                {
                    DataManager.satisPer_Accuracy += 20;
                    Debug.Log("서브 굿");
                }
            }

        }
        
        //음료
        if ((StateManger.p_drinkObject.name == DataManager.orderDrink))
        {
            if (DataManager.orderSub_1 == null)
            {
                DataManager.satisPer_Accuracy += 20; 
            }
            else
            {
                DataManager.satisPer_Accuracy += 10; 
            }
            // DataManager.satisPer_Accuracy += 10;    
            Debug.Log("음료 굿");
        }
        //현재 고객 만족도
        DataManager.cusSatisPer = DataManager.satisPer_Patience + DataManager.satisPer_Accuracy;
        GameManager.Inst.SatisPercent_Today = DataManager.satisPer_Patience + DataManager.satisPer_Accuracy; // -Add EJ-
        Debug.Log(DataManager.cusSatisPer);
        if (DataManager.satisPer_Patience == 0)
        {
            DataManager.cusSatisPer = 0;
        }
        //총 만족도
        Debug.Log(DataManager.totalSatisPer);
        DataManager.sumSatisPer += DataManager.cusSatisPer;
        DataManager.totalSatisPer = DataManager.sumSatisPer / DataManager.cusCount;
        Debug.Log(DataManager.totalSatisPer);
        
    }

    private IEnumerator AppearReaction()
    {
        Reaction.SetActive(true);
        ReactionRendere.sprite = ReactionSprites[1];
        UIManager.Inst.ChangeSatisPer();
        // SatisPerText.text = DataManager.totalSatisPer.ToString() + "%";
        if(DataManager.cusSatisPer >= 80 )
        {
            ReactionRendere.sprite = ReactionSprites[0];
        }
        else if((DataManager.cusSatisPer >= 60) && (DataManager.cusSatisPer < 80))
        {
            ReactionRendere.sprite = ReactionSprites[1];
        }
        else if((DataManager.cusSatisPer >= 40) && (DataManager.cusSatisPer < 60))
        {
            ReactionRendere.sprite = ReactionSprites[2];
        }
        else if((DataManager.cusSatisPer >= 20) && (DataManager.cusSatisPer < 40))
        {
            ReactionRendere.sprite = ReactionSprites[3];
        }
        else if((DataManager.cusSatisPer >= 0) && (DataManager.cusSatisPer < 20))
        {
            ReactionRendere.sprite = ReactionSprites[4];
        }

        // 추가
        UIManager.Inst.StopCoroutine(UIManager.Inst.cor);
        UIManager.Inst.patienceGaugeUI.SetActive(false);       

        yield return new WaitForSeconds(0.5f);
        DataManager.satisPer_Patience = 0;
        DataManager.satisPer_Accuracy = 0;
        DataManager.cusSatisPer = 0;
        SceneManager.LoadScene("HallScene");
    }

}