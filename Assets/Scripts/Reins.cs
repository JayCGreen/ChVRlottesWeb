using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Reins : MonoBehaviour
{
    public Steer steer;

    public Hand handL;
    
    public Hand handR;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(handL.ObjectIsAttached(gameObject) || handR.ObjectIsAttached(gameObject)){
            steer.isSteering=true;
        }
        else{
            steer.isSteering=false;
        }
        
    }
}
