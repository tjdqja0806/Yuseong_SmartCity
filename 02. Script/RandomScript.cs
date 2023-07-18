using TMPro;
using UnityEngine;

public class RandomScript : MonoBehaviour
{
    public TextMeshProUGUI text;

    private float timer = 0.0f;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 1.0f;            
            text.text = float.Parse(string.Format("{0:0}", randomValue(0, 100))) + "";
        }
    }

    private float randomValue(float min, float max)
    {
        float random = Random.Range(min, max);
        return Mathf.Round(random * 10.0f) / 10.0f;
    }
}