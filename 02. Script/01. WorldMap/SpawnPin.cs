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

public class SpawnPin : MonoBehaviour
{

    [SerializeField]  AbstractMap _map;
    [HideInInspector] public bool isPinDestroy = true;
    [HideInInspector] public Transform pinPos = null;
    [HideInInspector] public bool isCloseUp = false;
    [HideInInspector] public int callPinCount;
    [HideInInspector] public int pinCount;
    public PinData _marker;

    private Vector2d _location;
    private ServerConnect serverConnect;
    private int index;
    private float timer = 8f;
    private string userType;

    private void Start()
    {
        serverConnect = GameObject.Find("Menu").GetComponent<ServerConnect>();
        userType = PlayerPrefs.GetString("User_Type");
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && userType == "Manager")
        {
            timer = 5f;
            if (serverConnect.isMinwonListUpdate && pinCount == 0)
            {
                PinInstantiate();
            }

            if(serverConnect.isCallInfoUpdate && callPinCount == 0)
                CallPinInstantiate();
        }
    }

    public void PinInstantiate()
    {
        isPinDestroy = false;
        index = serverConnect.getMinwonListListCount() - 1;
        _location = new Vector2d(serverConnect.getMinwonList(index).latitude, serverConnect.getMinwonList(index).longitude);
        var instance = Instantiate(_marker);
        instance.index = serverConnect.getMinwonListListCount() - 1;
        instance.transform.localPosition = _map.GeoToWorldPosition(_location);
        instance.transform.localScale = new Vector3(3, 3, 3);
        pinPos = instance.transform;
        pinCount++;
        isCloseUp = true;
    }
    public void CallPinInstantiate()
    {
        isPinDestroy = false;
        index = serverConnect.getCallInfoListCount() - 3;
        _location = new Vector2d(serverConnect.getCallInfo(index).latitude, serverConnect.getCallInfo(index).longitude);
        var instance = Instantiate(_marker);
        instance.index = serverConnect.getCallInfoListCount() - 3;
        instance.transform.localPosition = _map.GeoToWorldPosition(_location);
        instance.transform.localScale = new Vector3(3, 3, 3);
        pinPos = instance.transform;
        callPinCount++;
        isCloseUp = true;
    }
    public void _ClickContentPinInstantiate(int num)
    {
        isPinDestroy = false;
        index = num;
        _location = new Vector2d(serverConnect.getMinwonList(index).latitude, serverConnect.getMinwonList(index).longitude);
        var instance = Instantiate(_marker);
        instance.index = num;
        instance.transform.localPosition = _map.GeoToWorldPosition(_location);
        instance.transform.localScale = new Vector3(3, 3, 3);
        pinPos = instance.transform;
        pinCount++;
        isCloseUp = true;
    }
}
