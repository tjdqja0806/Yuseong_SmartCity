using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallContentSpawn : MonoBehaviour
{
    public RectTransform parent;
    public CallContentPrefab contentPrefab;
    private float timer = 1.0f;
    private ServerConnect serverConnect;
    void Awake()
    {
        serverConnect = GameObject.Find("Menu").GetComponent<ServerConnect>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 5.0f;
            if (serverConnect.isCallInfoUpdate)
            {
                ContentAdd();
            }
        }
    }

    void ContentAdd()
    {
        var contents = GameObject.FindGameObjectsWithTag("Complaint Content");
        foreach (var content in contents)
        {
            Destroy(content);
        }
        for (int j = 0; j <= (serverConnect.getCallInfoListCount() - 1); j++)
        {
            var instance = Instantiate(contentPrefab);
            instance.transform.SetParent(parent, false);
            instance.index = j;
        }
    }
}
