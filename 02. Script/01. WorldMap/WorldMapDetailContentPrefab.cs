using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WorldMapDetailContentPrefab : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI locationText; //위도 + 경도
    public TextMeshProUGUI contentText; // sub
    public RawImage contentRawImage;
    public Image statusImage;
    public TMP_InputField memoInputText;
    [HideInInspector]
    public int index;

    private SpawnPin spawnPin;
    private ServerConnect serverConnect;
    private MinwonListGroup group;
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
    void Update()
    {

    }

    private void updateData()
    {
        group = serverConnect.getMinwonList(index);

        dateText.text = group.createTime;
        typeText.text = group.title;
        locationText.text = group.addr;
        contentText.text = group.sub;
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

        ImageLoad(group.photoUrl);
    }

    public void CloseClick()
    {
        spawnPin.isPinDestroy = true;
        cameraMove.isDetailContent = false;
        spawnPin.pinCount = 0;
        Destroy(gameObject);
    }

    // -------------------------------------------------------------------------------------------

    // 이미지 관련
    public void ImageLoad(string url)
    {
        StartCoroutine(ImageLoadCorourine(url));
    }

    public IEnumerator ImageLoadCorourine(string imageURL)
    {
        if (imageURL != null)
        {
            string url = "http://119.196.91.155:28080" + imageURL;
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success) { Debug.Log(request.error); }
                else { contentRawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture; }
            }
        }
    }

    public void _ClickDepartmentSelection()
    {
        menuDepartmentSelection._ClickDepartmentSelection();
    }
}
