using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public float Distance;
    public float sensitivity;
    private float pitch;
    private float yaw;

    private Vector3 cameraTargetPosition;
    public LayerMask collisionMask;

    private float autoCameraTimer;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        autoCameraTimer = 4f;
    }

    private void Update()
    {
        if (Input.GetAxis("HorizontalCamera") == 0f && Input.GetAxis("VerticalCamera") == 0f) {
            // no camera input
            autoCameraTimer -= Time.deltaTime;
            if (autoCameraTimer < 0) {
                float difference = Mathf.DeltaAngle(target.eulerAngles.y, transform.eulerAngles.y);
                difference = Mathf.Clamp(difference, -30, 30);
                Debug.Log(difference);
                if (difference < 0) {
                    yaw -= difference * Time.deltaTime;
                } else {
                    yaw -= difference * Time.deltaTime;
                }
            }

        } else {
            autoCameraTimer = 2f;
            pitch += Input.GetAxis("HorizontalCamera") * sensitivity;
        }

        yaw += Input.GetAxis("VerticalCamera") * sensitivity;


        pitch = Mathf.Clamp(pitch, -90, 90);

        transform.eulerAngles = new Vector3(pitch, yaw);


        cameraTargetPosition = target.position - transform.forward * Distance;

        CameraCollision();

        transform.position = cameraTargetPosition;

    }

    private void CameraCollision() {
        RaycastHit hit;
        Vector3 point;
        if (Physics.Raycast(target.position, -transform.forward * Distance, out hit, Distance, collisionMask)) {
            point = hit.point;
            cameraTargetPosition = point;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(target.position, cameraTargetPosition);
    }

}