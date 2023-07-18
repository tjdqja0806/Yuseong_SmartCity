using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIControl : MonoBehaviour
{
    public GameObject[] UI;

    private bool[] isBool;
    void Start()
    {
        isBool = new bool[UI.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void _ClickConstructionPin(int num)
    {
        isBool[num] = !isBool[num];
        UI[num].SetActive(isBool[num]);
    }
}
