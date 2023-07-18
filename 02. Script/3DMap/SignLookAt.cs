using UnityEngine;

public class SignLookAt : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}