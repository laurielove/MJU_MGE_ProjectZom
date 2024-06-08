using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class CustomerManager : MonoBehaviour  // 손님, 말풍선 UI, 인내심 게이지 UI 관리
{
    private float cusAppearPoint = 0.4f;
    private float cusDefaltPoint = -7f;

    [SerializeField]
    private SpriteRenderer CusIMG;

    [SerializeField]
    private Sprite[] CusIMGs;

    [SerializeField]
    private string[] orderList_Main;
    [SerializeField]
    private string[] orderList_Sub;
    [SerializeField]
    private string[] orderList_Cook;
    [SerializeField]
    private string[] orderList_Drink;

    [SerializeField]
    private int[] orderLevel_Main;
    [SerializeField]
    private int[] orderLevel_Sub;

    // 말풍선 UI
    private GameObject orderUI;
    private TextMeshProUGUI orderText;

    private GameObject obj;
    public Coroutine cor;

    private int[] orderLists;

    private Dictionary<int, string> orderMainMapping;
    private Dictionary<int, string> orderCookMapping;
    private Dictionary<int, string> orderSubMapping;
    private Dictionary<int, string> orderDrinkMapping;

    private void Awake()
    {
        obj = GameObject.Find("OrderUI");
        if (obj != null)
        {
            orderUI = obj;
            if (!orderUI.transform.Find("OrderText").TryGetComponent<TextMeshProUGUI>(out orderText))
                Debug.Log("CustomerManager.cs - Awake() - orderText 참조 실패");
        }
        else
            Debug.Log("CustomerManager.cs - Awake() - orderUI 참조 실패");

        orderUI.SetActive(false);

        orderMainMapping = new Dictionary<int, string>
        {{ 0, "Main_bowels" }, { 1, "Main_heart" }, { 2, "Main_liver" }, { 3, "Main_lung" }, { 4, "Main_brain" }, { 5, "Main_stomach" }};
        orderCookMapping = new Dictionary<int, string>
        {{ 0, "Raw" }, { 1, "Rare" }, { 2, "Medium" }, { 3, "WellDone" }};
        orderSubMapping = new Dictionary<int, string>
        {{ 0, "Sub_noses" }, { 1, "Sub_teeth" }, { 2, "Sub_finger" }, { 3, "Sub_ears" }, { 4, "Sub_eye" }, { 5, "Sub_hair" }};
        orderDrinkMapping = new Dictionary<int, string>
        {{ 0, "Cup_2" }, { 1, "Cup_1" }};
    }
    
    public void SetCusCor()  
    {
        cor = StartCoroutine(cusAppear());
    }

    public void CancelCusCor()
    {
        StopCoroutine(cor);
    }

    public IEnumerator cusAppear()
    {
        yield return new WaitForSeconds(2f);
        int randomIndex = Random.Range(0, CusIMGs.Length);
        CusIMG.sprite = CusIMGs[randomIndex];

        GameManager.Inst.CusCount = 1;
        yield return null;
        transform.DOMoveY(cusAppearPoint, 1f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1f);

        Order(GameManager.Inst.currentWeek);
    }

    public void cusDisappear()
    {
        transform.DOMoveY(cusDefaltPoint, 1f).SetEase(Ease.OutBounce);
        orderUI.SetActive(false);
        UIManager.Inst.patienceGaugeUI.SetActive(false);
    }

    public int[] PickRandomOrder(int Level)
    {
        int[] randomIndexs = new int[6];
        
        randomIndexs[0] = Random.Range(0, orderLevel_Main[Level-1]);
        randomIndexs[1] = Random.Range(0, orderList_Cook.Length);
        randomIndexs[2] = Random.Range(0, orderList_Drink.Length);

        for (int i = 3; i < 6; i++)
            randomIndexs[i] = Random.Range(0, orderLevel_Sub[Level - 1]);

        return randomIndexs;
    }

    public void Order(int Level)
    {
        orderLists = PickRandomOrder(Level);

        string orderSub = "";
        if (Level == 2)
            orderSub = $"\n{orderList_Sub[orderLists[3]]}도 주세요.";
        else if (Level == 3)
            orderSub = $"\n{orderList_Sub[orderLists[3]]}, {orderList_Sub[orderLists[4]]}도 주세요.";
        else if (Level == 4)
            orderSub = $"\n{orderList_Sub[orderLists[3]]}, {orderList_Sub[orderLists[4]]}, {orderList_Sub[orderLists[5]]}도 주세요.";

        orderText.text = $"{orderList_Main[orderLists[0]]}를 {orderList_Cook[orderLists[1]]} 해주시고요," + 
            orderSub + $"\n그리고 {orderList_Drink[orderLists[2]]} 주세요.";

        DataManager.recipeText = orderText.text;
        CheckOrder();

        orderUI.SetActive(true);

        UIManager.Inst.patienceGaugeBG.color = new Color(34f / 255f, 63f / 255f, 62f / 255f);
        UIManager.Inst.patienceGaugeFill.color = new Color(31f / 255f, 115f / 255f, 111f / 255f);
        UIManager.Inst.patienceGaugeUI.SetActive(true);
        UIManager.Inst.patienceGauge.value = 100;
        UIManager.Inst.StartDownGauge(GameManager.Inst.patiencePerSecond[GameManager.Inst.currentWeek-1]);
    }

    public void OnClick_OKBtn()
    {
        Debug.Log("홀 -> 주방 씬 변경");
        UIManager.Inst.StopCoroutine(UIManager.Inst.cor);
        SceneManager.LoadScene("KitchenScene");
    }

    public void OnClick_CancelBtn()
    {
        cusDisappear();
        DataManager.sumSatisPer += DataManager.cusSatisPer;
        DataManager.totalSatisPer = DataManager.sumSatisPer / DataManager.cusCount;
        GameManager.Inst.SatisPercent_Today = 0;
        UIManager.Inst.ChangeSatisPer();
        UIManager.Inst.StopCoroutine(UIManager.Inst.cor);
        cor = StartCoroutine(cusAppear());
    }

    private void CheckOrder()
    {
        if (orderMainMapping.TryGetValue(orderLists[0], out string orderMain))
        {
            DataManager.orderMain = orderMain;
        }
        if (orderCookMapping.TryGetValue(orderLists[1], out string orderCook))
        {
            DataManager.orderCook = orderCook;
        }
        if (orderSubMapping.TryGetValue(orderLists[2], out string orderDrink))
        {
            if (GameManager.Inst.currentWeek == 1)
            {
                DataManager.orderSub_1 = null;
            }
            else
            {
                DataManager.orderSub_1 = orderDrink;
            }
            // DataManager.orderSub_1 = orderDrink;
        }
        if (orderSubMapping.TryGetValue(orderLists[3], out string orderSub_1))
        {
            if (GameManager.Inst.currentWeek == 1)
            {
                DataManager.orderSub_2 = null;
            }
            else
            {
                DataManager.orderSub_2 = orderSub_1;
            }
        }
        if (orderSubMapping.TryGetValue(orderLists[4], out string orderSub_2))
        {
            if (GameManager.Inst.currentWeek == 1)
            {
                DataManager.orderSub_3 = null;
            }
            else
            {
                DataManager.orderSub_3 = orderSub_2;
            }
            // DataManager.orderSub_3 = orderSub_2;
        }
        if (orderDrinkMapping.TryGetValue(orderLists[5], out string orderSub_3))
        {
            DataManager.orderDrink = orderSub_3;
        }
    }
}
