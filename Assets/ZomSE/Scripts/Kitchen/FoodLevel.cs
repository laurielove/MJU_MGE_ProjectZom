using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodLevel : MonoBehaviour
{
    [SerializeField]
    private int openLevel;

    [SerializeField]
    private GameObject food;

    private void Awake()
    {
        if (GameManager.Inst.currentWeek >= openLevel)
            gameObject.SetActive(false);
        else
            food.SetActive(false);
    }
}
