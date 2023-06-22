using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBreak : MonoBehaviour
{
    //public GameObject windowPieces;
    public float explosionForce = 10f;
    public float explosionRadius = 5f;
    public AudioClip windowBreak;
    public GameObject[] brokenPieces;
    //public GameObject id;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Invoke("Break", 1);
    }
    /*
    void Break()
    {
        Debug.Log("pos " + transform.position + " dl "  + LevelManager.distractLoc);
        float dist = Vector3.Distance(LevelManager.distractLoc, transform.position);
        Debug.Log("dist: " + dist);
        if(LevelManager.distracted && dist <= 4)
        {
            // Transform currentWindow = gameObject.transform;
            // GameObject pieces = Instantiate(
            //     windowPieces, currentWindow.position, currentWindow.rotation);
            // Rigidbody[] rbPieces = pieces.GetComponentsInChildren<Rigidbody>();
            // foreach(Rigidbody rb in rbPieces)
            // {
            //     rb.AddExplosionForce(explosionForce, currentWindow.position, explosionRadius);
            // }
            foreach(GameObject broken in brokenPieces)
            {
                broken.SetActive(true);
            }
            //id.SetActive(true);
            AudioSource.PlayClipAtPoint(windowBreak, transform.position);
            //LevelManager.distracted = true;
            //LevelManager.distractLoc = transform.position;
            //LevelManager.distractLoc -= Vector3.one;
            Destroy(gameObject);
        }
    }
    */
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            // Transform currentWindow = gameObject.transform;
            // GameObject pieces = Instantiate(
            //     windowPieces, currentWindow.position, currentWindow.rotation);
            // Rigidbody[] rbPieces = pieces.GetComponentsInChildren<Rigidbody>();
            // foreach(Rigidbody rb in rbPieces)
            // {
            //     rb.AddExplosionForce(explosionForce, currentWindow.position, explosionRadius);
            // }
            // broken.SetActive(true);
            // //id.SetActive(true);
            // AudioSource.PlayClipAtPoint(windowBreak, transform.position);
            // LevelManager.distracted = true;
            // LevelManager.distractLoc = transform.position;
            // LevelManager.distractLoc -= Vector3.one;
            // Destroy(gameObject);
        }
    }
}
