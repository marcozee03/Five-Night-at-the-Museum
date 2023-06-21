using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVTriggerBehavior : MonoBehaviour
{

    private bool hasTriggered;

    public GameObject projectorSpotlight;

    public GameObject tv;

    // Start is called before the first frame update
    void Start()
    {
        this.hasTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!this.hasTriggered)
        {
            this.hasTriggered = true;

            this.projectorSpotlight.SetActive(true);
            this.tv.GetComponent<VideoPlayer>().Play();
        }
    }
}
