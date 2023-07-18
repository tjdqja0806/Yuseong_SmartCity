using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Text;

[Serializable]
public struct list_Detail_call
{
    public int id;
    public string name;
    public string phoneNo;
    public double latitude;
    public double longitude;
    public string createTime;
    public string updateTime;
}
[Serializable]
public struct list_Detail
{
    public int id;
    public int status;
    public string name;
    public string phoneNo;
    public string title;
    public string sub;
    public string result;
    public string photoUrl;
    public string photoThumbUrl;
    public double latitude;
    public double longitude;
    public string createTime;
    public string updateTime;
}
[HideInInspector]
public class MemberData_call
{
    public List<list_Detail_call> list_call = new List<list_Detail_call>();
}
[HideInInspector]
public class MemberData
{
    public List<list_Detail> list = new List<list_Detail>();
}


public class DataUpload : MonoBehaviour
{
    [HideInInspector]
    public string Last_time;
    [HideInInspector]
    public string Save_time;
    private string download;
    public List<list_Detail_call> list_call = new List<list_Detail_call>();
    public List<list_Detail> list = new List<list_Detail>();
    public MemberData memberData_call;
    public MemberData memberData;

    public SpawnTest spawnTest;
    void Start()
    {
        spawnTest = GameObject.Find("EventSystem").GetComponent<SpawnTest>();
        //StartCoroutine(Postdata_Corourine_call("20210830130203"));
        //StartCoroutine(Postdata_Corourine("20210829130203"));
    }

    void Update()
    {

    }

    IEnumerator Postdata_Corourine_call(string last_time)
    {
        //URL = 데이터를 보낼 URL
        string url = "http://my.jinzza.kr:28080/BeaconServer/api/callInfo";
        //form = URL에 데이터를 보낼 변수들
        WWWForm form = new WWWForm();
        //form 요청할 데이터 추가
        //form.AddField("KEY값","VAlUE값")
        form.AddField("last_time", last_time);
        //nityWebRequest형식의 request를 만들어 url, form(요청할 데이터)과 함께 변수에 넣어줌
        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            //request의 데이터 요청을 url에 보냄.
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError) //에러나면 에러표시
            {
                Debug.Log(request.error);
            }
            else
            {
                var Downdata = request.downloadHandler.data;
                string jsonData = Encoding.UTF8.GetString(Downdata); // 받아오는 데이터 downdata에 string형식으로 저장
                MemberData memberData_call = JsonUtility.FromJson<MemberData>(jsonData); //jsondata를 MemverData형식의 memberdata에 저장
                Debug.Log("name : " + memberData_call.list[1].name);
            }
        }
    }
    public IEnumerator Postdata_Corourine(string last_time)
    {
        //URL = 데이터를 보낼 URL
        string url = "http://my.jinzza.kr:28080/BeaconServer/api/minwonList";
        //form = URL에 데이터를 보낼 변수들
        WWWForm form = new WWWForm();
        //form 요청할 데이터 추가
        //form.AddField("KEY값","VAlUE값")
        form.AddField("last_time", last_time);
        //nityWebRequest형식의 request를 만들어 url, form(요청할 데이터)과 함께 변수에 넣어줌
        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            //request의 데이터 요청을 url에 보냄.
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError) //에러나면 에러표시
            {
                Debug.Log(request.error);
            }
            else
            {
                var Downdata_call = request.downloadHandler.data;
                string jsonData_call = Encoding.UTF8.GetString(Downdata_call); // 받아오는 데이터 downdata에 string형식으로 저장
                memberData = JsonUtility.FromJson<MemberData>(jsonData_call); //jsondata를 MemverData형식의 memberdata에 저장
                Debug.Log("name : " + memberData.list[0].name);
            }
            spawnTest.longitude = memberData.list[0].longitude.ToString();
            spawnTest.latitude = memberData.list[0].latitude.ToString();
            spawnTest.dateText = memberData.list[0].createTime;
            spawnTest.typeText = memberData.list[0].title;
            spawnTest.statusText = memberData.list[0].status.ToString();
            spawnTest.contentText = memberData.list[0].sub;
            spawnTest.imageURL = memberData.list[0].photoThumbUrl;
            spawnTest.PinInstantiate();
        }
    }
    public void TestClick()
    {
        StartCoroutine(Postdata_Corourine("20210829130203"));
    }
}
