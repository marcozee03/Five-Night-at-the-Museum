using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{

    //public Text interactText;
    public static bool hiding;
    int enemyHit = 0;

    // Start is called before the first frame update
    void Start()
    {
        hiding = false;
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        //this.ReticleEffect();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // enemyHit ++;
            // if(enemyHit == 2)
            // {
            //     //LevelManager lm = FindObjectOfType<LevelManager>();
            //     //lm.LevelLost();
            //     Destroy(gameObject);
            // }
            FindObjectOfType<LevelManager>().LevelLost();
            //Destroy(gameObject);   
        }
    }

    //private void ReticleEffect()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(
    //        this.transform.position,
    //        this.transform.forward,
    //        out hit,
    //        Mathf.Infinity))
    //    {
    //        if (hit.collider.CompareTag("Hidable")
    //            && Vector3.Distance(
    //                this.transform.position,
    //                hit.collider.gameObject.transform.position) < 4)
    //        {

    //            this.interactText.text = "Press E to Hide";
    //            this.interactText.gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            this.interactText.gameObject.SetActive(false);
    //        }
    //    }
    //    else
    //    {
    //        this.interactText.gameObject.SetActive(false);
    //    }
    //}
}
