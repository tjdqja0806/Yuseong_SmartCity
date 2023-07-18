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
                funtionTitleText.text = "긴급 화재 경보 알림 서비스";
                break;
            case 1:
                funtionTitleText.text = "민원 통계 정보 확인 서비스";
                break;
            case 2:
                funtionTitleText.text = "공공 시설물 확인 서비스";
                break;
            case 3:
                funtionTitleText.text = "대전 관광 정보 확인 서비스";
                break;
            case 4:
                funtionTitleText.text = "디지털 행정 서비스 - 교통";
                break;
            case 5:
                funtionTitleText.text = "지역 상권 홍보 서비스";
                break;
            case 10:
                funtionTitleText.text = "디지털 행정 서비스 - 교통 CCTV";
                break;
            case 11:
                funtionTitleText.text = "디지털 행정 서비스 - 공사, 사고 정보";
                break;
            case 6:
                funtionTitleText.text = "디지털 행정 서비스 - 교통 사고 정보";
                break;
            case 7:
                funtionTitleText.text = "구청 당직자 민원 대응";
                break;
            case 8:
                funtionTitleText.text = "유성 구청 주차 문제 해결";
                break;
            case 9:
                funtionTitleText.text = "디지털 행정 서비스 - 긴급 범죄 신고";
                break;
            case 12:
                funtionTitleText.text = "부동산 건물 정보 서비스";
                break;
            case 13:
                funtionTitleText.text = "상습 침수구역 빅데이터 활용";
                break;

        }
        /*
        0. 긴급 화제 경보 v
        1. 민원 통계 v
        2. 건물 정보 v
        3. 관광 정보 v
        4. 교통 정보 v
        10. CCTV 정보(10) v
        11. 공사정보(11) v
        6. 교통사고 정보 v
        7. 민원 현황 v
        8. 주차 공간 정보 v
        9. 범죄 발생 v
        5. 12.지역 상원 홍보
         */
    }
}
