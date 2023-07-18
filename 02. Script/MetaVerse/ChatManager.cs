using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public RectTransform content;
    public TextMeshProUGUI chat;
    public TMP_InputField inputField;
    public Image chatting_Log;
    public GameObject talkPannel_1;
    public TextMeshProUGUI talkText;
    [HideInInspector]
    public bool isActive  = false;
    private Color active = new Color(1, 1, 1, 0.8f);
    private Color deactive = new Color(1, 1, 1, 0);
    private string containText;

    void Start()
    {
    }

    void Update()
    {
        if (inputField.gameObject.activeSelf && isActive && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            Chatting();
        }
        //Enter������ InputFeild, SenfButton Ȱ��ȭ, InputFeild Curser focus�̵�, Chatting_Lofâ ���� ����
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) 
        {
            inputField.gameObject.SetActive(true); 
            isActive = true;
            inputField.ActivateInputField();
            chatting_Log.color = active;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = false;
            inputField.gameObject.SetActive(false);
            chatting_Log.color = deactive;
        }
    }

    public void Chatting()
    {
        TextMeshProUGUI chatText = Instantiate(chat);
        chatText.transform.SetParent(content, false);
        if (inputField.text == "")
        {
            chatText.text = "    ";
            chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 1] : ";
        }
        else
        {
            chatText.text = inputField.text;
            chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 1] : ";
            if (inputField.text.Contains("�ȳ�"))
            {
                Invoke("ChattingSinalio", 1f);
                Invoke("talk_Panel_Off", 7f);
            }
            if (inputField.text.Contains("�ڱ�Ұ�"))
            {
                Invoke("ChattingSinalio_2", 1f);
                Invoke("talk_Panel_Off", 7f);
            }
            if (inputField.text.Contains("���ǻ���"))
            {
                Invoke("ChattingSinalio_3", 1f);
                Invoke("talk_Panel_Off", 7f);
            }
            if (inputField.text.Contains("ȫ��"))
            {
                Invoke("ChattingSinalio_4", 1f);
                Invoke("talk_Panel_Off", 7f);
            }
            if (inputField.text.Contains("�Ⱓ"))
            {
                Invoke("ChattingSinalio_5", 1f);
                Invoke("talk_Panel_Off", 7f);
            }
            if (inputField.text.Contains("����"))
            {
                Invoke("ChattingSinalio_6", 1f);
                Invoke("talk_Panel_Off", 7f);
            }
        }
        inputField.text = "";
    }
    public void talk_Panel_Off()
    {
        talkPannel_1.transform.gameObject.SetActive(false);
    }
    public void ChattingSinalio()
    {
        TextMeshProUGUI chatText = Instantiate(chat);
        chatText.transform.SetParent(content, false);
        chatText.text = "�ȳ��ϼ���.";
        chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 2] : ";
        //------------------------��ǳ��----------------------
        talkPannel_1.transform.gameObject.SetActive(true);
        talkText.text = chatText.text;

    }
    public void ChattingSinalio_2()
    {
        TextMeshProUGUI chatText = Instantiate(chat);
        chatText.transform.SetParent(content, false);
        chatText.text = "�ȳ��ϼ���. ���� �ƹ����Դϴ�. ���� ������õ������ ������ 5�аŸ��� �ִ� �Ĵ��� ��ϰ� �ֽ��ϴ�.";
        chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 2] : ";
        //------------------------��ǳ��----------------------
        talkPannel_1.transform.gameObject.SetActive(true);
        talkText.text = chatText.text;
    }
    public void ChattingSinalio_3()
    {
        TextMeshProUGUI chatText = Instantiate(chat);
        chatText.transform.SetParent(content, false);
        chatText.text = "�ֱ�, ������õ���� �߽����� �ڷγ� Ȯ���ڰ� ���� �߻��ϰ� �ֽ��ϴ�. ������ ���� ���� ���� ���� ������ ���� ������ �����Դϴ�. �׷��� ������õ������ �ùε鿡�� ����ũ�� �帮�� ���ÿ� ���� ���� ȫ���� �ұ� �ϴµ� ���������?";
        chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 2] : ";
        //------------------------��ǳ��----------------------
        talkPannel_1.transform.gameObject.SetActive(true);
        talkText.text = chatText.text;
    }
    public void ChattingSinalio_4()
    {
        TextMeshProUGUI chatText = Instantiate(chat);
        chatText.transform.SetParent(content, false);
        chatText.text = "����ũ ��� ���� ������ ��� �� ���� ���� �Ĵ� �̹����� �׷����ִ� ��ƼĿ�� ���ϱ� �մϴ�.";
        chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 2] : ";
        //------------------------��ǳ��----------------------
        talkPannel_1.transform.gameObject.SetActive(true);
        talkText.text = chatText.text;
    }
    public void ChattingSinalio_5()
    {
        TextMeshProUGUI chatText = Instantiate(chat);
        chatText.transform.SetParent(content, false);
        chatText.text = "������ �����Ϻ��� �ݿ��ϱ��� �Դϴ�.";
        chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 2] : ";
        //------------------------��ǳ��----------------------
        talkPannel_1.transform.gameObject.SetActive(true);
        talkText.text = chatText.text;
    }
    public void ChattingSinalio_6()
    {
        TextMeshProUGUI chatText = Instantiate(chat);
        chatText.transform.SetParent(content, false);
        chatText.text = "�����մϴ�.";
        chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 2] : ";
        //------------------------��ǳ��----------------------
        talkPannel_1.transform.gameObject.SetActive(true);
        talkText.text = chatText.text;
    }
}
