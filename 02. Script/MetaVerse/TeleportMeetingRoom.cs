using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMeetingRoom : MonoBehaviour
{
    public Transform meetingRoomPos;
    public GameObject player;

    public void Teleport()
    {
        player.transform.position = meetingRoomPos.position;
        player.transform.rotation = meetingRoomPos.rotation;
    }
}
