using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    public float startTimeBtwSpawns;
    public GameObject echo;
    public bool is1F = false;


    private float timeBtwSpawns = 0.1f;
    private float rotY = 0;

    //private PathPlay pathPlay;
    private void Awake()
    {
        //pathPlay = GameObject.Find("Animation Group").GetComponent<PathPlay>();
    }

    void Update()
    {
        if (timeBtwSpawns <= 0)
        {
            if (is1F) { rotY = transform.localEulerAngles.y; }
            else { rotY = transform.localEulerAngles.y + 90; }
            GameObject instance = Instantiate(echo, transform.position, Quaternion.Euler(90, rotY, transform.localEulerAngles.z));
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}