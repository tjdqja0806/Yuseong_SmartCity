using TMPro;
using UnityEngine;

public class TextDataScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    [Space]
    public float min = 0;
    public float max = 100;
    public int dataIndex = 0;

    private DataAgent dataAgent;
    private float timer = 0.0f;
    private double value;

    void Start()
    {
        dataAgent = GameObject.Find("EventSystem").GetComponent<DataAgent>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 1.0f;
            if (dataAgent.isAuto) { value = randomValue(min, max); }
            else { value = dataAgent.ReadData(dataIndex); }
            text.text = float.Parse(string.Format("{0:0.0}", value)) + "";
        }
    }

    private float randomValue(float min, float max)
    {
        float random = Random.Range(min, max);
        return Mathf.Round(random * 10.0f) / 10.0f;
    }
}