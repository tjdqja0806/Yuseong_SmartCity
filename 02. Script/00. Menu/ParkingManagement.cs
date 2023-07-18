using UnityEngine;

public class ParkingManagement : MonoBehaviour
{
    public GameObject parkingUI;
    public GameObject pointGroup;

    private bool isActive = false;

    void Start()
    {

    }

    void Update()
    {
        parkingUI.SetActive(isActive);
        pointGroup.SetActive(isActive);
    }

    public void _ClickParkingButton() { isActive = !isActive; }
}