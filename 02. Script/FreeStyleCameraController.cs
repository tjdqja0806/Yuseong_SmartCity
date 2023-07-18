using System;
using UnityEngine;

/// <summary>
/// Free Style Camera Controller.
/// </summary>
[RequireComponent(typeof(UnityEngine.Camera), typeof(AudioListener))]
public class FreeStyleCameraController : MonoBehaviour
{
    #region Inspector Properties

    [Header("Move Settings")]
    [Range(1, 10)]
    [Tooltip("The move speed with WASD keys.")]
    public float MoveSpeed = 5f;

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

    [Header("Look Settings")]
    [Range(100, 300)]
    [Tooltip("How responsive the Look action should be.")]
    public float LookSensitivity = 200.0f;
    [Tooltip("The angle in which the user can look Vertically measured from the horizontal")]
    public float ClampAngle = 80.0f;
    [Tooltip("Invert the Look direction in the X-axis")]
    public bool LookInvertX = false;
    [Tooltip("Invert the Look direction in the Y-axis")]
    public bool LookInvertY = false;

    [Header("Orbit Settings")]
    [Range(1, 10)]
    [Tooltip("How responsive the Orbit action should be.")]
    public float OrbitSpeed = 5.0f;
    [Tooltip("Invert the Orbit direction.")]
    public bool InvertOrbit = false;

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

    #endregion

    #region Private Properties

    // Selection properties...
    private bool _hasSelection;
    /// <summary>
    /// If a Selection is active.
    /// </summary>
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
    /// <summary>
    /// The target orbit pivot point.
    /// </summary>
    public Vector3 Target
    {
        get { return _target; }
        set
        {
            if (value != _target)
            {
                _target = value;
                targetAltitude = Target.y;
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

    // Look rotation properties...
    private float lookRotY = 0.0f;
    private float lookRotX = 0.0f;

    // Orbit properties...
    private float targetAltitude { get; set; }
    private float orbitRotX = 0.0f;

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

    #endregion

    #region Methods

    /// <summary>
    /// On component enabled.
    /// </summary>
    private void OnEnable()
    {

        // Get the Camera's Local Rotation as Euler angles...
        Vector3 rot = transform.localRotation.eulerAngles;
        // Set initial x, y rotation angles...
        lookRotY = rot.y;
        lookRotX = rot.x;

        Target = transform.position;

        // Enable Audio listener...
        transform.GetComponent<AudioListener>().enabled = true;
    }

    /// <summary>
    /// On Component disabled.
    /// </summary>
    private void OnDisable()
    {
        // Disable AudioListener so doesn't conflict with other AudioListeners...
        transform.GetComponent<AudioListener>().enabled = false;
    }

    /// <summary>
    /// When Scene starts.
    /// </summary>
    private void Start()
    {
        // Ensure Camera's tag is set to MainCamera...
        if (transform.tag != "MainCamera")
        {
            transform.tag = "MainCamera";
        }

        //Instantiate HitPoint Marker...
        if (HitpointMarkerPrefab != null)
        {
            _hitPointMarker = Instantiate(HitpointMarkerPrefab, _target, Quaternion.identity);
            _hitPointMarker.SetActive(false);
        }

    }

    /// <summary>
    /// When Update called...
    /// </summary>
    private void Update()
    {
        // Check of user is selecting an object...
        if (Input.GetMouseButtonDown(0))
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

        // Check if user is escaping Selection...
        if (Input.GetKeyDown(KeyCode.Escape) && HasSelection)
        {
            Target = transform.position;
            HasSelection = false;
        }

        // Check if user is using controls to Move/Pan/Scroll/Look/Orbit/Focus...
        CameraMove();
        CameraPan();
        CameraScroll();
        CameraLook();
        CameraOrbit();
        FocusOnTarget();
    }

    /// <summary>
    /// Moves the Camera based on Key Press (WASDQE).
    /// </summary>
    private void CameraMove()
    {
        bool moving = false;

        // Ensure user can't move while orbiting on the spot...
        if (!HasSelection && IsOrbiting)
        {
            return;
        }

        // Calculate move speed...
        var speed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? 3 * MoveSpeed / 100 : MoveSpeed / 100;

        // Move forwards...
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // Maintain min distance to target while moving...
            if (HasSelection && UseMinDistance)
            {
                if (Vector3.Distance(transform.position, Target) <= MinDistanceToSelection)
                {
                    return;
                }
            }

            transform.Translate(transform.forward.normalized * speed, Space.World);
            moving = true;
        }

        // Move left...
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(transform.right.normalized * -speed, Space.World);
            moving = true;
        }

        // Move backwards...
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(transform.forward.normalized * -speed, Space.World);
            moving = true;
        }

        // Move right...
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(transform.right.normalized * speed, Space.World);
            moving = true;
        }

