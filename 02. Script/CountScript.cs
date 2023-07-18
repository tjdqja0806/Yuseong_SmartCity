using TMPro;
using UnityEngine;

public class CountScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    private int count = 0;

    public void _ClickPlus()
    {
        count++;
        text.text = count + "";
    }

    public void _ClickMinus()
    {
        count--;
        text.text = count + "";
    }
}