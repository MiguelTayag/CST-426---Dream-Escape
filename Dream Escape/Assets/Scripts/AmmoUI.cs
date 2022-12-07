using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI ammoUI;
    private GunShooting gunShooting;


    // Start is called before the first frame update
    void Start()
    {
        gunShooting = GetComponent<GunShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoUI.text = gunShooting.currentAmmo.ToString() + "/" + gunShooting.maxAmmo.ToString();
    }
}
