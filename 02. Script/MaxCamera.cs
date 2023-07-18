using UnityEngine;

[AddComponentMenu("Camera-Control/3dsMax Camera Style")]
public class MaxCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 targetOffset;
    public float distance = 5.0f;
    public float maxDistance = 20;
    public float minDistance = .6f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public int yMinLimit = -80;
    public int yMaxLimit = 80;
    public int zoomRate = 40;
    public float panSpeed = 0.3f;
    public float zoomDampening = 5.0f;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;

    [Space]
    public float cameraMoveSpeed = 3;
    public float cameraRotateSpeed = 5;
    [Space]
    public GameObject[] points;
    [HideInInspector]
    public int cameraStatus = 0;
    [HideInInspector]
    public bool isLerp = false;

    void Start() { Init(); }
    void OnEnable() { Init(); }

    public void Init()
    {
        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (transform.forward * distance);
            target = go.transform;
        }

        distance = Vector3.Distance(transform.position, target.position);
        currentDistance = distance;
        desiredDistance = distance;

        position = transform.position;
        rotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;

        xDeg = Vector3.Angle(Vector3.right, transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);
    }

    void LateUpdate()
    {
        if (!isLerp)
        {
            if (Input.GetMouseButton(0))
            {
                xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.01f; // float : 회전속도
                yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.01f;

                yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);

                desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
                currentRotation = transform.rotation;
                rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
                transform.rotation = rotation;
            }
            else if (Input.GetMouseButton(1))
            {
                target.rotation = transform.rotation;
                target.Translate(Vector3.right * -Input.GetAxis("Mouse X") * panSpeed); // 좌우 이동
                target.Translate(transform.up * -Input.GetAxis("Mouse Y") * panSpeed, Space.World); // 상하 이동
            }

            desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate;
            currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);
            position = target.position - (rotation * Vector3.forward * currentDistance + targetOffset);
            transform.position = position;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, points[cameraStatus].transform.position, Time.deltaTime * cameraMoveSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, points[cameraStatus].transform.rotation, Time.deltaTime * cameraRotateSpeed);
            if (Vector3.Distance(transform.position, points[cameraStatus].transform.position) < 0.01f)
            {
                Init();
                target.transform.position = transform.position + (transform.forward * distance);
                isLerp = false;
            }
        }
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}