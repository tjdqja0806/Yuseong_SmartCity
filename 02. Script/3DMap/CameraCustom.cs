using UnityEngine;

public class CameraCustom : MonoBehaviour
{
    [Header("Pan Settings")]
    [Range(1, 20)]
    [Tooltip("How responsive the Pan action should be.")]
    public float PanSpeed = 10f;
    [Tooltip("Ivert the Pan direction.")]
    public bool PanInverted = true;

    [Header("Scroll Settings")]
    [Range(1, 20)]
    [Tooltip("How responsive the Scroll action should be.")]
    public float ScrollSpeed = 10f;
    [Tooltip("Invert the Scroll direction.")]
    public bool ScrollInverted = false;

    [Header("Orbit Settings")]
    [Range(1, 10)]
    [Tooltip("How responsive the Orbit action should be.")]
    public float OrbitSpeed = 5.0f;
    [Tooltip("Invert the Orbit direction in the X-axis.")]
    public bool InvertOrbitX = false;
    [Tooltip("Invert the Orbit direction in the Y-axis.")]
    public bool InvertOrbitY = false;

    [Header("Misc. Settings")]
    [Range(1, 10)]
    [Tooltip("The minimum distance from the target when focusing or while Orbiting")]
    public float FocusDistance = 5f;

    [Tooltip("The minimum distance to active selection.")]
    public float MinDistanceToSelection = 2f;

    [Tooltip("Toggle min distance to active selection.")]
    public bool UseMinDistance = true;

    [Tooltip("The prefab to use as a marker to show hit location when user selects a GameObject")]
    public GameObject HitpointMarkerPrefab;

    [Space]
    public float cameraMoveSpeed = 3;
    public float cameraRotateSpeed = 5;
    [Space]
    public GameObject[] points;
    [HideInInspector]
    public int cameraStatus = 0;
    [HideInInspector]
    public bool isLerp = false;

    private bool _hasSelection;
    public bool HasSelection
    {
        get { return _hasSelection; }
        set
        {
            if (value != _hasSelection)
            {
                _hasSelection = value;

                _hitPointMarker.SetActive(_hasSelection);
            }
        }
    }

    private Vector3 _target;
    public Vector3 Target
    {
        get { return _target; }
        set
        {
            if (value != _target)
            {
                _target = value;
                if (_hitPointMarker != null)
                {
                    _hitPointMarker.transform.position = _target;
                }
            }
        }
    }

    // Instantiated HitPointmarker GameObject
    private GameObject _hitPointMarker { get; set; }

    // Panning properties...
    private Vector3 initMousePos { get; set; }
    private Vector3 panDelta { get; set; }

    private bool _isMoving;
    /// <summary>
    /// Is the Camera being moved via the WASDQE keys.
    /// </summary>
    public bool IsMoving
    {
        get { return _isMoving; }
        set
        {
            if (value != _isMoving)
            {
                _isMoving = value;
                if (_isMoving)
                {
                    CameraIsMoving?.Invoke();
                }
                else
                {
                    CameraStoppedMoving?.Invoke();
                }
            }
        }
    }

    private bool _isPanning;
    /// <summary>
    /// Is the Camera panning.
    /// </summary>
    public bool IsPanning
    {
        get { return _isPanning; }
        set
        {
            if (value != _isPanning)
            {
                _isPanning = value;
                if (_isPanning)
                {
                    CameraIsPanning?.Invoke();
                }
                else
                {
                    CameraStoppedPanning?.Invoke();
                }
            }
        }
    }

    private bool _isScrolling;
    /// <summary>
    /// Is the Camera Scrolling.
    /// </summary>
    public bool IsScrolling
    {
        get { return _isScrolling; }
        set
        {
            if (value != _isScrolling)
            {
                _isScrolling = value;
                if (_isScrolling)
                {
                    CameraIsScrolling?.Invoke();
                }
                else
                {
                    CameraStoppedScrolling?.Invoke();
                }
            }
        }
    }

    private bool _isLooking;
    /// <summary>
    /// Is the Camera Looking around.
    /// </summary>
    public bool IsLooking
    {
        get { return _isLooking; }
        set
        {
            if (value != _isLooking)
            {
                _isLooking = value;
                if (_isLooking)
                {
                    CameraIsLooking?.Invoke();
                }
                else
                {
                    CameraStoppedLooking?.Invoke();
                }
            }
        }
    }

