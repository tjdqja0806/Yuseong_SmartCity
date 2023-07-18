using System;
using TMPro;
using UnityEngine;

public class CurrentDate : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update() { text.text = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"); }
}