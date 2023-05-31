using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBreak : MonoBehaviour
{
    public GameObject windowPieces;
    public float explosionForce = 100f;
    public float explosionRadius = 5f;
    public AudioClip windowBreak;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            Transform currentWindow = gameObject.transform;
            GameObject pieces = Instantiate(
                windowPieces, currentWindow.position, currentWindow.rotation);
            Rigidbody[] rbPieces = pieces.GetComponentsInChildren<Rigidbody>();
            foreach(Rigidbody rb in rbPieces)
            {
                rb.AddExplosionForce(explosionForce, currentWindow.position, explosionRadius);
            }
            AudioSource.PlayClipAtPoint(windowBreak, transform.position);
            LevelManager.distracted = true;
            LevelManager.distractLoc = transform.position;
            LevelManager.distractLoc -= Vector3.one;
            Destroy(gameObject);
        }
    }
}
