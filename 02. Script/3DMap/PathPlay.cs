using System;
using UnityEngine;

public class PathPlay : MonoBehaviour
{
    [Serializable]
    public struct AniGroup
    {
        public GameObject aniGroup;
        public GameObject route;
        [HideInInspector]
        public Animator animator;
        public GameObject[] department;
    }

    public GameObject effect;
    public GameObject effectPoint1;
    public GameObject effectPoint2;
    public GameObject effectPoint3;
    [Space]
    public AniGroup[] ani1F;
    [Space]
    public AniGroup[] ani2F;
    [Space]
    public AniGroup[] ani3F;

    [HideInInspector]
    public string index = "";
    [HideInInspector]
    public bool isPlay = false;
    [HideInInspector]
    public bool isEnd = false;

    private int cycleStatus = 0;
    private float timer = 2;
    private float timer2 = 0.3f;
    private bool isDepartment = false;

    private int index1 = 0;
    private int index2 = 0;
    private int index3 = 0;
    private string type = "";


    void Start()
    {
        for (int i = 0; i < ani1F.Length; i++) { ani1F[i].animator = ani1F[i].aniGroup.GetComponent<Animator>(); }
        for (int i = 0; i < ani2F.Length; i++) { ani2F[i].animator = ani2F[i].aniGroup.GetComponent<Animator>(); }
        for (int i = 0; i < ani3F.Length; i++) { ani3F[i].animator = ani3F[i].aniGroup.GetComponent<Animator>(); }
    }

    void Update()
    {
        if (isPlay) { PlayCycle(); }
        else { ResetCycle();  isEnd = true; }
    }

    private void PlayCycle()
    {
        switch (cycleStatus)
        {
            case 0:
                isEnd = false;
                if (index1 == 1)
                {
                    ani1F[1].aniGroup.SetActive(true);
                    ani1F[1].route.SetActive(true);
                    if (ani1F[1].animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                    {
                        ani1F[1].aniGroup.SetActive(false);
                        timer = 2;
                        cycleStatus = 3;
                    }
                }
                else
                {
                    ani1F[0].aniGroup.SetActive(true);
                    ani1F[0].route.SetActive(true);
                    if (ani1F[0].animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                    {
                        ani1F[0].aniGroup.SetActive(false);
                        timer = 2;
                        cycleStatus++;
                    }
                }
                break;

            case 1:
                if (timer >= 2)
                {
                    GameObject instance = Instantiate(effect, effectPoint1.transform.position, effectPoint1.transform.rotation);
                    Destroy(instance, 2f);
                    switch (index1)
                    {
                        case 2:
                            instance = Instantiate(effect, effectPoint2.transform.position, effectPoint2.transform.rotation);
                            Destroy(instance, 2f);
                            break;

                        case 3:
                            instance = Instantiate(effect, effectPoint3.transform.position, effectPoint3.transform.rotation);
                            Destroy(instance, 2f);
                            break;
                    }
                    timer -= Time.deltaTime;
                }
                else if (timer <= 0)
                {
                    cycleStatus++;
                    timer = 2;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
                break;

            case 2:
                switch (index1)
                {
                    case 2:
                        ani2F[index2].aniGroup.SetActive(true);
                        ani2F[index2].route.SetActive(true);
                        if (ani2F[index2].animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                        {
                            ani2F[index2].aniGroup.SetActive(false);
                            cycleStatus++;
                            timer = 2;
                        }
                        break;

                    case 3:
                        ani3F[index2].aniGroup.SetActive(true);
                        ani3F[index2].route.SetActive(true);
                        if (ani3F[index2].animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                        {
                            ani3F[index2].aniGroup.SetActive(false);
                            cycleStatus++;
                            timer = 2;
                        }
                        break;
                }
                break;

            case 3:
                if (timer2 <= 0)
                {
                    switch (index1)
                    {
                        case 1:
                            isDepartment = !isDepartment;
                            ani1F[index2].department[index3].SetActive(isDepartment);
                            timer2 = 0.3f;
                            break;

                        case 2:
                            isDepartment = !isDepartment;
                            ani2F[index2].department[index3].SetActive(isDepartment);
                            timer2 = 0.3f;
                            break;

                        case 3:
                            isDepartment = !isDepartment;
                            ani3F[index2].department[index3].SetActive(isDepartment);
                            timer2 = 0.3f;
                            break;
                    }
                }
                else
                {
                    timer2 -= Time.deltaTime;
                }
                if (timer <= 0)
                {
                    isDepartment = false;
                    switch (index1)
                    {
                        case 1:
                            ani1F[index2].department[index3].SetActive(isDepartment);
                            break;

                        case 2:
                            ani2F[index2].department[index3].SetActive(isDepartment);
                            break;

                        case 3:
                            ani3F[index2].department[index3].SetActive(isDepartment);
                            break;
                    }
                    isEnd = true;
                    timer2 = 0.3f;
                    cycleStatus = 0;
                }
                timer -= Time.deltaTime;
                break;
        }
    }

    private void ResetCycle()
    {
        for (int i = 0; i < ani1F.Length; i++)
        {
            ani1F[i].aniGroup.SetActive(false);
            ani1F[i].route.SetActive(false);
            if (ani1F[i].department.Length > 0)
            {
                for (int j = 0; j < ani1F[i].department.Length; j++)
                {
                    ani1F[i].department[j].SetActive(false);
                }
            }
        }
        for (int i = 0; i < ani2F.Length; i++)
        {
            ani2F[i].aniGroup.SetActive(false);
            ani2F[i].route.SetActive(false);
            if (ani2F[i].department.Length > 0)
            {
                for (int j = 0; j < ani2F[i].department.Length; j++)
                {
                    ani2F[i].department[j].SetActive(false);
                }
            }
        }
        for (int i = 0; i < ani3F.Length; i++)
        {
            ani3F[i].aniGroup.SetActive(false);
            ani3F[i].route.SetActive(false);
            if (ani3F[i].department.Length > 0)
            {
                for (int j = 0; j < ani3F[i].department.Length; j++)
                {
                    ani3F[i].department[j].SetActive(false);
                }
            }
        }
        cycleStatus = 0;
        timer = 2;
        timer2 = 0.3f;
        isDepartment = false;
    }

    public void IndexSetting(string content)
    {
        string[] temp = content.Split(char.Parse("-"));
        index1 = int.Parse(temp[0]);
        index2 = int.Parse(temp[1]) - 1;
        index3 = int.Parse(temp[2]);
        type = temp[3];
    }
}