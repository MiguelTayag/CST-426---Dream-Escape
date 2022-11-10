
using UnityEngine;

public class DamageTarget : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            DestroyTarget();
        }
    }

    void DestroyTarget()
    {
        Destroy(gameObject);
    }
}
