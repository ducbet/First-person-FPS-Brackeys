using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerControllerFixed : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Spring setting:")]
    [SerializeField]
    private float jointSpring = 10f;
    [SerializeField]
    private float jointMaxForce = 40f;

    private PlayerMotor playerMotor;
    private ConfigurableJoint joint;

    // Start is called before the first frame update
    void Start()
    {
        playerMotor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        SetJointSettings(jointSpring);

        // disable Capsule Collider and Character Controller (avoid bug)
        CapsuleCollider _capsuleCollider = GetComponent<CapsuleCollider>();
        if (_capsuleCollider) {
            _capsuleCollider.enabled = false;
        }
        CharacterController _characterController = GetComponent<CharacterController>();
        if (_characterController)
        {
            _characterController.enabled = false;
        }
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
        float _cameraRotationX = _xRot * lookSensitivity;
        playerMotor.RotateCamera(_cameraRotationX);

        Vector3 _thrusterForce = Vector3.zero;
        if(Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        }
        else
        {
            // optimize: this case is called a lot
            SetJointSettings(jointSpring);
        }
        playerMotor.ApplyThruster(_thrusterForce);
    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
