using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    GameObject gun;
    private GunShooting gunShootingScript;

    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.FindWithTag("Gun");
        gunShootingScript = gun.GetComponent<GunShooting>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gunShootingScript.maxAmmo += 15;
            Destroy(gameObject);
        }
    }

}
