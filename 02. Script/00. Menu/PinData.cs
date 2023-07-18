using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PinData : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI statusText;
    public Image statusImage;
    public WorldMapDetailContentPrefab detailContentUI;
    public WorldMapCallDetailContent callDetailContentUI;

    [HideInInspector] public int index;
    [HideInInspector] public string contentText; // sub

    private SpawnPin spawnPin;
    private ServerConnect serverConnect;
    private CallInfoGroup callGroup;
    private MinwonListGroup group;
    private Color[] statusColor = new Color[3];
    private float timer = 1.0f;

    void Awake()
    {
        spawnPin = GameObject.Find("EventSystem").GetComponent<SpawnPin>();
        serverConnect = GameObject.Find("Menu").GetComponent<ServerConnect>();
        statusColor[0] = new Color(0.9803922f, 0.6862745f, 0.2235294f, 1);//orange
        statusColor[1] = new Color(0.2196079f, 0.7058824f, 0.2862745f, 1);//green
        statusColor[2] = new Color(0.0509804f, 0.4470589f, 0.7254902f, 1);//blue
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 5.0f;
            updateData();
        }
        if (spawnPin.isPinDestroy)
            Destroy(gameObject);
    }

    private void updateData()
    {
        if (spawnPin.pinCount == 1)
        {
            group = serverConnect.getMinwonList(index);
            dateText.text = group.createTime;
            typeText.text = group.title;
            //locationText.text = "주소";
            switch (group.status.ToString())
            {
                case "0":
                    statusImage.color = statusColor[0];
                    statusText.text = "접수중";
                    break;
                case "1":
                    statusImage.color = statusColor[1];
                    statusText.text = "접수완료";
                    break;
                case "2":
                    statusImage.color = statusColor[2];
                    statusText.text = "민원처리완료";
                    break;
            }
        }
        if (spawnPin.callPinCount == 1)
        {
            callGroup = serverConnect.getCallInfo(index);
            dateText.text = callGroup.createTime;
            typeText.text = "-";
            statusImage.color = statusColor[0];
            statusText.text = "접수중";
        }
    }

    public void SignClick()
    {
        if (spawnPin.pinCount == 1)
        {
            var instance = Instantiate(detailContentUI);
            instance.index = index;
        }
        if (spawnPin.callPinCount == 1)
        {
            var instance = Instantiate(callDetailContentUI);
            instance.index = index;
        }
    }
}