using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rigidbody;

public class RigidbodySleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) 
            rb.Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