        // Move up...
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.up * speed, Space.World);
            moving = true;
        }

        // Move down...
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.up * -speed, Space.World);
            moving = true;
        }

        // Update the IsMoving property...
        IsMoving = moving;
    }

    /// <summary>
    /// Pans the Camera parallel to screen.
    /// </summary>
    private void CameraPan()
    {
        bool panning = false;

        bool canPan = (!HasSelection && IsOrbiting) ? false : true;

        // Panning Started...
        if (Input.GetMouseButtonDown(2) && canPan)
        {
            initMousePos = Input.mousePosition;
            panning = true;
        }

        // While Panning...
        if (Input.GetMouseButton(2) && canPan)
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

    /// <summary>
    /// Scrolls the Camera in the normal to screen.
    /// </summary>
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

    /// <summary>
    /// Rotates the camera depending towards the screen space mouse position.
    /// </summary>
    private void CameraLook()
    {
        bool looking = false;

        if (Input.GetMouseButtonDown(1) && !HasSelection)
        {
            // Reset pivot target position to camera's position...
            Target = transform.position;
        }

        if (Input.GetMouseButton(1))
        {
            // Get Mouse X/Y values...
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            // Append axis Rotations...
            if (LookInvertX)
            {
                lookRotY -= mouseX * LookSensitivity * Time.deltaTime;
            }
            else
            {
                lookRotY += mouseX * LookSensitivity * Time.deltaTime;
            }

            if (LookInvertY)
            {
                lookRotX -= mouseY * LookSensitivity * Time.deltaTime;
            }
            else
            {
                lookRotX += mouseY * LookSensitivity * Time.deltaTime;
            }

            // Ensure the value is measured in +- PI (or +- 180). Without this, the clamp below locks out at +- ClampAngle causing camera to direction to jump...
            lookRotX = EnsureValuePlusMinusPi(lookRotX, true);
            lookRotY = EnsureValuePlusMinusPi(lookRotY, true);

            // Clamp rotation...
            lookRotX = Mathf.Clamp(lookRotX, -ClampAngle, ClampAngle);

            // Apply Quarternion Rotation to Transforms rotation....
            transform.rotation = Quaternion.Euler(lookRotX, lookRotY, 0.0f);

            looking = true;
        }

        // Update the IsLooking Property...
        IsLooking = looking;
    }

    /// <summary>
    /// Rotates Camera about a target point.
    /// </summary>
    private void CameraOrbit()
    {
        bool orbiting = false;

        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
        {
            // Get X Axis Value...
            float mouseX = Input.GetAxis("Mouse X");

            if (InvertOrbit)
            {
                mouseX = mouseX * -1;
            }
            // Calculate rotation angle...
            orbitRotX += mouseX * OrbitSpeed * Time.deltaTime;

            // Rotate camera about target and set target as camera's subject.
            transform.RotateAround(Target, Vector3.up, orbitRotX);
            //transform.LookAt(Target);

            // Get updated Euler angles from transfrom...
            Vector3 rot = transform.localRotation.eulerAngles;
            lookRotY = rot.y;
            lookRotX = rot.x;

            orbiting = true;
        }

        // Update the IsOrbiting Property...
        IsOrbiting = orbiting;
    }

    /// <summary>
    /// Focus on Target if HasSelection.
    /// </summary>
    private void FocusOnTarget()
    {
        if (Input.GetKeyDown(KeyCode.F) && HasSelection)
        {
            // Look at target...
            transform.LookAt(Target);
            // Calculate new Camera position...
            var t = (Target - transform.position).normalized * (Vector3.Distance(transform.position, Target) - FocusDistance);
            // Move the Camera...
            transform.Translate(t, Space.World);

            // Get updated Euler angles from transfrom...
            Vector3 rot = transform.localRotation.eulerAngles;
            lookRotY = rot.y;
            lookRotX = rot.x;

            // Fire event...
            CameraFocusedOnObject?.Invoke();
        }
    }

    /// <summary>
    /// Ensures angle is always between -180/180 degrees.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static float EnsureValuePlusMinusPi(float value, bool inDegrees)
    {
        float limit = (inDegrees) ? 180 : (float)Math.PI;

        if (value < -limit)
        {
            return value + (limit * 2);
        }
        if (value > limit)
        {
            return value - (limit * 2);
        }
        return value;
    }

    #endregion

    #region Events
    #region Event Delegates

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

    #endregion

    #region Events

    /// <summary>
    /// Triggered when the Camera is Moving.
    /// </summary>
    public static event CameraIsMovingAction CameraIsMoving;
    /// <summary>
    /// Triggered when the Camera stops Moving.
    /// </summary>
    public static event CameraStoppedMovingAction CameraStoppedMoving;

    /// <summary>
    /// Triggered when the Camera is Panning.
    /// </summary>
    public static event CameraIsPanningAction CameraIsPanning;
    /// <summary>
    /// Triggered when the Camera stops Panning.
    /// </summary>
    public static event CameraStoppedPanningAction CameraStoppedPanning;

    /// <summary>
    /// Triggered when the Camera is Scrolling.
    /// </summary>
    public static event CameraIsScrollingAction CameraIsScrolling;
    /// <summary>
    /// Triggered when the Camera stops Scrolling.
    /// </summary>
    public static event CameraStoppedScrollingAction CameraStoppedScrolling;

    /// <summary>
    /// Triggered when the Camera is Looking.
    /// </summary>
    public static event CameraIsLookingAction CameraIsLooking;
    /// <summary>
    /// Triggered when the Camera stops Looking.
    /// </summary>
    public static event CameraStoppedLookingAction CameraStoppedLooking;

    /// <summary>
    /// Triggered when the Camera is Orbiting.
    /// </summary>
    public static event CameraIsOrbittingAction CameraIsOrbiting;
    /// <summary>
    /// Triggered when the Camera stops Orbiting.
    /// </summary>
    public static event CameraStoppedOrbitingAction CameraStoppedOrbiting;

    /// <summary>
    /// Triggered when the Camera focuses on an Object.
    /// </summary>
    public static event CameraFocusedOnObjectAction CameraFocusedOnObject;

    /// <summary>
    /// Triggered when the Selection is changed.
    /// </summary>
    public static event SelectionChangedAction SelectionChanged;

    #endregion
    #endregion
}