using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MeetingRoomVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private bool isPlay;
    void Start()
    {
        
    }


    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if (other.gameObject.name == "PlayerCapsule")
                videoPlayer.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.name == "PlayerCapsule")
                videoPlayer.Pause();
        }
    }
}
