using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerControllerFixed : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor playerMotor;

    // Start is called before the first frame update
    void Start()
    {
        playerMotor = GetComponent<PlayerMotor>();
        // disable Capsule Collider and Character Controller
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float _zMove = Input.GetAxisRaw("Horizontal");
        float _xMove = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _zMove;
        Vector3 _movVerical = transform.forward * _xMove;

        Vector3 _velocity = (_movHorizontal + _movVerical).normalized * speed;
        playerMotor.Move(_velocity);

        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;
        playerMotor.Rotate(_rotation);

        float _xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;
        playerMotor.RotateCamera(_cameraRotation);
    }
}
