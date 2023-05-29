using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractItem : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float throwSpeed = 100;
    //public AudioClip throwSFX;
    bool collected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(projectilePrefab, 
                transform.position + transform.forward, transform.rotation) as GameObject;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwSpeed, ForceMode.VelocityChange);
            //AudioSource.PlayClipAtPoint(throwSFX, transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            collected = true;
            LevelManager.distractions += 1;
            Destroy(gameObject);
        }
    }
}
