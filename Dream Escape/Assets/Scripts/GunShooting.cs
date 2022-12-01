using UnityEngine;
using System.Collections;

public class GunShooting : MonoBehaviour
{

    public float damage = 10f;
    public float fireRate = 15f;


    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    private float nextTimeToFire = 0f;

    private Animator characterAnimator;

    private AudioSource fireSound;
    private AudioSource reloadSound;

    private void Start()
    {
        characterAnimator = GetComponentInParent<Animator>();
        fireSound = GameObject.FindWithTag("fireSound").GetComponent<AudioSource>();
        reloadSound = GameObject.FindWithTag("reloadSound").GetComponent<AudioSource>();

        if (currentAmmo == -1)
        {
            currentAmmo = maxAmmo;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0 || (Input.GetKey(KeyCode.R) && currentAmmo != maxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }


        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            fireSound.Play();
            Shooting();
        }
        

    }

    IEnumerator Reload()
    {
        isReloading = true;
        characterAnimator.SetBool("isReloading", true);
        Debug.Log("Reloading!");
        reloadSound.Play();

        yield return new WaitForSeconds(reloadTime);


        currentAmmo = maxAmmo;
        isReloading = false;
        characterAnimator.SetBool("isReloading", false);
        reloadSound.Stop();
    }

    void Shooting()
    {
        muzzleFlash.Play();

        currentAmmo--;

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)){
            Debug.Log(hit.transform.name);

            DamageTarget target = hit.transform.GetComponent<DamageTarget>();


            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

    }
}
