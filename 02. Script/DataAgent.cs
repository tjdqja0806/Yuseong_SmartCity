using SmartMiddleware;
using UnityEngine;

public class DataAgent : MonoBehaviour
{
    private float timer = 0.0f;
    [HideInInspector]
    public bool isAuto;

    void Start()
    {
        isAuto = false;
        if (!isAuto) { DataService.Init(); }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 1.0f;
            if (!isAuto) { DataService.UpdateData(); }
        }
    }

    public double ReadData(int index)
    {
        double temp = 0;
        switch (index)
        {
            case 0:
                temp = DataService.Temperature;
                break;

            case 1:
                temp = DataService.Humidity;
                break;

            case 2:
                temp = DataService.PM10;
                break;

            case 3:
                temp = DataService.PM25;
                break;

            case 4:
                temp = DataService.CO2;
                break;

            case 5:
                temp = DataService.VOCs;
                break;
        }
        return temp;
    }

    public bool DeviceStatus() { return DataService.ActiveDevice; }
}