using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuLineNewsText : MonoBehaviour
{
    public GameObject[] lineNewsText;
    private int lineNewsStatus;
    private float lineNewsTimer = 10f;
    void Start()
    {
        //숫자 입력 시 text 굵게 색 변경<b><#FF0000> </color></b>
    }

    // Update is called once per frame
    void Update()
    {
        lineNewsTimer -= Time.deltaTime;
        if (lineNewsTimer <= 0)
        {
            lineNewsTimer = 10f;
            lineNewsStatus++;
            if (lineNewsStatus > lineNewsText.Length - 1)
                lineNewsStatus = 0;
            for(int i = 0; i < lineNewsText.Length; i++)
            {
                if (i == lineNewsStatus)
                    lineNewsText[i].SetActive(true);
                else
                    lineNewsText[i].SetActive(false);
            }
        }
    }
}
