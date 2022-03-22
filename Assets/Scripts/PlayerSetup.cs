using System;
using Mirror;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    private string remotePlayerName = "RemotePlayer";

    private string playerNamePrefix = "Player ";

    Camera sceneCamera;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            // local player
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
        else
        {
            // on network
            DisableComponents();
            AssignRemotePlayer();
        }
        SetPlayerName();
    }

    private void SetPlayerName()
    {
        this.name = playerNamePrefix + GetComponent<NetworkIdentity>().netId;
    }

    private void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void AssignRemotePlayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remotePlayerName);
    }

    private void OnDestroy()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
