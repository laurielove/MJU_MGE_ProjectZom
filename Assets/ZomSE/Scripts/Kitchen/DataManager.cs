using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager inst = null;

    [SerializeField]
    private string m_recipeText;
    public static string recipeText
    {
        get
        {
            return inst.m_recipeText;
        }

        set
        {
            inst.m_recipeText = value;
        }
    }

    [SerializeField]
    private int m_satisPer_Patience;
    public static int satisPer_Patience
    {
        get
        {
            return inst.m_satisPer_Patience;
        }

        set
        {
            inst.m_satisPer_Patience = value;
        }
    }

    [SerializeField]
    private int m_satisPer_Accuracy;
    public static int satisPer_Accuracy
    {
        get
        {
            return inst.m_satisPer_Accuracy;
        }

        set
        {
            inst.m_satisPer_Accuracy = value;
        }
    }

    [SerializeField]
    private int m_cusSatisPer;
    public static int cusSatisPer
    {
        get
        {
            return inst.m_cusSatisPer;
        }

        set
        {
            inst.m_cusSatisPer = value;
        }
    }
    [SerializeField]
    private int m_sumSatisPer;
    public static int sumSatisPer
    {
        get
        {
            return inst.m_sumSatisPer;
        }

        set
        {
            inst.m_sumSatisPer = value;
        }
    }

    [SerializeField]
    private int m_totalSatisPer;
    public static int totalSatisPer
    {
        get
        {
            return inst.m_totalSatisPer;
        }

        set
        {
            inst.m_totalSatisPer = value;
        }
    }

    [SerializeField]
    private int m_cusCount;
    public static int cusCount
    {
        get
        {
            return inst.m_cusCount;
        }

        set
        {
            inst.m_cusCount = value;
        }
    }
    [SerializeField]
    private int m_sumStar;
    public static int sumStar
    {
        get
        {
            return inst.m_sumStar;
        }

        set
        {
            inst.m_sumStar = value;
        }
    }

    [SerializeField]
    private string m_orderMain;
    public static string orderMain
    {
        get
        {
            return inst.m_orderMain;
        }

        set
        {
            inst.m_orderMain = value;
        }
    }

    [SerializeField]
    private string m_orderCook;
    public static string orderCook
    {
        get
        {
            return inst.m_orderCook;
        }

        set
        {
            inst.m_orderCook = value;
        }
    }
    [SerializeField]
    private string m_orderSub_1;
    public static string orderSub_1
    {
        get
        {
            return inst.m_orderSub_1;
        }

        set
        {
            inst.m_orderSub_1 = value;
        }
    }
    [SerializeField]
    private string m_orderSub_2;
    public static string orderSub_2
    {
        get
        {
            return inst.m_orderSub_2;
        }

        set
        {
            inst.m_orderSub_2 = value;
        }
    }
    [SerializeField]
    private string m_orderSub_3;
    public static string orderSub_3
    {
        get
        {
            return inst.m_orderSub_3;
        }

        set
        {
            inst.m_orderSub_3 = value;
        }
    }
    [SerializeField]
    private string m_orderDrink;
    public static string orderDrink
    {
        get
        {
            return inst.m_orderDrink;
        }

        set
        {
            inst.m_orderDrink = value;
        }
    }

    private void Awake()
    {
        // inst = this;
        if (inst != null)
        {
            if (inst != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }
        else  // 최초의 인스턴스 생성
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
