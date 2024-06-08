using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour  // �ð� UI, ��ü ������ UI, �մ� �� UI, �γ��� ������ UI, ���� UI ����
{
    // �ð� UI
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI dayText;

    // ��ü ������ UI
    [SerializeField]
    public TextMeshProUGUI satisPercetText;

    // �մ� �� UI
    [SerializeField]
    private TextMeshProUGUI cusCountText;

    // �γ��� ������ UI
    [SerializeField]
    public GameObject patienceGaugeUI;
    [SerializeField]
    public Slider patienceGauge;
    [SerializeField]
    public Image patienceGaugeBG;
    [SerializeField]
    public Image patienceGaugeFill;

    // ���� UI
    [SerializeField]
    public GameObject receiptBG;
    [SerializeField]
    public TextMeshProUGUI rec_TodayText;
    [SerializeField]
    public TextMeshProUGUI rec_AllText;
    [SerializeField]
    public TextMeshProUGUI rec_DayText;

    [SerializeField]
    private CustomerManager cusManager;

    public Coroutine cor;


    private static UIManager instance;
    public static UIManager Inst => instance;

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
        else  // ������ �ν��Ͻ� ����
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // ��¥ ����
    public void SetDayText(int Day)
    {
        string[] dayName = { "��", "ȭ", "��", "��", "��", "��", "��" };

        dayText.text = $"D-{29 - Day} ({dayName[(Day - 1) % 7]})";
    }

    // �ð� ����
    public void SetTimeText(int CurrentTime)
    {
        int startTime = 18;  // ���� �ð�

        int hours = startTime + (CurrentTime / 60);
        int minutes = CurrentTime % 60;

        string period = (hours >= 24) ? "����" : "����";
        hours %= 12;
        if (hours == 0) hours = 12;

        timeText.text = string.Format("{0} {1:D2}:{2:D2}", period, hours, minutes);
    }

    // ������ ����
    public void ChangeSatisPer()
    {
        Debug.Log("������ ����");
        satisPercetText.text = DataManager.totalSatisPer.ToString() + "%";
        Debug.Log(satisPercetText.text);
        //if (DataManager.totalSatisPer <= 30)
        //{
        //    StopCoroutine(UIManager.Inst.cor);
        //    Destroy(this.gameObject);
        //    SceneManager.LoadScene("Ending03Scene");
        //}
    }

    // �մ� �� ����
    public void ChangeCusCount(int currentCusCount, int dayCusCount)
    {
        cusCountText.text = $"{currentCusCount} / {dayCusCount}";
        DataManager.cusCount += 1;
    }

    public void StartDownGauge(int patiencePerSecond)
    {
        cor = null;
        cor = StartCoroutine(DownPatienceGauge(patiencePerSecond));
    }

    // �γ��� ������ ����
    private IEnumerator DownPatienceGauge(int patiencePerSecond)
    {
        while (patienceGauge.value > 0)
        {
            yield return new WaitForSeconds(1f);
            patienceGauge.value -= patiencePerSecond;
            if (patienceGauge.value >= 51)
            {
                patienceGaugeBG.color = new Color(34f / 255f, 63f / 255f, 62f / 255f);
                patienceGaugeFill.color = new Color(31f / 255f, 115f / 255f, 111f / 255f);
            }
            else if (patienceGauge.value >= 21)
            {
                patienceGaugeBG.color = new Color(81f / 255f, 76f / 255f, 26f / 255f);
                patienceGaugeFill.color = new Color(183f / 255f, 172f / 255f, 54f / 255f);
            }
            else
            {
                patienceGaugeBG.color = new Color(83f / 255f, 37f / 255f, 37f / 255f);
                patienceGaugeFill.color = new Color(151f / 255f, 23f / 255f, 23f / 255f);
            }

            //�γ��� ������ ����
            if (patienceGauge.value >= 50)
            {
                DataManager.satisPer_Patience = 50;
            }
            else if ((patienceGauge.value >= 10) && (patienceGauge.value < 50))
            {
                DataManager.satisPer_Patience = 25;
            }
            else
            {
                //DataManager.satisPer_Patience = 0;
                //DataManager.sumSatisPer += DataManager.cusSatisPer;
                //DataManager.totalSatisPer = DataManager.sumSatisPer / DataManager.cusCount;
                //ChangeSatisPer();
                //StopCoroutine(UIManager.Inst.cor);

                //if (DataManager.totalSatisPer != 0)
                //{
                //    SceneManager.LoadScene("HallScene");
                //}
                // SceneManager.LoadScene("HallScene");
                
            }

        }
                

        if (SceneManager.GetActiveScene().buildIndex == 1)
            cusManager.cusDisappear();
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            patienceGaugeUI.SetActive(false);
            // �ʿ���...
            DataManager.satisPer_Patience = 0;
            DataManager.sumSatisPer += DataManager.cusSatisPer;
            DataManager.totalSatisPer = DataManager.sumSatisPer / DataManager.cusCount;
            ChangeSatisPer();
            StopCoroutine(UIManager.Inst.cor);
            SceneManager.LoadScene("HallScene");
        }
    }

    public void ShowReceipt()
    {
        rec_DayText.text = $"D-{29 - GameManager.Inst.dayCount}";
        rec_TodayText.text = $"{GameManager.Inst.SatisPercent_Today}%";
        rec_AllText.text = $"{DataManager.totalSatisPer}%";
        receiptBG.SetActive(true);
    }

    public void DestroyUI()
    {
        Destroy(this.gameObject);
    }

}
