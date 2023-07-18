using Mapbox.Unity.Map;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationCheck : MonoBehaviour
{
    [SerializeField] AbstractMap _map;
    Vector2d[] _location;

    // Start is called before the first frame update
    void Start()
    {
        _location = new Vector2d[3];
        //_location[0] = new Vector2d(36.365460, 127.324902);
        _location[0] = new Vector2d(36.369439, 127.367805);
        _location[1] = new Vector2d(36.423302, 127.388807);
        _location[2] = new Vector2d(36.352549, 127.331502);
        Debug.Log("대전 월드컵 경기장  : " + _map.GeoToWorldPosition(_location[0]));
        Debug.Log("관평동 주민센터  : " + _map.GeoToWorldPosition(_location[1]));
        Debug.Log("평생학습센터  : " + _map.GeoToWorldPosition(_location[2]));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
