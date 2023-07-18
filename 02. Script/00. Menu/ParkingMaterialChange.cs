using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingMaterialChange : MonoBehaviour
{
    public Material parkingMaterial;

    private float timer = 1.0f;
    private bool isChange;

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            timer = 1f;
            if (isChange)
                isChange = false;
            else
                isChange = true;
        }
        if (isChange)
            parkingMaterial.color = Color.white;
        else
            parkingMaterial.color = Color.green;
    }
}
