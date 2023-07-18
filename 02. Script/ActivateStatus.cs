using UnityEngine;

public class ActivateStatus : MonoBehaviour
{
    public GameObject text;

    private float timer = 0.0f;
    private bool isActive = false;

    void Start()
    {

    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 0.5f;
            isActive = !isActive;
            text.SetActive(isActive);
        }
    }
}