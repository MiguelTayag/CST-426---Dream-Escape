
using UnityEngine;

public class DamageTarget : MonoBehaviour
{
    public float health = 50f;
    public GameObject ammo;
    
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Vector3 ammoPosition = transform.position + new Vector3(0, 1, 0);
            DestroyTarget();
            Instantiate(ammo, ammoPosition, Quaternion.identity);
        }
    }

    void DestroyTarget()
    {
        Destroy(gameObject);
    }
}
