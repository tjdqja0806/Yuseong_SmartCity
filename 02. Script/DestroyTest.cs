using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTest : MonoBehaviour
{
    private PathPlay pathPlay;
    // Start is called before the first frame update
    void Awake()
    {
        pathPlay = GameObject.Find("Animation Group").GetComponent<PathPlay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pathPlay.isEnd)
        {
            Destroy(gameObject);
        }
    }
}
