using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Inst => instance;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }
        else  // 최초의 인스턴스 생성
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        currentWeek = 1;
        dayCount = 1;
        timeCount = 0;
        satisPercent_All = 0;
        satisSum_Today = 0;
        satisPercent_Today = 0;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    #region _Manager_
    private GameObject obj;

    private FadeIn fadeIn;
    public FadeIn FadeIn
    {
        get
        {
            if (fadeIn == null)
            {
                obj = GameObject.Find("FadeInBG");
                if (!obj.TryGetComponent<FadeIn>(out fadeIn))
                    Debug.Log("GameManager.cs - fadeIn 참조 실패");
                obj.SetActive(false);
            }
            return fadeIn;
        }
    }


    private CustomerManager customerManager;
    public CustomerManager CustomerManager
    {
        get
        {
            if (customerManager == null)
            {
                obj = GameObject.Find("Customer_man");
                if (!obj.TryGetComponent<CustomerManager>(out customerManager))
                    Debug.Log("GameManager.cs - customerManager 참조 실패");
            }
            return customerManager;
        }
    }

    #endregion

    [SerializeField]
    [Range(0f, 4.5f)]
    private float timeSpeed = 4.5f;

    [SerializeField]
    private int[] dayCusCount = new int[] { 6, 8, 10, 10 };

    [SerializeField]
    public int[] patiencePerSecond = new int[] { 4, 4, 3, 3 };

    public int currentWeek;
    public int dayCount;
    public int timeCount;

    private int satisSum_Today;
    private int satisPercent_Today;
    public int SatisPercent_Today
    {
        set
        {
            satisSum_Today += value;
            satisPercent_Today = satisSum_Today / cusCount;
        }
        get
        {
            return satisPercent_Today;
        }
    }

    private int satisPercent_All;
    public int SatisPercent_All
    {
        get
        {
            return satisPercent_All;
        }
    }

    private int cusCount;
    public int CusCount
    {
        set
        {
            cusCount += value;

            if (cusCount > dayCusCount[currentWeek - 1])
            {
                CustomerManager.CancelCusCor();
                EndDay();
            }
            else
                UIManager.Inst.ChangeCusCount(cusCount, dayCusCount[currentWeek - 1]);
        }
    }


    public void NewGameStart()
    {
        FadeIn.DayFadeStart();
        UIManager.Inst.SetDayText(dayCount);
        CustomerManager.SetCusCor();
        InvokeRepeating("StartTimer", 0, timeSpeed);
    }

    public void OnGameStart()
    {
        // tobe: 이어하기 경우 
    }

    public void StartTimer()
    {
        UIManager.Inst.SetTimeText(timeCount);
        timeCount += 15;
       
        if (timeCount > 600)  // 마감 시간
        {
            CancelInvoke("StartTimer");
            EndDay();
        }
    }

    public void EndDay()
    {
        UIManager.Inst.StopCoroutine(UIManager.Inst.cor);
        CancelInvoke("StartTimer");
        customerManager.cusDisappear();
        UIManager.Inst.ShowReceipt();
    }

    public void NextDay()
    {
        UIManager.Inst.receiptBG.SetActive(false);

        if (DataManager.totalSatisPer <= 30)
        {
            StopCoroutine(UIManager.Inst.cor);
            Destroy(UIManager.Inst.gameObject);
            SceneManager.LoadScene("Ending03Scene");
            return;
        }

        dayCount++;
        if ((dayCount-1) % 28 == 0)
        {
            //UIManager.Inst.DestroyUI();
            SceneManager.LoadScene("StarScene");
            return;
        }
        if ((dayCount-1) % 7 == 0)
        {
            currentWeek++;
            satisPercent_All = 100;
            if ((DataManager.totalSatisPer > 30) && (DataManager.totalSatisPer < 50))
            {
                DataManager.sumStar += 1;
            }
            else if ((DataManager.totalSatisPer >= 50) && (DataManager.totalSatisPer < 80))
            {
                DataManager.sumStar += 2;
            }
            else if ((DataManager.totalSatisPer >= 80) && (DataManager.totalSatisPer <= 100))
            {
                DataManager.sumStar += 3;
            }
            DataManager.totalSatisPer = 0;
            DataManager.sumSatisPer = 0;
            DataManager.cusCount = 0;
            satisPercent_All = 0;
            UIManager.Inst.satisPercetText.text = "- %";
        }
        timeCount = 0;
        cusCount = 0;
        satisSum_Today = 0;
        satisPercent_Today = 0;
        UIManager.Inst.ChangeCusCount(cusCount, dayCusCount[currentWeek - 1]);
        DataManager.cusCount -= 1;
        UIManager.Inst.ChangeSatisPer();

        FadeIn.DayFadeStart();
        UIManager.Inst.SetDayText(dayCount);
        CustomerManager.SetCusCor();

        InvokeRepeating("StartTimer", 0, timeSpeed);
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {

            if (dayCount == 1 && timeCount == 0)
                NewGameStart();
            else
            {
                FadeIn.MoveFadeStart();
                CustomerManager.SetCusCor();
            }
        }
        else if (scene.buildIndex == 2)
        {
            UIManager.Inst.StartDownGauge(patiencePerSecond[currentWeek-1]);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
