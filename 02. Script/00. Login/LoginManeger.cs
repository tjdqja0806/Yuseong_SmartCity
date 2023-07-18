using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginManeger : MonoBehaviour
{
    [HideInInspector]
    public int status = 1; //User/Manager Status
    [HideInInspector]
    public bool rememberStatus;

    [Header("User/Manager Button Image")]
    [SerializeField] Image userImage;
    [SerializeField] Image managerImage;

    [Header("User/Manager Sprite")]
    [SerializeField] Sprite userNoneSprite;
    [SerializeField] Sprite userClickSprite;
    [SerializeField] Sprite managerNoneSprite;
    [SerializeField] Sprite managerClickSprite;

    [Header("Input Text Image")]
    [SerializeField] Image name_idInput;
    [SerializeField] Image number_pwInput;

    [Header("InputText Sprite")]
    [SerializeField] Sprite userNameSprite;
    [SerializeField] Sprite userNumberSprite;
    [SerializeField] Sprite managerIDSprite;
    [SerializeField] Sprite managerPWSprite;

    [Header("Input Text Text")]
    [SerializeField] TextMeshProUGUI name_idText;
    [SerializeField] TMP_InputField name_idInputText;
    [SerializeField] TextMeshProUGUI number_pwText;
    [SerializeField] TMP_InputField number_pwInputText;

    [Header("Remember Button Image/Sprite")]
    [SerializeField] Image rememberImage;
    [SerializeField] Sprite rememberClick;
    [SerializeField] Sprite rememberNone;
    [Space]
    [SerializeField] GameObject loginFailImage;

    SceneChange sceneChange;
    void Awake()
    {
        sceneChange = GameObject.Find("EventSystem").GetComponent<SceneChange>();   
    }
    void Start()
    {
        UserManagerClick(status);
        name_idInputText.Select();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            LoginClick();
        if (name_idInputText.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                number_pwInputText.Select();
        }
    }
    public void UserManagerClick(int num)
    {
        status = num;

        if(status == 0)//status = user
        {
            //버튼 Sprite 변경
            userImage.sprite = userClickSprite;
            managerImage.sprite = managerNoneSprite;

            //InputField 변경
            name_idInput.sprite = userNameSprite;
            number_pwInput.sprite = userNumberSprite;
            name_idText.text = "이름을 작성해주세요.";
            number_pwText.text = "전화번호를 작성해주세요";
            number_pwInputText.contentType = TMP_InputField.ContentType.Standard;
        }
        else //status = manager
        {
            //버튼 Sprite 변경
            userImage.sprite = userNoneSprite;
            managerImage.sprite = managerClickSprite;

            //InputField 변경
            name_idInput.sprite = managerIDSprite;
            number_pwInput.sprite = managerPWSprite;
            name_idText.text = "이메일을 작성해주세요.";
            number_pwText.text = "비밀번호를 작성해주세요";
            number_pwInputText.contentType = TMP_InputField.ContentType.Password;
        }
    }

    public void RememberClick()
    {
        rememberStatus = !rememberStatus;
        if (rememberStatus)
            rememberImage.sprite = rememberClick;
        else
            rememberImage.sprite = rememberNone;
    }

    public void LoginClick()
    {

        if(status == 1)
        {
            if (name_idInputText.text == "admin" && number_pwInputText.text == "admin")
            {
                PlayerPrefs.SetString("User_Type", "Manager");
                sceneChange.clickSceneChange("01. yuseong_WorldMap");
            }
            else
            {
                loginFailImage.SetActive(true);
            }
        }
        else
        {
            if (number_pwInputText.text.Length == 11 && number_pwInputText.text.Contains("010"))
            {
                PlayerPrefs.SetString("User_Type", "User");
                sceneChange.clickSceneChange("01. yuseong_WorldMap");
            }
        }

    }
    public void ReturnClick()
    {
        loginFailImage.SetActive(false);
    }
}
