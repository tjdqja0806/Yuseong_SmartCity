using UnityEngine;

public class MouseDragControl : MonoBehaviour
{
    public Transform zeroPos;
    public float distance = 12.0f;
    public float panSpeed = 10.0f;
    [Space]
    public Transform model;

    float rotXSpeed = 120.0f;
    float rotYSpeed = 250.0f;
    float rotYMinLimit = -20;
    float rotYMaxLimit = 80;
    private double rotX = 0.0f;
    private double rotY = 0.0f;
    float panY = 0;

    void Start()
    {
        var angles = transform.eulerAngles;
        rotX = angles.y;
        rotY = angles.x;

        panY = transform.position.y;
    }

    void Update()
    {
        // 이동
        if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") < -0.3)
            {
                transform.Translate(Vector3.right * Time.deltaTime * panSpeed * 0.5f);
                transform.position = new Vector3(transform.position.x, panY, transform.position.z);
            }
            if (Input.GetAxis("Mouse X") > 0.3)
            {
                transform.Translate(Vector3.left * Time.deltaTime * panSpeed * 0.5f);
                transform.position = new Vector3(transform.position.x, panY, transform.position.z);
            }
            if (Input.GetAxis("Mouse Y") < -0.3)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * panSpeed);
                transform.position = new Vector3(transform.position.x, panY, transform.position.z);
            }
            if (Input.GetAxis("Mouse Y") > 0.3)
            {
                transform.Translate(Vector3.back * Time.deltaTime * panSpeed);
                transform.position = new Vector3(transform.position.x, panY, transform.position.z);
            }
        }

        // 확대 / 축소
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 50.0f);
            panY = transform.position.y;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.Translate(Vector3.back * Time.deltaTime * 50.0f);
            panY = transform.position.y;
        }

        // 회전
        if (Input.GetMouseButton(1))
        {
            rotX += Input.GetAxis("Mouse X") * rotXSpeed * 0.02;
            rotY -= Input.GetAxis("Mouse Y") * rotYSpeed * 0.02;

            rotY = ClampAngle((float)rotY, rotYMinLimit, rotYMaxLimit);

            Quaternion rotation = Quaternion.Euler((float)rotY, (float)rotX, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + zeroPos.position;

            transform.rotation = rotation;
            transform.position = position;
            panY = transform.position.y;
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}