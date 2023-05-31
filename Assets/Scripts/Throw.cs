using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float throwSpeed = 100;
    //public AudioClip throwSFX;
    //bool collected = false; public in level manager

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && LevelManager.distractions > 0)
        {
            GameObject projectile = Instantiate(projectilePrefab, 
                transform.position + transform.forward, transform.rotation) as GameObject;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwSpeed, ForceMode.VelocityChange);
            LevelManager.distractions --;
            //AudioSource.PlayClipAtPoint(throwSFX, transform.position);
        }
        else if (Input.GetButtonDown("Fire1") && LevelManager.distractions == 0)
        {
            Debug.Log("no distractions");
        }
    }
}
