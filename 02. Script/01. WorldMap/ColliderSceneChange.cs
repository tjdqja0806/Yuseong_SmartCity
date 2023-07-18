using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSceneChange : MonoBehaviour
{
    private string plantName;

    private 
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if (hit.collider != null)
            {
                if (hit.transform.gameObject.name != null) { plantName = hit.transform.gameObject.name; }
                switch (plantName)
                {
                    case "BD_office_Yuseong_G":
                        AutoFade.LoadLevel("02. yuseong_annex", 0.5f, 0.5f, Color.black);
                        break;
                }
            }
        }
    }
}
