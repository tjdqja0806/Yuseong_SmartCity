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
        //Enter누를시 InputFeild, SenfButton 활성화, InputFeild Curser focus이동, Chatting_Lof창 투명도 조절
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
            if (inputField.text.Contains("안녕"))
            {
                Invoke("ChattingSinalio", 1f);
                Invoke("talk_Panel_Off", 7f);
            }
            if (inputField.text.Contains("자기소개"))
            {
                Invoke("ChattingSinalio_2", 1f);
                Invoke("talk_Panel_Off", 7f);
            }
            if (inputField.text.Contains("건의사항"))
            {
                Invoke("ChattingSinalio_3", 1f);
                Invoke("talk_Panel_Off", 7f);
            }
            if (inputField.text.Contains("홍보"))
            {
                Invoke("ChattingSinalio_4", 1f);
                Invoke("talk_Panel_Off", 7f);
            }
            if (inputField.text.Contains("기간"))
            {
                Invoke("ChattingSinalio_5", 1f);
                Invoke("talk_Panel_Off", 7f);
            }
            if (inputField.text.Contains("승인"))
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
        chatText.text = "안녕하세요.";
        chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 2] : ";
        //------------------------말풍선----------------------
        talkPannel_1.transform.gameObject.SetActive(true);
        talkText.text = chatText.text;

    }
    public void ChattingSinalio_2()
    {
        TextMeshProUGUI chatText = Instantiate(chat);
        chatText.transform.SetParent(content, false);
        chatText.text = "안녕하세요. 저는 아무개입니다. 저는 유성온천역에서 도보로 5분거리에 있는 식당을 운영하고 있습니다.";
        chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 2] : ";
        //------------------------말풍선----------------------
        talkPannel_1.transform.gameObject.SetActive(true);
        talkText.text = chatText.text;
    }
    public void ChattingSinalio_3()
    {
        TextMeshProUGUI chatText = Instantiate(chat);
        chatText.transform.SetParent(content, false);
        chatText.text = "최근, 유성온천역을 중심으로 코로나 확진자가 많이 발생하고 있습니다. 때문에 저희 매장 매출 또한 전년대비 많이 떨어진 상태입니다. 그래서 유성온천역에서 시민들에게 마스크를 드리며 동시에 저희 가게 홍보도 할까 하는데 괜찮을까요?";
        chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 2] : ";
        //------------------------말풍선----------------------
        talkPannel_1.transform.gameObject.SetActive(true);
        talkText.text = chatText.text;
    }
    public void ChattingSinalio_4()
    {
        TextMeshProUGUI chatText = Instantiate(chat);
        chatText.transform.SetParent(content, false);
        chatText.text = "마스크 몇개를 작은 봉투에 담고 그 위에 저희 식당 이미지가 그려져있는 스티커를 붙일까 합니다.";
        chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 2] : ";
        //------------------------말풍선----------------------
        talkPannel_1.transform.gameObject.SetActive(true);
        talkText.text = chatText.text;
    }
    public void ChattingSinalio_5()
    {
        TextMeshProUGUI chatText = Instantiate(chat);
        chatText.transform.SetParent(content, false);
        chatText.text = "다음주 월요일부터 금요일까지 입니다.";
        chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 2] : ";
        //------------------------말풍선----------------------
        talkPannel_1.transform.gameObject.SetActive(true);
        talkText.text = chatText.text;
    }
    public void ChattingSinalio_6()
    {
        TextMeshProUGUI chatText = Instantiate(chat);
        chatText.transform.SetParent(content, false);
        chatText.text = "감사합니다.";
        chatText.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "[User 2] : ";
        //------------------------말풍선----------------------
        talkPannel_1.transform.gameObject.SetActive(true);
        talkText.text = chatText.text;
    }
}
