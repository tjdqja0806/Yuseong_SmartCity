using UnityEngine;

public class AxisRotate : MonoBehaviour
{
    public float min = 1800;
    public float max = 1800;
    [Space]
    public bool Reverse = false;

    private float timer = 0.0f;
    private double value = 0;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 1.0f;
            value = randomValue(min, max);
        }

        if (Reverse) { transform.Rotate(Vector3.back * (float)value * Time.deltaTime * 0.9f); }
        else { transform.Rotate(Vector3.forward * (float)value * Time.deltaTime * 0.9f); }
    }

    private float randomValue(float min, float max)
    {
        float random = Random.Range(min, max);
        return Mathf.Round(random * 10.0f) / 10.0f;
    }
}