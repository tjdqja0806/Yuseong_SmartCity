using UnityEngine;

public class AnimationCheck : MonoBehaviour
{
    public Animator animator;
    public string animName;
    [Space]
    public GameObject[] obj;

    private DataAgent dataAgent;
    private float timer = 0.0f;

    void Start()
    {
        dataAgent = GameObject.Find("EventSystem").GetComponent<DataAgent>();
        for (int i = 0; i < obj.Length; i++) { obj[i].SetActive(false); }
        animator.SetBool("Active", false);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 1.0f;
            if (dataAgent.DeviceStatus())
            {
                for (int i = 0; i < obj.Length; i++) { obj[i].SetActive(true); }
                animator.SetBool("Active", true);
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rotate") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1
            )
        {
            for (int i = 0; i < obj.Length; i++) { obj[i].SetActive(false); }
            animator.SetBool("Active", false);
        }
    }
}