    private bool _isOrbiting;
    /// <summary>
    /// If the Camera is orbiting.
    /// </summary>
    public bool IsOrbiting
    {
        get { return _isOrbiting; }
        set
        {
            if (value != _isOrbiting)
            {
                _isOrbiting = value;
                if (_isOrbiting)
                {
                    CameraIsOrbiting?.Invoke();
                }
                else
                {
                    CameraStoppedOrbiting?.Invoke();
                }
            }
        }
    }

    private void Start()
    {
        if (HitpointMarkerPrefab != null)
        {
            _hitPointMarker = Instantiate(HitpointMarkerPrefab, _target, Quaternion.identity);
            _hitPointMarker.SetActive(false);
        }

    }

    private void Update()
    {
        if (!isLerp)
        {
            CameraPan();
            CameraScroll();
            CameraOrbit();
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, points[cameraStatus].transform.position, Time.deltaTime * cameraMoveSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, points[cameraStatus].transform.rotation, Time.deltaTime * cameraRotateSpeed);
            if (Vector3.Distance(transform.position, points[cameraStatus].transform.position) < 0.01f)
            {
                transform.position = points[cameraStatus].transform.position;
                transform.rotation = points[cameraStatus].transform.rotation;
                isLerp = false;
            }
        }
    }

    private void CameraPan()
    {
        bool panning = false;

        bool canPan = (!HasSelection && IsOrbiting) ? false : true;

        // Panning Started...
        if (Input.GetMouseButtonDown(0) && canPan) 
        {
            initMousePos = Input.mousePosition;
            panning = true;
        }

        // While Panning...
        if (Input.GetMouseButton(0) && canPan)
        {
            // Calculate Pan delta between frames...
            panDelta = (Input.mousePosition - initMousePos);

            // If pan is inverted, reverse vector...
            panDelta = (PanInverted) ? panDelta = panDelta * -1 : panDelta;

            // Calculate Speed multiplier...
            var speed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? PanSpeed / 1000 * 3 : PanSpeed / 1000;

            // Translate the camera's position...
            transform.Translate(new Vector3(panDelta.x * speed, panDelta.y * speed));

            // Set initial mouse postion to current mouse position for next Update...
            initMousePos = Input.mousePosition;

            panning = true;
        }

        // Update the IsPanning Property...
        IsPanning = panning;
    }

    private void CameraScroll()
    {
        bool scrolling = false;

        // Ensure user can't move while orbiting on the spot...
        if (!HasSelection && IsOrbiting)
        {
            return;
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            // Get mouse scrollwheel value
            var scrollValue = Input.GetAxisRaw("Mouse ScrollWheel");

            // Maintain min distance to target while moving...
            if (scrollValue > 0 && (HasSelection && UseMinDistance))
            {
                if (Vector3.Distance(transform.position, Target) <= MinDistanceToSelection)
                {
                    return;
                }
            }

            // if Scroll is inverted, reverse Scroll direction...
            scrollValue = (ScrollInverted) ? scrollValue * -1 : scrollValue;

            // Calculate Speed multiplier...
            float speed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? scrollValue * ScrollSpeed * 3 : scrollValue * ScrollSpeed;

            // Translate the camera's position...
            transform.Translate(transform.forward * speed, Space.World);

            scrolling = true;
        }

        // Update the IsScrolling property...
        IsScrolling = scrolling;
    }

    private void CameraOrbit()
    {
        bool orbiting = false;

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                // Update the target position...
                Target = hit.point;
                HasSelection = true;
                // Raise event to any listeners...
                SelectionChanged?.Invoke(hit);

            }
            else
            {
                Target = transform.position;
                HasSelection = false;
            }
        }

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            if (InvertOrbitX) { mouseX = mouseX * -1; }
            if (InvertOrbitY) { mouseY = mouseY * -1; }

            Vector3 tempTF = transform.position;
            if (calculateCheck(transform.eulerAngles.x))
            {
                transform.RotateAround(Target, transform.right, mouseY * OrbitSpeed * 0.1f);
            }
            else
            {
                transform.position = tempTF;

                if (calculateRange(transform.eulerAngles.x) > 85 && mouseY < 0)
                {
                    //Debug.Log("조건 1 : " + calculateRange(transform.eulerAngles.x) + ", mouseY : " + mouseY);
                    transform.RotateAround(Target, transform.right, mouseY * OrbitSpeed * 0.1f);
                }
                else if (calculateRange(transform.eulerAngles.x) < 0 && mouseY > 0)
                {
                    //Debug.Log("조건 2 : " + calculateRange(transform.eulerAngles.x) + ", mouseY : " + mouseY);
                    transform.RotateAround(Target, transform.right, mouseY * OrbitSpeed * 0.1f);
                }
            }
            transform.RotateAround(Target, Vector3.up, mouseX * OrbitSpeed * 0.1f);

            orbiting = true;
        }

        //if (Input.GetMouseButton(1))
        //{
        //    float mouseX = Input.GetAxis("Mouse X");
        //    float mouseY = -Input.GetAxis("Mouse Y");

        //    if (InvertOrbitX) { mouseX = mouseX * -1; }
        //    if (InvertOrbitY) { mouseY = mouseY * -1; }

        //    Vector3 currentPosition = transform.position;
        //    //transform.RotateAround(Target, transform.right, mouseY * OrbitSpeed * 200 * Time.deltaTime);
        //    //transform.RotateAround(Target, Vector3.up, mouseX * OrbitSpeed * 200 * Time.deltaTime);
        //    Vector3 currentRotation = transform.localRotation.eulerAngles;

        //    if (calculateCheck(currentRotation.x))
        //    {
        //        transform.RotateAround(Target, transform.right, mouseY * OrbitSpeed * 200 * Time.deltaTime);
        //        transform.RotateAround(Target, Vector3.up, mouseX * OrbitSpeed * 200 * Time.deltaTime);
        //    }
        //    else if (calculateRange(currentRotation.x) < 0)
        //    {
        //        transform.position = currentPosition;
        //        //currentRotation.x = 0.1f;
        //        //currentRotation.z = 0;
        //        //transform.localRotation = Quaternion.Euler(currentRotation);
        //    }
        //    else if (calculateRange(currentRotation.x) > 85)
        //    {
        //        transform.position = currentPosition;
        //        //currentRotation.x = 84.9f;
        //        //currentRotation.z = 0;
        //        //transform.localRotation = Quaternion.Euler(currentRotation);
        //    }

        //    //float tempY = currentRotation.x;
        //    //if (tempY > 180) { tempY -= 360; }
        //    //if (tempY < 5 || tempY > 85)
        //    //{
        //    //    transform.position = currentPosition;
        //    //}
        //    //else if (tempY < 0)
        //    //{
        //    //    currentRotation.x = 0.1f;
        //    //    transform.localRotation = Quaternion.Euler(currentRotation);
        //    //}
        //    //else if (tempY > 90)
        //    //{
        //    //    currentRotation.x = 84.9f;
        //    //    transform.localRotation = Quaternion.Euler(currentRotation);
        //    //}
        //    orbiting = true;
        //}

        IsOrbiting = orbiting;
    }

    private float calculateRange(float value)
    {
        if (value > 180) { value -= 360; }
        return value;
    }

    private bool calculateCheck(float value)
    {
        if (value > 180) { value -= 360; }
        if (value >= 0 && value <= 85) { return true; }
        else { return false; }
    }

    public delegate void CameraIsMovingAction();
    public delegate void CameraStoppedMovingAction();
    public delegate void CameraIsPanningAction();
    public delegate void CameraStoppedPanningAction();
    public delegate void CameraIsScrollingAction();
    public delegate void CameraStoppedScrollingAction();
    public delegate void CameraIsLookingAction();
    public delegate void CameraStoppedLookingAction();
    public delegate void CameraIsOrbittingAction();
    public delegate void CameraStoppedOrbitingAction();
    public delegate void CameraFocusedOnObjectAction();
    public delegate void SelectionChangedAction(RaycastHit hit);

    public static event CameraIsMovingAction CameraIsMoving;
    public static event CameraStoppedMovingAction CameraStoppedMoving;
    public static event CameraIsPanningAction CameraIsPanning;
    public static event CameraStoppedPanningAction CameraStoppedPanning;
    public static event CameraIsScrollingAction CameraIsScrolling;
    public static event CameraStoppedScrollingAction CameraStoppedScrolling;
    public static event CameraIsLookingAction CameraIsLooking;
    public static event CameraStoppedLookingAction CameraStoppedLooking;
    public static event CameraIsOrbittingAction CameraIsOrbiting;
    public static event CameraStoppedOrbitingAction CameraStoppedOrbiting;
    public static event CameraFocusedOnObjectAction CameraFocusedOnObject;
    public static event SelectionChangedAction SelectionChanged;
}