using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignControl_Test : MonoBehaviour
{
    [HideInInspector]
    public bool isAni = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
        animator.SetBool("Animate", true);
    }
}
