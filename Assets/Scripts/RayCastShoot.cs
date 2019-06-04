using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot : MonoBehaviour {
    public int gunDamage = 5;
    public float fireRate = .25f;
    public float weaponRange =25f;
    public float hitForce = 100f;
    public float camShake = .08f;
    public Transform gunEnd;
    public CameraShake cameraShake;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    private Animator anim; 
    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private float nextFire;
    
	void Start () {  
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
        anim = GetComponent<Animator>(); 
	}

    void Update()
    {

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            StartCoroutine(cameraShake.Shake(.085f, camShake));

            
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;


            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {

                Chase health = hit.collider.GetComponent<Chase>();
                GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

                Destroy(impactGo, 2f);
                //if there is an enemy
                if (health != null)
                  {
                    //damage the enemy
                      health.Damage(gunDamage); 
                  }

                //if rigidbody then add force
                  if (hit.rigidbody != null)
                 {
                   hit.rigidbody.AddForce(-hit.normal * hitForce);
                 
                  }
            }
              //else
              //{
              //}
        }

        else if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("Fire", false);
        }
    }
        

        private IEnumerator ShotEffect()
        {
        anim.SetBool("Fire", true); 
        gunAudio.Play();
        muzzleFlash.Play();
      

        yield return shotDuration;
       
        }

   

}
