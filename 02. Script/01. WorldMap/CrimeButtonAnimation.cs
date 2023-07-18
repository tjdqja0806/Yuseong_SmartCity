using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimeButtonAnimation : MonoBehaviour
{
    public Animator crimeAnimator;
    private bool isPlay;

    public void _CrimeButtonClick()
    {
        //if(!isPlay)

        isPlay = !isPlay;
        crimeAnimator.gameObject.SetActive(isPlay);
        crimeAnimator.SetBool("PinMoveTrigger", isPlay);
    }

}
