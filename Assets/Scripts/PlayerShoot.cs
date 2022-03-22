using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    public PlayerWeapon weapon;

    [SerializeField]
    public LayerMask mask;

    [SerializeField]
    private Camera cam;

    private void Start()
    {
        if(cam == null)
        {
            Debug.LogError("PlayerShoot: No camera reference");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    private void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            if(_hit.collider.CompareTag(PLAYER_TAG))
            {
                CmdPlayerShot(_hit.collider.name);
            }
        }
    }

    [Command]
    private void CmdPlayerShot(string _playerId)
    {
        Debug.Log(_playerId + " has been shot");
    }
}
