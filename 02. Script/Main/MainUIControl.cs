using UnityEngine;

public class MainUIControl : MonoBehaviour
{
    public GameObject mainUI;

    public void _Click3DMap() { mainUI.SetActive(false); }

    public void _ClickMain() { mainUI.SetActive(true); }

    public void _ClickDistrictOffice() { Application.OpenURL("https://www.yuseong.go.kr/kor/"); }

    public void _ClickTourism() { Application.OpenURL("https://www.yuseong.go.kr/tour/"); }
}