using UnityEngine;
using Cinemachine;

public class CinemachinePanAndZoom : MonoBehaviour
{
    [SerializeField, Tooltip("Speed at which to pan screen")]
    private float panSpeed = 2f;
    [SerializeField, Tooltip("Maximum fov to zoom in")]
    private float zoomInMax = 40f;
    [SerializeField, Tooltip("Maximum fov to zoom out")]
    private float zoomOutMax = 90f;
    [SerializeField, Tooltip("How fast to zoom")]
    private float zoomSpeed = 3f;
    [SerializeField] Collider2D boundary;

    private CinemachineInputProvider input;
    private CinemachineVirtualCamera vcam;
    private Transform cameraTransform;

    private float startingZPosition { get; set; }

    private void Awake()
    {
        input = GetComponent<CinemachineInputProvider>();
        vcam = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = vcam.VirtualCameraGameObject.transform;
    }

    private void Start()
    {
        startingZPosition = cameraTransform.position.z;
    }

    private void Update()
    {
        float x = CinemachineCore.GetInputAxis("Horizontal");
        float y = CinemachineCore.GetInputAxis("Vertical");
        float z = 0;

        if (x != 0 || y != 0)
        {
            PanScreen(x, y);
        }
        if (z != 0)
        {
            ZoomScreen(z);
        }
    }

    public void ZoomScreen(float increment)
    {
        float fov = vcam.m_Lens.FieldOfView;
        float target = Mathf.Clamp((fov + (increment)), zoomInMax, zoomOutMax);
        vcam.m_Lens.FieldOfView = Mathf.Lerp(fov, target, zoomSpeed * Time.deltaTime);
    }

    public Vector2 PanDirection(float x, float y)
    {
        Vector2 direction = Vector2.zero;
        if (y >= Screen.height * .95) direction.y += 1;
        if (y <= Screen.height * .05) direction.y -= 1;
        if (x >= Screen.width * .95) direction.x += 1;
        if (x <= Screen.width * .05) direction.x -= 1;

        return direction;
    }

    public void PanScreen(float x, float y)
    {
        Vector2 direction = PanDirection(x, y);

        Vector3 targetPosition = (Vector3)direction * panSpeed + cameraTransform.position;
        targetPosition.x = Mathf.Clamp(targetPosition.x, boundary.bounds.min.x, boundary.bounds.max.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, boundary.bounds.min.y, boundary.bounds.max.y);

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, Time.deltaTime);
    }
}
