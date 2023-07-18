using System;
using TMPro;
using UnityEngine;

public class OrganizationControl : MonoBehaviour
{
    [Serializable]
    public struct SubGroup
    {
        public GameObject group;
        public TextMeshProUGUI text;
        [HideInInspector]
        public SubOrganizationControl subOrganizationControl;
        public GameObject roomGuide;
    }
    public Color nomarlColor;
    public Color overColor;
    [Space]
    public SubGroup[] subs;
    [Space]
    public GameObject signGroup;
    [HideInInspector]
    public int index = 99;
    [HideInInspector]
    public bool isGuide = false;

    private BuildingAnimation buildingAnimation;
    private DescriptionControl descriptionControl;
    private bool isRoom = false;
    private float timer = 0.0f;

    void Start()
    {
        descriptionControl = GameObject.Find("Description Group").GetComponent<DescriptionControl>();
        buildingAnimation = GameObject.Find("yuseong_annex_G").GetComponent<BuildingAnimation>();

        for (int i = 0; i < subs.Length; i++)
        {
            if (subs[i].group.GetComponent<SubOrganizationControl>() != null)
            {
                subs[i].subOrganizationControl = subs[i].group.GetComponent<SubOrganizationControl>();
            }
            else { subs[i].subOrganizationControl = null; }
        }
    }

    void Update()
    {
        for (int i = 0; i < subs.Length; i++)
        {
            if (i == index)
            {
                subs[i].text.color = overColor;
                subs[i].group.SetActive(true);
            }
            else
            {
                subs[i].group.SetActive(false);
                subs[i].roomGuide.SetActive(false);
            }
        }

        timer -= Time.deltaTime;
        if (timer <= 0.0f && index < subs.Length && isGuide)
        {
            timer = 0.5f;
            isRoom = !isRoom;
            subs[index].roomGuide.SetActive(isRoom);
        }

        if ((index != 99) && !isGuide) { subs[index].roomGuide.SetActive(false); }
    }

    public void _ClickSub(int num)
    {
        for (int i = 0; i < subs.Length; i++)
        {
            if (subs[i].subOrganizationControl != null)
            {
                subs[i].subOrganizationControl.resetIndex();
            }
        }

        if (num == index)
        {
            index = 99;
            isGuide = false;
            subs[num].roomGuide.SetActive(false);
            descriptionControl.description.SetActive(false);
        }
        else
        {
            index = num;
            descriptionControl.description.SetActive(true);
        }

        for (int i = 0; i < subs.Length; i++)
        {
            if (i != index) { subs[i].text.color = nomarlColor; }
        }

        buildingAnimation._ClickFloor();
        signGroup.SetActive(true);
        isGuide = true;
    }

    public void resetIndex()
    {
        index = 99;

        for (int i = 0; i < subs.Length; i++)
        {
            subs[i].text.color = nomarlColor;
            subs[i].group.SetActive(false);
            subs[i].roomGuide.SetActive(false);
            isGuide = false;
        }
    }

    public void _ChangeDescription(string index) { descriptionControl.ChangeDescription(index); }
}