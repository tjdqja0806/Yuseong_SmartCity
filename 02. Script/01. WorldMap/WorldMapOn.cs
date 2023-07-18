using Mapbox.Unity.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapOn : MonoBehaviour
{
    [SerializeField] private GameObject _worldMap;
    private float timer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (!_worldMap.activeSelf && timer < 0)
        {
            _worldMap.SetActive(true);
            gameObject.GetComponent<WorldMapOn>().enabled = false;
        }
    }
}
