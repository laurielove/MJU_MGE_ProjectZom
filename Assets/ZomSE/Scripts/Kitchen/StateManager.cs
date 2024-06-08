using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManger : MonoBehaviour
{
    
    private static StateManger inst = null;
    // private static StateManger instance;
    // public static StateManger Inst => instance;

    [SerializeField]
    private bool m_grillState;
    public static bool grillState
    {
        get
        {
            return inst.m_grillState;
        }

        set
        {
            inst.m_grillState = value;
        }
    }
    [SerializeField]
    private bool m_pan01State;
    public static bool pan01State
    {
        get
        {
            return inst.m_pan01State;
        }

        set
        {
            inst.m_pan01State = value;
        }
    }
    [SerializeField]
    private bool m_pan02State;
    public static bool pan02State
    {
        get
        {
            return inst.m_pan02State;
        }

        set
        {
            inst.m_pan02State = value;
        }
    }
    [SerializeField]
    private bool m_pan03State;
    public static bool pan03State
    {
        get
        {
            return inst.m_pan03State;
        }

        set
        {
            inst.m_pan03State = value;
        }
    }

    [SerializeField]
    private GameObject m_FireObject;
    public static GameObject FireObject
    {
        get
        {
            return inst.m_FireObject;
        }

        set
        {
            inst.m_FireObject = value;
        }
    }
    [SerializeField]
    private GameObject m_Pan01Object;
    public static GameObject Pan01Object
    {
        get
        {
            return inst.m_Pan01Object;
        }

        set
        {
            inst.m_Pan01Object = value;
        }
    }
    [SerializeField]
    private GameObject m_Pan02Object;
    public static GameObject Pan02Object
    {
        get
        {
            return inst.m_Pan02Object;
        }

        set
        {
            inst.m_Pan02Object = value;
        }
    }
    [SerializeField]
    private GameObject m_Pan03Object;
    public static GameObject Pan03Object
    {
        get
        {
            return inst.m_Pan03Object;
        }

        set
        {
            inst.m_Pan03Object = value;
        }
    }

    [SerializeField]
    private GameObject m_p_mainObject;
    public static GameObject p_mainObject
    {
        get
        {
            return inst.m_p_mainObject;
        }

        set
        {
            inst.m_p_mainObject = value;
        }
    }
    [SerializeField]
    private GameObject m_p_sub01Object;
    public static GameObject p_sub01Object
    {
        get
        {
            return inst.m_p_sub01Object;
        }

        set
        {
            inst.m_p_sub01Object = value;
        }
    }
    [SerializeField]
    private GameObject m_p_sub02Object;
    public static GameObject p_sub02Object
    {
        get
        {
            return inst.m_p_sub02Object;
        }

        set
        {
            inst.m_p_sub02Object = value;
        }
    }
    [SerializeField]
    private GameObject m_p_sub03Object;
    public static GameObject p_sub03Object
    {
        get
        {
            return inst.m_p_sub03Object;
        }

        set
        {
            inst.m_p_sub03Object = value;
        }
    }
    [SerializeField]
    private GameObject m_p_drinkObject;
    public static GameObject p_drinkObject
    {
        get
        {
            return inst.m_p_drinkObject;
        }

        set
        {
            inst.m_p_drinkObject = value;
        }
    }

    // [SerializeField]
    // private string m_mainCookState;
    // public static string mainCookState
    // {
    //     get
    //     {
    //         return inst.m_mainCookState;
    //     }

    //     set
    //     {
    //         inst.m_mainCookState = value;
    //     }
    // }

    // [SerializeField]
    // private string m_sub01CookState;
    // public static string sub01CookState
    // {
    //     get
    //     {
    //         return inst.m_sub01CookState;
    //     }

    //     set
    //     {
    //         inst.m_sub01CookState = value;
    //     }
    // }
    // [SerializeField]
    // private string m_sub02CookState;
    // public static string sub02CookState
    // {
    //     get
    //     {
    //         return inst.m_sub02CookState;
    //     }

    //     set
    //     {
    //         inst.m_sub02CookState = value;
    //     }
    // }
    // [SerializeField]
    // private string m_sub03CookState;
    // public static string sub03CookState
    // {
    //     get
    //     {
    //         return inst.m_sub03CookState;
    //     }

    //     set
    //     {
    //         inst.m_sub03CookState = value;
    //     }
    // }


    

    private void Awake()
    {
        inst = this;

    }

    // private enum MainState
    // {
    //     None,
    //     Rare,
    //     Medium,
    //     WellDone,
    //     Burnt
    // }

    // private enum SubState
    // {
    //     None,
    //     Done
    // }
    
}
