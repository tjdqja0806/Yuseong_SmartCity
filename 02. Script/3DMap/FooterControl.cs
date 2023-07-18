using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FooterControl : MonoBehaviour
{
    [Serializable]
    public struct MainGroup
    {
        public Image image;
    }

    [Serializable]
    public struct SubGroup
    {
        public GameObject group;
        public Image image;
        public GameObject signGroup;
        [HideInInspector]
        public OrganizationControl organizationControl;
    }

    public Color nomarlColor;
    public Color overColor;
    [Space]
    public MainGroup[] mains;
    [Space]
    public SubGroup[] subs;

    private int indexMain = 1;
    private int index = 99;
    private DescriptionControl descriptionControl;
    private BuildingAnimation buildingAnimation;

    void Start()
    {
        descriptionControl = GameObject.Find("Description Group").GetComponent<DescriptionControl>();
        buildingAnimation = GameObject.Find("yuseong_annex_G").GetComponent<BuildingAnimation>();

        for (int i = 0; i < subs.Length; i++)
        {
            subs[i].organizationControl = subs[i].group.GetComponent<OrganizationControl>();
            subs[i].image.color = nomarlColor;
        }
        _ClickOffice();
    }

    void Update()
    {
        for (int i = 0; i < mains.Length; i++)
        {
            if (i == indexMain)
            {
                mains[i].image.color = overColor;
            }
        }

        for (int i = 0; i < subs.Length; i++)
        {
            if (i == index)
            {
                subs[i].image.color = overColor;
                subs[i].group.SetActive(true);
            }
            else
            {
                subs[i].group.SetActive(false);
            }
        }
    }

    public void _ClickFloor(int num)
    {
        descriptionControl.description.SetActive(false);
        for (int i = 0; i < subs.Length; i++)
        {
            subs[i].organizationControl.resetIndex();
            subs[i].signGroup.SetActive(false);
        }

        if (num == index) { index = 99; }
        else { index = num; }

        for (int i = 0; i < subs.Length; i++)
        {
            if (i != index)
            {
                subs[i].image.color = nomarlColor;
            }
        }

        if (index < subs.Length)
        {
            buildingAnimation._ClickFloor();
            subs[index].signGroup.SetActive(true);
        }

        indexMain = 99;
        for (int i = 0; i < mains.Length; i++)
        {
            if (i != indexMain)
            {
                mains[i].image.color = nomarlColor;
            }
        }
    }

    public void ResetSignGroup()
    {
        for (int i = 0; i < subs.Length; i++)
        {
            subs[i].signGroup.SetActive(false);
        }
    }

    public void _ClickOffice()
    {
        _ClickFloor(99);
        buildingAnimation._ClickOffice();
        indexMain = 0;
        for (int i = 0; i < mains.Length; i++)
        {
            if (i != indexMain)
            {
                mains[i].image.color = nomarlColor;
            }
        }
    }

    public void _ClickBuilding()
    {
        _ClickFloor(99);
        buildingAnimation._ClickBuilding();
        indexMain = 1;
        for (int i = 0; i < mains.Length; i++)
        {
            if (i != indexMain)
            {
                mains[i].image.color = nomarlColor;
            }
        }
    }
}