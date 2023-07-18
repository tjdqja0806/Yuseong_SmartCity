using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [HideInInspector] public bool isDetailContent;
    [SerializeField] private MemuController memuController;
    [SerializeField] private GameObject[] builingModel;
    [SerializeField] private GameObject water;
    /*
     buildingModel 배열 순서
    0. 예방 접종 센터
    1. 유성 도서관
    2. 온천1동 행정복지센터
    3. 신성동 행정복지센터
    4. 전민동 행정복지센터
    5. 청소년 수련관
    6. 유성구청
    7. 구암 도서관
     */
    private float moveSpeed = 10f;
    private float rotateSpeed = 200f;

    private float _x;
    private float _y;

    private SpawnPin spawnPin;
    private float distance;
    private Vector3 offset = new Vector3(0, 9, 0);

    void Awake()
    {
        spawnPin = GameObject.Find("EventSystem").GetComponent<SpawnPin>();
    }
    void Update()
    {
        //Pin CloseUp
        if (spawnPin.isCloseUp)
        {
            distance = Vector3.Distance(transform.position, spawnPin.pinPos.position);
            transform.LookAt(spawnPin.pinPos);
            transform.position = Vector3.Lerp(transform.position, spawnPin.pinPos.position, 0.5f);
            if (distance < 100f)
            {
                spawnPin.isCloseUp = false;
                transform.position += offset;
                transform.LookAt(spawnPin.pinPos);
            }
        }
        else if (memuController.isWaterColseUp)
        {
            distance = Vector3.Distance(transform.position, water.transform.position);
            transform.LookAt(water.transform.position);
            transform.position = Vector3.Lerp(transform.position, water.transform.position, 0.5f);
            if (distance < 50f)
            {
                memuController.isWaterColseUp = false;
                transform.position += offset;
                transform.LookAt(water.transform.position);
            }
        }
        else if (memuController.buildingCloseUp)
        {
            distance = Vector3.Distance(transform.position, builingModel[memuController.buildingStatus].transform.position);
            transform.LookAt(builingModel[memuController.buildingStatus].transform.position);
            transform.position = Vector3.Lerp(transform.position, builingModel[memuController.buildingStatus].transform.position, 0.5f);
            if (distance < 50f)
            {
                memuController.buildingCloseUp = false;
                transform.position += offset;
                transform.LookAt(builingModel[memuController.buildingStatus].transform.position);
            }
        }
        else
        {
            if (!isDetailContent)
            {
                KeybordMove();
                if (Input.GetMouseButton(1))
                    MouseRotation();
            }
        }
    }
    private void KeybordMove()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.Self);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.Self);
        if (Input.GetKey(KeyCode.E))
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.Self);
        if (Input.GetKey(KeyCode.Q))
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.Self);
    }
    private void MouseRotation()
    {
        _x = Input.GetAxis("Mouse X");
        _y = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.down * rotateSpeed * -_x * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.left * rotateSpeed * _y * Time.deltaTime, Space.Self);
    }
}
