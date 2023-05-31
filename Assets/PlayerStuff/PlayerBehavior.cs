using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{

    public Text interactText;
    public static bool hiding;

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
        this.ReticleEffect();
    }

    private void ReticleEffect()
    {
        RaycastHit hit;
        if (Physics.Raycast(
            this.transform.position,
            this.transform.forward,
            out hit,
            Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Hidable")
                && Vector3.Distance(
                    this.transform.position,
                    hit.collider.gameObject.transform.position) < 4)
            {

                this.interactText.text = "Press E to Hide";
                this.interactText.gameObject.SetActive(true);
                if(Input.GetKey(KeyCode.E))
                {
                    gameObject.GetComponent<Animator>().SetTrigger("hiding");
                    hiding = true;
                }
                //press e to get out?
            }
            else
            {
                this.interactText.gameObject.SetActive(false);
            }
        }
        else
        {
            this.interactText.gameObject.SetActive(false);
        }
    }
}
