using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPin : MonoBehaviour
{
    private SpawnTest spawnTest;
    void Start()
    {
        spawnTest = GameObject.Find("EventSystem").GetComponent<SpawnTest>();
    }

    void Update()
    {
        if (spawnTest.isPinDestroy)
            Destroy(gameObject);
    }
}
