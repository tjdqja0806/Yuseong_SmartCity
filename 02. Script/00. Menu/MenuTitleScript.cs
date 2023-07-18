using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuTitleScript : MonoBehaviour
{
    public GameObject funtionTitle;
    public TextMeshProUGUI funtionTitleText;
    private bool isActive;
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void _MenuTitleClick(int num)
    {
        isActive = !isActive;
        funtionTitle.SetActive(isActive);
        switch (num)
        {
            case 0:
                funtionTitleText.text = "��� ȭ�� �溸 �˸� ����";
                break;
            case 1:
                funtionTitleText.text = "�ο� ��� ���� Ȯ�� ����";
                break;
            case 2:
                funtionTitleText.text = "���� �ü��� Ȯ�� ����";
                break;
            case 3:
                funtionTitleText.text = "���� ���� ���� Ȯ�� ����";
                break;
            case 4:
                funtionTitleText.text = "������ ���� ���� - ����";
                break;
            case 5:
                funtionTitleText.text = "���� ��� ȫ�� ����";
                break;
            case 10:
                funtionTitleText.text = "������ ���� ���� - ���� CCTV";
                break;
            case 11:
                funtionTitleText.text = "������ ���� ���� - ����, ��� ����";
                break;
            case 6:
                funtionTitleText.text = "������ ���� ���� - ���� ��� ����";
                break;
            case 7:
                funtionTitleText.text = "��û ������ �ο� ����";
                break;
            case 8:
                funtionTitleText.text = "���� ��û ���� ���� �ذ�";
                break;
            case 9:
                funtionTitleText.text = "������ ���� ���� - ��� ���� �Ű�";
                break;
            case 12:
                funtionTitleText.text = "�ε��� �ǹ� ���� ����";
                break;
            case 13:
                funtionTitleText.text = "��� ħ������ ������ Ȱ��";
                break;

        }
        /*
        0. ��� ȭ�� �溸 v
        1. �ο� ��� v
        2. �ǹ� ���� v
        3. ���� ���� v
        4. ���� ���� v
        10. CCTV ����(10) v
        11. ��������(11) v
        6. ������ ���� v
        7. �ο� ��Ȳ v
        8. ���� ���� ���� v
        9. ���� �߻� v
        5. 12.���� ��� ȫ��
         */
    }
}
