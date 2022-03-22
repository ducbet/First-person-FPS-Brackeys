using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float cameraRotationLimit = 75f;

    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 thrusterForce = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void ApplyThruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    private void FixedUpdate()
    {
        PerformMove();
        PerformRotation();
        PerformThruster();
    }

    void PerformMove()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        }
    }
    void PerformRotation()
    {
        if (rotation != Vector3.zero)
        {
            rb.MoveRotation(transform.rotation * Quaternion.Euler(rotation));
            if(cam != null)
            {
                //cam.transform.Rotate(-cameraRotation);
                currentCameraRotationX -= cameraRotationX;
                currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
                cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
            }
        }
    }

    void PerformThruster()
    {
        if (thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
}
