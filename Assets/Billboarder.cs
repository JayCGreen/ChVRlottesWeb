using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarder : MonoBehaviour
{
    Vector3 camDir;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("fired");
    }

    // Update is called once per frame
    void Update()
    {
        camDir = Camera.main.transform.forward;

        transform.LookAt(Camera.main.transform.position);
        
        
    }
}
