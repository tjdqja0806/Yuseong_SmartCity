using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConnect : MonoBehaviour
{
    public GameObject player;
    public bool ConnectTrigger = true;
    private Vector3 position;
    void Start()
    {
        position = new Vector3(-16.93f, 8.2f, -32.644f);
    }
    void Update()
    {
        player.transform.position = position;
    }
}
