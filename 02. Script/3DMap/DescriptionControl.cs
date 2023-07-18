using System;
using TMPro;
using UnityEngine;

public class DescriptionControl : MonoBehaviour
{
    [Serializable]
    public struct IndexGroup { public OrgGroup[] orgGroups; }

    [Serializable]
    public struct OrgGroup
    {
        [HideInInspector]
        public string index;
        [HideInInspector]
        public string content;
    }

    public GameObject description;
    public TextMeshProUGUI text;
    public GameObject buttonGroup;
    [Space]
    public IndexGroup[] indexGroups;
    [Space]
    public OrganizationControl[] organizationControls;

    [HideInInspector]
    public string status;
    [HideInInspector]
    public int routeStatus;

    private PathPlay pathPlay;
    private BuildingAnimation buildingAnimation;
    private FooterControl footerControl;
    private PathGroupControl pathGroupControl;

    void Start()
    {
        indexGroups[0].orgGroups[0].index = "1-1";
        indexGroups[0].orgGroups[1].index = "1-2";

        indexGroups[1].orgGroups[0].index = "2-1";
        indexGroups[1].orgGroups[1].index = "2-2";
        indexGroups[1].orgGroups[2].index = "2-3";
        indexGroups[1].orgGroups[3].index = "2-4";
        indexGroups[1].orgGroups[4].index = "2-5";

        indexGroups[2].orgGroups[0].index = "3-1";
        indexGroups[2].orgGroups[1].index = "3-2";
        indexGroups[2].orgGroups[2].index = "3-3";
        indexGroups[2].orgGroups[3].index = "3-4";
        indexGroups[2].orgGroups[4].index = "3-5";

        indexGroups[0].orgGroups[0].content = "세원관리과 : 세입총괄, 세외체납, 체납관리, 세무조사에 관한 업무를 합니다.\n주요업무 : 등록면허세, 부동산 자동차 압류, 고액체납관리, 주민세 부과징수 등";
        indexGroups[0].orgGroups[1].content = "유성구청 카페 '쉼'";

        indexGroups[1].orgGroups[0].content = "청소행정과 : 폐기물관리, 청소행정, 자원순환에 관한 업무를 합니다.\n주요업무 : 생활폐기물, 대형폐기물, 음식물쓰레기 전용봉투 등";
        indexGroups[1].orgGroups[1].content = "교통정책과 : 교통행정, 교통지도, 차량관리에 관한 업무를 합니다.\n주요업무 : 마을버스, 교통유발부담금, 이륜차, 화물자동차운송사업 인허가 등";
        indexGroups[1].orgGroups[2].content = "주차관리과 : 주차행정, 주차시설, 교통체납에 관한 업무를 합니다.\n주요업무 : 불법주정차, 공영주차장, 과태료 체납 등";
        indexGroups[1].orgGroups[3].content = "";
        indexGroups[1].orgGroups[4].content = "푸른환경과 : 환경정책, 대기환경, 수질관리에 관한 업무를 합니다.\n주요업무 : 환경개선부담금, 야생동물포획, 비산먼지, 생활 소음, 폐수배출 등";

        indexGroups[2].orgGroups[0].content = "미래전략과 : 공공데이터 구축, 디지털혁신, 청년지원, 1인가구 지원에 관한 업무를 합니다.\n주요업무 : 공공데이터 관련, 스마트행정, 청년, 1인가구";
        indexGroups[2].orgGroups[1].content = "녹지산림과 : 녹지, 숲체험, 산림에 관한 업무를 합니다.\n주요업무 : 가로수, 녹지대, 숲체험, 산지전용 등";
        indexGroups[2].orgGroups[2].content = "공원과 : 공원정책, 공원관리, 공원조성에 관한 업무를 합니다.\n주요업무 : 족욕체험장, 도시공원, 어린이공원 등";
        indexGroups[2].orgGroups[3].content = "";
        indexGroups[2].orgGroups[4].content = "문화관광과 : 문화예술일반, 관광홍보, 건강체육, 온천시설에 관한 업무를 합니다.\n주요업무 : 축제, 문화예술단체, 관광활성화, 공공체육시설, 체육시설 신고변경, 온천수 등";

        pathPlay = GameObject.Find("Animation Group").GetComponent<PathPlay>();
        buildingAnimation = GameObject.Find("yuseong_annex_G").GetComponent<BuildingAnimation>();
        footerControl = GameObject.Find("Footer Group").GetComponent<FooterControl>();
        pathGroupControl = GameObject.Find("Description Group").GetComponent<PathGroupControl>();
    }

    public void ChangeDescription(string index)
    {
        status = index;
        for (int i = 0; i < indexGroups.Length; i++)
        {
            for (int j = 0; j < indexGroups[i].orgGroups.Length; j++)
            {
                if (status.Equals(indexGroups[i].orgGroups[j].index))
                {
                    text.text = indexGroups[i].orgGroups[j].content;
                }
            }
        }
    }

    public void _ClickElevatorButton()
    {
        string[] temp = status.Split(char.Parse("-"));
        pathPlay.index = status + "-" + routeStatus + "-El";
        pathPlay.IndexSetting(status + "-" + routeStatus + "-El");
        pathGroupControl.IndexSetting(status + "-" + routeStatus + "-El");
        pathGroupControl.isPath = true;
        buildingAnimation._ClickSubOrg(int.Parse(temp[0]) - 1);
        footerControl.ResetSignGroup();
        for (int i = 0; i < organizationControls.Length; i++) { organizationControls[i].isGuide = false; }
    }

    public void _ClickStairsButton() { }

    public void ResetRoute()
    {

    }
}