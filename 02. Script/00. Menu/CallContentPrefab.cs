using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CallContentPrefab : MonoBehaviour
{
    public TextMeshProUGUI date;
    public TextMeshProUGUI type;
    public TextMeshProUGUI status;
    public Image statusImage;
    [HideInInspector]
    public int index;

    private ServerConnect serverConnect;
    private CallInfoGroup group;
    private Color[] statusColor = new Color[3];
    private float timer = 3.0f;

    void Start()
    {
        serverConnect = GameObject.Find("Menu").GetComponent<ServerConnect>();
        statusColor[0] = new Color(0.9803922f, 0.6862745f, 0.2235294f, 1);//orange
        statusColor[1] = new Color(0.2196079f, 0.7058824f, 0.2862745f, 1);//green
        statusColor[2] = new Color(0.0509804f, 0.4470589f, 0.7254902f, 1);//blue
        updateData();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 5.0f;
            updateData();
        }
    }

    private void updateData()
    {

        group = serverConnect.getCallInfo(index);
        date.text = group.createTime;
        type.text = "-";
        statusImage.color = statusColor[0];
        status.text = "접수중";
        /*switch (group.status.ToString())
        {
            case "0":
                statusImage.color = statusColor[0];
                status.text = "접수중";
                break;
            case "1":
                statusImage.color = statusColor[1];
                status.text = "접수완료";
                break;
            case "2":
                statusImage.color = statusColor[2];
                status.text = "민원처리완료";
                break;
        }*/
    }
}
