using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;    
    }

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        Debug.Log(this.name + " now has " + currentHealth + " health");
    }
}
