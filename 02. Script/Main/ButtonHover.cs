using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    public Sprite normal;
    public Sprite hover;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void _PointEnter() { button.image.sprite = hover; }

    public void _PointExit() { button.image.sprite = normal; }
}