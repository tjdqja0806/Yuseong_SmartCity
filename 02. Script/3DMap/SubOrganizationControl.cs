using System;
using TMPro;
using UnityEngine;

public class SubOrganizationControl : MonoBehaviour
{
    [Serializable]
    public struct SubGroup
    {
        public TextMeshProUGUI text;
        public SignControl signControl;
    }
    public Color nomarlColor;
    public Color overColor;
    [Space]
    public SubGroup[] subs;

    private int index = 99;
    private DescriptionControl descriptionControl;

    void Start()
    {
        descriptionControl = GameObject.Find("Description Group").GetComponent<DescriptionControl>();
    }

    void Update()
    {
        for (int i = 0; i < subs.Length; i++)
        {
            if (i == index)
            {
                subs[i].text.color = overColor;
                descriptionControl.routeStatus = i;
                subs[i].signControl.isAni = true;
            }
        }
    }

    public void _ClickSub(int num)
    {
        if (num == index)
        {
            index = 99;
            descriptionControl.buttonGroup.SetActive(false);
        }
        else
        {
            index = num;
            descriptionControl.buttonGroup.SetActive(true);
        }

        for (int i = 0; i < subs.Length; i++)
        {
            if (i != index)
            {
                subs[i].text.color = nomarlColor;
                subs[i].signControl.isAni = false;
            }
        }
    }

    public void resetIndex()
    {
        index = 99;
        if (descriptionControl != null) { descriptionControl.buttonGroup.SetActive(false); }

        for (int i = 0; i < subs.Length; i++)
        {
            subs[i].text.color = nomarlColor;
            subs[i].signControl.isAni = false;
        }
    }
}