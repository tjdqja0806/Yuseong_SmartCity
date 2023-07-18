using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;
using TMPro;
using UnityEngine.UI;
using System;

public class SpawnTest : MonoBehaviour
{

    [SerializeField]  AbstractMap _map;

    public string dateText;
    public string typeText;
    public string statusText;
    public string longitude;
    public string latitude;
    [HideInInspector] public string locationText; //위도 + 경도
    [HideInInspector] public string contentText;//SUB
    [HideInInspector] public string imageURL; //imageURL

    [HideInInspector] public bool isPinDestroy = true;
     public bool isCloseUp = false;
    [HideInInspector] public Transform pinPos = null;

    Vector2d _location;

    [SerializeField] WorldMapPinClick _marker;
    public void PinInstantiate()
    {
        isPinDestroy = false;
        _location = new Vector2d(Double.Parse(latitude), Double.Parse(longitude));
        var instance = Instantiate(_marker);
        instance.transform.localPosition = _map.GeoToWorldPosition(_location);
        instance.transform.localScale = new Vector3(3, 3, 3);
        pinPos = instance.transform;
        instance.dateText.text = dateText;
        instance.typeText.text = typeText;
        instance.contentText = contentText;
        switch (statusText)
        {
            case "0":
                instance.statusText.text = "접수중";
                break;
            case "1":
                instance.statusText.text = "접수완료";
                break;
            case "2":
                instance.statusText.text = "민원처리완료";
                break;
        }
        isCloseUp = true;
    }

    public void KaistPinInstantiate()
    {
        latitude = "36.369439";
        longitude = "127.367805";
        PinInstantiate();
    }
    public void Hanbat()
    {
        latitude = "36.351709";
        longitude = "127.3008675";
        PinInstantiate();
    }

    public void DestroyPin()
    {
        isPinDestroy = true;
    }
}
