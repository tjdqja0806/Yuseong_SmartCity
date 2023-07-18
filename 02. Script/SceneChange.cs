using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void click(int index) { SceneManager.LoadScene(index); }
    public void clickSceneChange(string sceneName) 
    {
        //AutoFade.LoadLevel(sceneName, 0.5f, 0.5f, Color.black); 
        LodingBarScript.LoadScene(sceneName);
    }
}
