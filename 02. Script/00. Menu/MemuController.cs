 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MemuController : MonoBehaviour
{
    //UI ¼ø¼­
    /*public struct GameObjectGroup
    {
        public GameObject emergencyFireAlarm;
        public GameObject complaintsStas;
        public GameObject BuildingInfoPin;
        public GameObject tourismInfo;
        public GameObject trafficInfo;
        public GameObject cctvUI;
        public GameObject constructionInfo;
        public GameObject trafficAccidentInfo;
        public GameObject publicInstitutionInfo;
        public GameObject Complaintstatus;
        public GameObject LocalInfo; == 13
    }*/


    //PopUp UI
    public GameObject[] ui;
    [HideInInspector] public bool[] uiBool;
    [HideInInspector] public int uiStatus;
    [HideInInspector] public bool isWaterColseUp;

    //DropDown
    public GameObject[] sideDropdown;
    private bool[] isDropdown;

    //BuildingInfo
    public GameObject[] buildingInfoUI;
    [HideInInspector] public bool buildingCloseUp;
    [HideInInspector] public int buildingStatus;
    private bool[] isBuildingInfoBool;


    //Complaint Status(Mobile / Call)
    public GameObject mobileComplaint;
    public GameObject callComplaint;



    //ListUI
    public GameObject tourListUI;
    public GameObject buildingListUI;
    public GameObject localUI;
    void Start()
    {
        uiBool = new bool[ui.Length];
        isDropdown = new bool[sideDropdown.Length];
        isBuildingInfoBool= new bool[buildingInfoUI.Length];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void _ClickUI(int num)
    {
        uiStatus = num;
        uiBool[num] = !uiBool[num];
        ui[num].SetActive(uiBool[num]);
        switch (num)
        {
            case 2:
                buildingListUI.SetActive(uiBool[num]);
                break;
            case 3:
                tourListUI.SetActive(uiBool[num]);
                break;
            case 13:
                localUI.SetActive(uiBool[num]);
                break;
            case 14:
                if (uiBool[14])
                    isWaterColseUp = true;
                break;
        }
    }

    public void _ClickDropDown(int num)
    {
        isDropdown[num] = !isDropdown[num];
        sideDropdown[num].SetActive(isDropdown[num]);
    }

    public void _ClickBuildingInfo(int num)
    {
        buildingStatus = num;
        if (!isBuildingInfoBool[num])
            buildingCloseUp = true;
        isBuildingInfoBool[num] = !isBuildingInfoBool[num];
        buildingInfoUI[num].SetActive(isBuildingInfoBool[num]);

    }
    
    public void _ClickchangeConplaint(int num)
    {
        if(num == 0)
        {
            mobileComplaint.SetActive(true);
            callComplaint.SetActive(false);
        }
        else
        {
            mobileComplaint.SetActive(false);
            callComplaint.SetActive(true);
        }
    }
    public void _OpenWebSite(string url) { Application.OpenURL(url); }

    public void _ClickProgramExit() { Application.Quit(); }
}
