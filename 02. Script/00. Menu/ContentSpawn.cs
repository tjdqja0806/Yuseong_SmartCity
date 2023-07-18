using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ContentSpawn : MonoBehaviour
{

    public RectTransform parent;
    public ContentPrefab contentPrefab;
    private float timer = 1.0f;
    private ServerConnect serverConnect;
    void Start()
    {
        serverConnect = GameObject.Find("Menu").GetComponent<ServerConnect>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 5.0f;
            if (serverConnect.isMinwonListUpdate)
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
        for (int j = 0; j <= (serverConnect.getMinwonListListCount() - 1); j++)
        {
            var instance = Instantiate(contentPrefab);
            instance.transform.SetParent(parent, false);
            instance.index = j;
        }
    }
}
