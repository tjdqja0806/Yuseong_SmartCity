using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyFireAlarmScript : MonoBehaviour
{

    public GameObject fireEffect;
    public GameObject[] people;
    public GameObject[] indoorMembersText;

    [SerializeField]private MemuController menuController;
    void Awake()
    {

    }

    void Update()
    {

    }
    public void _FireAlarmClick()
    {
        if (menuController.uiBool[0])
        {
            fireEffect.SetActive(true);
            for (int i = 0; i < indoorMembersText.Length; i++)
                indoorMembersText[i].SetActive(true);
            for (int i = 0; i < people.Length; i++)
                people[i].SetActive(false);

        }
        else
        {
            fireEffect.SetActive(false);
            for (int i = 0; i < indoorMembersText.Length; i++)
                indoorMembersText[i].SetActive(false);
            for (int i = 0; i < people.Length; i++)
                people[i].SetActive(true);

        }
    }
}
