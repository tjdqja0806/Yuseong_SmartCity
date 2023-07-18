using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapPinClick : MonoBehaviour
{
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI statusText;
    [HideInInspector] public string locationText; //위도 + 경도
    [HideInInspector] public string contentText; // sub
    [SerializeField] Image statusImage;
    [Header("DetailContent Prefab")]
    [SerializeField] WorldMapDetailContentPrefab detailContentUI;

    Color[] statusColor = new Color[3];
    
    private SpawnTest spawnTest;

    void Awake()
    {
        spawnTest = GameObject.Find("EventSystem").GetComponent<SpawnTest>();
    }
    void Start()
    {
        statusColor[0] = new Color(0.9803922f, 0.6862745f, 0.2235294f, 1);//orange
        statusColor[1] = new Color(0.2196079f, 0.7058824f, 0.2862745f, 1);//green
        statusColor[2] = new Color(0.0509804f, 0.4470589f, 0.7254902f, 1);//blue
        switch (statusText.text)
        {
            case "접수중":
                statusImage.color = statusColor[0];
                break;
            case "접수완료":
                statusImage.color = statusColor[1];
                break;
            case "민원처리완료":
                statusImage.color = statusColor[2];
                break;
        }
    }
    void Update()
    {
        if (spawnTest.isPinDestroy)
            Destroy(gameObject);
    }

    public void SignClick()
    {
        var instance = Instantiate(detailContentUI);
        detailContentUI.dateText.text = dateText.text;
        detailContentUI.typeText.text = typeText.text;
        detailContentUI.statusText.text = statusText.text;
        detailContentUI.statusImage.color = statusImage.color;
    }
}
