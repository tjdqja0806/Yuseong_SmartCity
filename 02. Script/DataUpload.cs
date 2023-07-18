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
        //URL = �����͸� ���� URL
        string url = "http://my.jinzza.kr:28080/BeaconServer/api/callInfo";
        //form = URL�� �����͸� ���� ������
        WWWForm form = new WWWForm();
        //form ��û�� ������ �߰�
        //form.AddField("KEY��","VAlUE��")
        form.AddField("last_time", last_time);
        //nityWebRequest������ request�� ����� url, form(��û�� ������)�� �Բ� ������ �־���
        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            //request�� ������ ��û�� url�� ����.
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError) //�������� ����ǥ��
            {
                Debug.Log(request.error);
            }
            else
            {
                var Downdata = request.downloadHandler.data;
                string jsonData = Encoding.UTF8.GetString(Downdata); // �޾ƿ��� ������ downdata�� string�������� ����
                MemberData memberData_call = JsonUtility.FromJson<MemberData>(jsonData); //jsondata�� MemverData������ memberdata�� ����
                Debug.Log("name : " + memberData_call.list[1].name);
            }
        }
    }
    public IEnumerator Postdata_Corourine(string last_time)
    {
        //URL = �����͸� ���� URL
        string url = "http://my.jinzza.kr:28080/BeaconServer/api/minwonList";
        //form = URL�� �����͸� ���� ������
        WWWForm form = new WWWForm();
        //form ��û�� ������ �߰�
        //form.AddField("KEY��","VAlUE��")
        form.AddField("last_time", last_time);
        //nityWebRequest������ request�� ����� url, form(��û�� ������)�� �Բ� ������ �־���
        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            //request�� ������ ��û�� url�� ����.
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError) //�������� ����ǥ��
            {
                Debug.Log(request.error);
            }
            else
            {
                var Downdata_call = request.downloadHandler.data;
                string jsonData_call = Encoding.UTF8.GetString(Downdata_call); // �޾ƿ��� ������ downdata�� string�������� ����
                memberData = JsonUtility.FromJson<MemberData>(jsonData_call); //jsondata�� MemverData������ memberdata�� ����
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
