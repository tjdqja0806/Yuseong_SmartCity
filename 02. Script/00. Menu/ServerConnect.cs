using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ServerConnect : MonoBehaviour
{
    [HideInInspector]
    public bool isCallInfoUpdate = false;
    [HideInInspector]
    public bool isMinwonListUpdate = false;

    private CallInfoList callInfoList;
    private MinwonListList minwonListList;
    private int callInfoListCount = 0;
    private int minwonListListCount = 0;

    void Awake()
    {
        UpdateMinwonList();
        UpdateCallInfo();
    }

    public void UpdateCallInfo() { StartCoroutine(CallInfoCorourine()); }

    public void UpdateMinwonList() { StartCoroutine(MinwonListCorourine()); }

    public CallInfoGroup getCallInfo(int index) { return callInfoList.list[index]; }

    public MinwonListGroup getMinwonList(int index) { return minwonListList.list[index]; }

    public int getCallInfoListCount() { return callInfoListCount; }

    public int getMinwonListListCount() { return minwonListListCount; }

    public IEnumerator CallInfoCorourine()
    {
        WaitForSeconds wait = new WaitForSeconds(5.0f);
        while (true)
        {
            string url = "http://119.196.91.155:28080/BeaconServer/api/callList";

            WWWForm form = new WWWForm();
            form.AddField("last_time", "");

            using (UnityWebRequest request = UnityWebRequest.Post(url,form))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success) { Debug.Log(request.error); }
                else
                {
                    var downloadData = request.downloadHandler.data;
                    string jsonString = Encoding.UTF8.GetString(downloadData);
                    callInfoList = JsonUtility.FromJson<CallInfoList>(jsonString);
                    //Debug.Log("CallInfoList Count : " + callInfoList.list.Count);
                    if (callInfoList.list.Count != callInfoListCount)
                    {
                        callInfoListCount = callInfoList.list.Count;
                        isCallInfoUpdate = true;
                    }
                    else { isCallInfoUpdate = false; }
                    //Debug.Log("jsonString : " + jsonString);
                }
            }

            yield return wait;
        }
    }

    public IEnumerator MinwonListCorourine()
    {
        WaitForSeconds wait = new WaitForSeconds(5.0f);
        while (true)
        {
            string url = "http://119.196.91.155:28080/BeaconServer/api/minwonList";

            WWWForm form = new WWWForm();
            form.AddField("last_time", "");

            using (UnityWebRequest request = UnityWebRequest.Post(url, form))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success) { Debug.Log(request.error); }
                else
                {
                    var downloadData = request.downloadHandler.data;
                    string jsonString = Encoding.UTF8.GetString(downloadData);
                    minwonListList = JsonUtility.FromJson<MinwonListList>(jsonString);
                    //Debug.Log("MinwonListList Count : " + minwonListList.list.Count);
                    if (minwonListList.list.Count != minwonListListCount)
                    {
                        minwonListListCount = minwonListList.list.Count;
                        isMinwonListUpdate = true;
                    }
                    else { isMinwonListUpdate = false; }
                    //Debug.Log("jsonString : " + jsonString);
                }
            }

            yield return wait;
        }
    }

    // -------------------------------------------------------------------------------------------

    // 서버로 데이터 전송 관련
    public void UploadResult(string id, string status, string result)
    {
        StartCoroutine(UploadCorourine(id, status, result));
    }

    public IEnumerator UploadCorourine(string id, string status, string result)
    {
        string url = "http://119.196.91.155:28080/BeaconServer/api/minwonList";

        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("status", status);
        form.AddField("result", result);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success) { Debug.Log(request.error); }
        }
    }
}

[Serializable]
public struct CallInfoGroup
{
    public string createTime;
    public string updateTime;
    public int id;
    public string name;
    public string phoneNo;
    public double latitude;
    public double longitude;
}

[Serializable]
public struct MinwonListGroup
{
    public string createTime;
    public string updateTime;
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
    public string addr;
}

public class CallInfoList { public List<CallInfoGroup> list = new List<CallInfoGroup>(); }
public class MinwonListList { public List<MinwonListGroup> list = new List<MinwonListGroup>(); }