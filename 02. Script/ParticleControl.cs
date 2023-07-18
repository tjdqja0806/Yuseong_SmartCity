using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    public ParticleSystem smoke;
    public Transform head;
    public Animator animator;

    void Start()
    {
    }

    void Update()
    {
        if (head.transform.localEulerAngles.y >= 280 && head.transform.localEulerAngles.y <= 281)
        {
            smoke.Play();
        }

        if (head.transform.localEulerAngles.y >= 30 && head.transform.localEulerAngles.y <= 45)
        {
            smoke.Stop();
        }
    }
}