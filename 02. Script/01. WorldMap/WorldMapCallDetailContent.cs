using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapCallDetailContent : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI statusText;
    public Image statusImage;
    [HideInInspector]
    public int index;

    private SpawnPin spawnPin;
    private ServerConnect serverConnect;
    private CallInfoGroup group;
    private CameraMove cameraMove;
    private Color[] statusColor = new Color[3];
    private MenuDepartmentSelection menuDepartmentSelection;

    void Awake()
    {
        cameraMove = GameObject.Find("Main Camera").GetComponent<CameraMove>();
        serverConnect = GameObject.Find("Menu").GetComponent<ServerConnect>();
        spawnPin = GameObject.Find("EventSystem").GetComponent<SpawnPin>();
        menuDepartmentSelection = GameObject.Find("Menu").GetComponent<MenuDepartmentSelection>();
    }
    void Start()
    {
        statusColor[0] = new Color(0.9803922f, 0.6862745f, 0.2235294f, 1);//orange
        statusColor[1] = new Color(0.2196079f, 0.7058824f, 0.2862745f, 1);//green
        statusColor[2] = new Color(0.0509804f, 0.4470589f, 0.7254902f, 1);//blue
        cameraMove.isDetailContent = true;
        updateData();
    }

    private void updateData()
    {
        group = serverConnect.getCallInfo(index);

        dateText.text = group.createTime;
        statusImage.color = statusColor[0];
        statusText.text = "Á¢¼öÁß";
    }

    public void CloseClick()
    {
        spawnPin.isPinDestroy = true;
        cameraMove.isDetailContent = false;
        spawnPin.callPinCount = 0;
        Destroy(gameObject);
    }
    public void _ClickDepartmentSelection()
    {
        menuDepartmentSelection._ClickDepartmentSelection();
    }
}
