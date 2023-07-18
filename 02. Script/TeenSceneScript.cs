using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeenSceneScript : MonoBehaviour
{
    public Image background;
    public Sprite[] backgroundSprite;

    private int status = 0;

    void Update()
    {
        background.sprite = backgroundSprite[status];
        if (status == 0)
            background.raycastTarget = false;
        else
            background.raycastTarget = true;
    }

    public void UpClick() 
    { 
        status++;
        if (status >= backgroundSprite.Length)
            status = 0;
    }
    public void DownClick()  
    { 
        status--;
        if (status < 0)
            status = 4;
    }
}
