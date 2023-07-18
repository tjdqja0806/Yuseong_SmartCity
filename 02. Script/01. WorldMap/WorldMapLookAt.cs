using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapLookAt : MonoBehaviour
{
    private GameObject target;
    private Vector3 targetPosition;

    private float distance;
    private float defaultScale = 0.15f;
    private float defaultDistance = 59.25891f;
    private float scale;

    private void Start()
    {
        target = Camera.main.gameObject;
    }
    void Update()
    {
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        scale = distance * defaultScale / defaultDistance * 0.6f;
        transform.localScale = new Vector3(scale, scale, 1);
        targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

        transform.LookAt(targetPosition);

    }
}
