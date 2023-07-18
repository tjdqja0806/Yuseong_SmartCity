using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuListControl : MonoBehaviour
{
    public GameObject[] UIList;
    private bool[] isListBool;
    private MemuController menuControl;

    void Awake()
    {
        menuControl = GetComponent<MemuController>();
    }
    void Start()
    {
        isListBool = new bool[UIList.Length];
    }

    public void _MenuListClick(int num)
    {
        isListBool[num] = !isListBool[num];
        UIList[num].SetActive(isListBool[num]);
    }
}
