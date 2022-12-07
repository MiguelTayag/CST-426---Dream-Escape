
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
            DestroyTarget();
            Instantiate(ammo, transform.position, Quaternion.identity);
        }
    }

    void DestroyTarget()
    {
        Destroy(gameObject);
    }
}
