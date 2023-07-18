using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other != null)
        {
            if(gameObject.tag != "3DModel")
            {
                Destroy(other.gameObject);
            }
            //Debug.Log(other.gameObject.name);
        }
    }
}
