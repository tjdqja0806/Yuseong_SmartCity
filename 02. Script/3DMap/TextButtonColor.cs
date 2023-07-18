using TMPro;
using UnityEngine;

public class TextButtonColor : MonoBehaviour
{
    public Color nomarlColor;
    public Color overColor;
    [Space]
    public TextMeshProUGUI text;

    public void _PointerEnter() { text.color = overColor; }

    public void _PointerExit() { text.color = nomarlColor; }
}