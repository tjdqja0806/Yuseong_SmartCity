using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuDepartmentSelection : MonoBehaviour
{
    public GameObject departmentObject;
    public Image[] departmentButton;

    private bool departmentSelectionBool = false;
    private Color seletColor = new Color(1, 1, 1, 1);
    private Color noneColor = new Color(1, 1, 1, 0.4f);

    public void _SeletDepartment(int num)
    {
        for (int i = 0; i < departmentButton.Length; i++)
        {
            if (i == num)
                departmentButton[i].color = seletColor;
            else
                departmentButton[i].color = noneColor;
        }

    }

    public void _ClickDepartmentSelection()
    {
        departmentSelectionBool = !departmentSelectionBool;
        departmentObject.SetActive(departmentSelectionBool);
    }
}
