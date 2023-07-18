using System;
using TMPro;
using UnityEngine;

public class PathGroupControl : MonoBehaviour
{
    [Serializable]
    public struct PathGroup
    {
        public GameObject path;
        public TextMeshProUGUI path1;
        public TextMeshProUGUI path2;
        public TextMeshProUGUI path3;
    }

    public PathGroup[] pathGroups;
    [HideInInspector]
    public bool isPath = false;
    [HideInInspector]
    private string currentIndex;

    private string[,,] pathStrings = new string[3, 5, 5];
    private int index1 = 0;
    private int index2 = 0;
    private int index3 = 0;

    void Start()
    {
        pathStrings[0, 0, 0] = "세원관리과";
        pathStrings[0, 0, 1] = "세무조사팀";
        pathStrings[0, 0, 2] = "체납관리팀";
        pathStrings[0, 0, 3] = "세외체납팀";
        pathStrings[0, 0, 4] = "세입총괄팀";

        // ------------------------------------------------------------

        pathStrings[1, 0, 0] = "청소행정과";
        pathStrings[1, 0, 1] = "자원순환팀";
        pathStrings[1, 0, 2] = "청소행정팀";
        pathStrings[1, 0, 3] = "폐기물관리팀";

        pathStrings[1, 1, 0] = "교통정책과";
        pathStrings[1, 1, 1] = "교통행정팀";
        pathStrings[1, 1, 2] = "교통지도팀";
        pathStrings[1, 1, 3] = "차량관리팀";

        pathStrings[1, 2, 0] = "주치관리과";
        pathStrings[1, 2, 1] = "주차행정팀";
        pathStrings[1, 2, 2] = "교통체납팀";
        pathStrings[1, 2, 3] = "주차시설팀";

        pathStrings[1, 4, 0] = "푸른환경과";
        pathStrings[1, 4, 1] = "수질관리팀";
        pathStrings[1, 4, 2] = "대기환경팀";
        pathStrings[1, 4, 3] = "환경정책팀";

        // ------------------------------------------------------------

        pathStrings[2, 0, 0] = "미래전략과";
        pathStrings[2, 0, 1] = "공공데이터팀";
        pathStrings[2, 0, 2] = "디지털혁신팀";
        pathStrings[2, 0, 3] = "미래세대팀";
        pathStrings[2, 0, 4] = "외로움해소팀";

        pathStrings[2, 1, 0] = "녹지산림과";
        pathStrings[2, 1, 1] = "녹지팀";
        pathStrings[2, 1, 2] = "숲체험팀";
        pathStrings[2, 1, 3] = "산림팀";

        pathStrings[2, 2, 0] = "공원과";
        pathStrings[2, 2, 1] = "공원정책팀";
        pathStrings[2, 2, 2] = "공원관리팀";
        pathStrings[2, 2, 3] = "공원조성팀";

        pathStrings[2, 4, 0] = "문화관광과";
        pathStrings[2, 4, 1] = "건강체육팀";
        pathStrings[2, 4, 2] = "온천시설팀";
        pathStrings[2, 4, 3] = "관광팀";
        pathStrings[2, 4, 4] = "문화예술팀";
    }

    void Update()
    {
        if (isPath)
        {
            if (index1 == 0)
            {
                pathGroups[0].path.SetActive(true);
                pathGroups[0].path1.text = pathStrings[0, 0, 0];
                pathGroups[0].path2.text = pathStrings[0, 0, index3];
                pathGroups[1].path.SetActive(false);
            }
            else
            {
                pathGroups[1].path.SetActive(true);
                pathGroups[1].path1.text = (index1 + 1) + "층";
                pathGroups[1].path2.text = pathStrings[index1, index2, 0];
                pathGroups[1].path3.text = pathStrings[index1, index2, index3];
                pathGroups[0].path.SetActive(false);
            }
        }
        else { for (int i = 0; i < pathGroups.Length; i++) { pathGroups[i].path.SetActive(false); } }
    }

    public void IndexSetting(string content)
    {
        string[] temp = content.Split(char.Parse("-"));
        index1 = int.Parse(temp[0]) - 1;
        index2 = int.Parse(temp[1]) - 1;
        index3 = int.Parse(temp[2]) + 1;
    }
}