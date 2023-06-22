using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float destroyDuration = 3f;
    void Start()
    {
        Destroy(gameObject, destroyDuration);
    }
}
