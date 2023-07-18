using UnityEngine;

public class LevelSetActive : MonoBehaviour
{
    public GameObject[] levels;

    private int status = 9;

    void Start()
    {

    }

    void Update()
    {

    }

    public void _ClickLevelButton(int num)
    {
        if (num == status)
        {
            for (int i = 0; i < levels.Length; i++) { levels[i].SetActive(true); }
            status = 9;
        }
        else
        {
            for (int i = 0; i < levels.Length; i++)
            {
                if (i == num) { levels[i].SetActive(true); }
                else { levels[i].SetActive(false); }
            }
            status = num;
        }
    }
}