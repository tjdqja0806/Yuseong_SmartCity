using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUserTypeCheck : MonoBehaviour
{
    /*
    Button Array 
    0.긴급화재경보
    1.민원 통계
    2. 민원 현황
    3. 주차 공간
    4. 시설물
    5. 관광 정보
    6. 생활 정보
    7. Setting
    8. 지역상권 홍보
     */
    public Button[] menuButton;

    private string user_type;
    private string sceneName;

    private void Awake()
    {
        user_type = PlayerPrefs.GetString("User_Type");
        sceneName = SceneManager.GetActiveScene().name;

    }
    void Start()
    {
        for (int i = 0; i < menuButton.Length; i++)
        {
            menuButton[i].interactable = true;
            menuButton[i].image.raycastTarget = true;
        }
        if (user_type == "Manager") //Manager
        {
            switch (sceneName)
            {
                case "01. yuseong_WorldMap":
                    for (int i = 0; i < menuButton.Length; i++)
                        menuButton[i].interactable = true;
                    menuButton[0].interactable = false;
                    menuButton[0].image.raycastTarget = false;
                    break;
                case "02. yuseong_annex":
                    menuButton[0].interactable = true;
                    menuButton[1].interactable = true;
                    menuButton[2].interactable = true;
                    menuButton[3].interactable = true;
                    menuButton[4].interactable = false;
                    menuButton[4].image.raycastTarget = false;
                    menuButton[5].interactable = false;
                    menuButton[5].image.raycastTarget = false;
                    menuButton[6].interactable = false;
                    menuButton[6].image.raycastTarget = false;
                    menuButton[8].interactable = false;
                    menuButton[8].image.raycastTarget = false;
                    break;
                case "03. yuseong_annex_metaVerse":
                    menuButton[0].interactable = false;
                    menuButton[0].image.raycastTarget = false;
                    menuButton[1].interactable = true;
                    menuButton[2].interactable = true;
                    menuButton[3].interactable = false;
                    menuButton[3].image.raycastTarget = false;
                    menuButton[4].interactable = false;
                    menuButton[4].image.raycastTarget = false;
                    menuButton[5].interactable = false;
                    menuButton[5].image.raycastTarget = false;
                    menuButton[6].interactable = false;
                    menuButton[6].image.raycastTarget = false;
                    menuButton[8].interactable = false;
                    menuButton[8].image.raycastTarget = false;
                    break;
                default:
                    menuButton[0].interactable = false;
                    menuButton[0].image.raycastTarget = false;
                    menuButton[1].interactable = true;
                    menuButton[2].interactable = true;
                    menuButton[3].interactable = false;
                    menuButton[3].image.raycastTarget = false;
                    menuButton[4].interactable = false;
                    menuButton[4].image.raycastTarget = false;
                    menuButton[5].interactable = false;
                    menuButton[5].image.raycastTarget = false;
                    menuButton[6].interactable = false;
                    menuButton[6].image.raycastTarget = false;
                    menuButton[8].interactable = false;
                    menuButton[8].image.raycastTarget = false;
                    break;
            }
            
        }
        else //User
        {
            switch (sceneName)
            {
                case "01. yuseong_WorldMap":
                    menuButton[3].interactable = true;
                    menuButton[4].interactable = true;
                    menuButton[5].interactable = true;
                    menuButton[6].interactable = true;
                    menuButton[8].interactable = true;
                    break;
                case "02. yuseong_annex":
                    menuButton[3].interactable = true;
                    menuButton[4].interactable = false;
                    menuButton[4].image.raycastTarget = false;
                    menuButton[5].interactable = false;
                    menuButton[5].image.raycastTarget = false;
                    menuButton[6].interactable = false;
                    menuButton[6].image.raycastTarget = false;
                    menuButton[8].interactable = false;
                    menuButton[8].image.raycastTarget = false;
                    break;
                default:
                    menuButton[3].interactable = false;
                    menuButton[3].image.raycastTarget = false;
                    menuButton[4].interactable = false;
                    menuButton[4].image.raycastTarget = false;
                    menuButton[5].interactable = false;
                    menuButton[5].image.raycastTarget = false;
                    menuButton[6].interactable = false;
                    menuButton[6].image.raycastTarget = false;
                    menuButton[8].interactable = false;
                    menuButton[8].image.raycastTarget = false;
                    break;
            }

            menuButton[0].interactable = false;
            menuButton[0].image.raycastTarget = false;
            menuButton[1].interactable = false;
            menuButton[1].image.raycastTarget = false;
            menuButton[2].interactable = false;
            menuButton[2].image.raycastTarget = false;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
