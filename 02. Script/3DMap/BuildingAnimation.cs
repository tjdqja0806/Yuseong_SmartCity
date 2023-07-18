using UnityEngine;

public class BuildingAnimation : MonoBehaviour
{
    public GameObject[] floors;
    [Space]
    public GameObject environment;
    [Space]
    public GameObject[] signGroup;

    private Animator animator;
    private int status = 0;
    private PathPlay pathPlay;
    private CameraCustom cameraCustom;
    private PathGroupControl pathGroupControl;
    private XRayControl xRayControl;

    void Awake()
    {
        animator = GetComponent<Animator>();
        pathPlay = GameObject.Find("Animation Group").GetComponent<PathPlay>();
        cameraCustom = GameObject.Find("Main Camera").GetComponent<CameraCustom>();
        pathGroupControl = GameObject.Find("Description Group").GetComponent<PathGroupControl>();
        xRayControl = GameObject.Find("EventSystem").GetComponent<XRayControl>();
    }

    void Update()
    {
        // && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("building_halfClose"))
        {
            for (int i = status; i < floors.Length; i++)
            {
                floors[i].SetActive(false);
                pathPlay.isPlay = true;
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("building_close")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("building_halfClose_open")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("building_halfClose_close"))
        {
            for (int i = 0; i < floors.Length; i++)
            {
                floors[i].SetActive(true);
                pathPlay.isPlay = false;
            }
        }
    }

    public void _ClickOffice()
    {
        animator.SetBool("Unfold", false);
        cameraCustom.cameraStatus = 0;
        cameraCustom.isLerp = true;
        environment.SetActive(true);
        pathGroupControl.isPath = false;
        xRayControl.ChangeXRay(false);
        SighGroupControl(false);
    }

    public void _ClickBuilding()
    {
        animator.SetBool("Unfold", false);
        cameraCustom.cameraStatus = 1;
        cameraCustom.isLerp = true;
        environment.SetActive(false);
        pathGroupControl.isPath = false;
        xRayControl.ChangeXRay(false);
        SighGroupControl(false);
    }

    public void _ClickFloor()
    {
        animator.SetBool("Unfold", true);
        animator.SetBool("Top", false);
        cameraCustom.cameraStatus = 2;
        cameraCustom.isLerp = true;
        environment.SetActive(false);
        pathGroupControl.isPath = false;
        xRayControl.ChangeXRay(false);
        SighGroupControl(true);
    }

    public void _ClickSubOrg(int num)
    {
        status = num;
        animator.SetBool("Top", true);
        cameraCustom.cameraStatus = 3;
        cameraCustom.isLerp = true;
        environment.SetActive(false);
        if (num == 2) { xRayControl.ChangeXRay(); }
        else { xRayControl.ChangeXRay(false); }
        SighGroupControl(false);
    }

    private void SighGroupControl(bool stauts)
    {
        for (int i = 0; i < signGroup.Length; i++)
        {
            signGroup[i].SetActive(stauts);
        }
    }
}