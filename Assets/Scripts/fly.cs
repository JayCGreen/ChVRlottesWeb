using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Fly : MonoBehaviour
{
    public Hand handR;
    public Hand handL;

    public Transform orientation;
    Rigidbody rb;
    private bool justGrabbed;
    private Vector3 initPosR;
    private Vector3 initPosL;
    private Quaternion initRotR;
    private Quaternion initRotL;



    Vector3 moveDirection;

    Vector3 rotateDirection;
    Vector3 m_EulerAngleVelocity;
    
    public AudioSource buzz;
    private float initPitch;

    // Start is called before the first frame update
    void Start()
    {
        initPitch = 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if((handL.currentAttachedObject != null && handL.currentAttachedObject.GetComponent<Wheel>() != null) || (handR.currentAttachedObject != null && handR.currentAttachedObject.GetComponent<Wheel>() != null)){
            if(!justGrabbed){
                initPosL = handL.transform.localPosition;
                initPosR = handR.transform.localPosition;
                initRotR = handR.transform.localRotation;
                initRotL = handL.transform.localRotation;
                justGrabbed = true;
            }
            var handRPosition = handR.transform.localRotation;
            var handLPosition = handL.transform.localRotation;

            var handRP = handR.transform.localPosition;
            var handLP = handL.transform.localPosition;

            //Forward
            if(Math.Abs(handLPosition.x - handRPosition.x) > 0.25)
            {
                orientation.Rotate(new Vector3(0, (handLPosition.x - handRPosition.x), 0));
                buzz.pitch = (handLPosition.x - handRPosition.x)/180 * 3;

            }
                
            else{
                var delta = Vector3.forward * (handLPosition.x - initRotL.x)*0.25f;
                if (inBounds(orientation.position + delta))
                {
                    //viewHelper.SetActive(true);
                    buzz.pitch = 3;
                    orientation.Translate(delta);
                }
                    
            }
                
            
            //Up & Down
            if(Math.Abs(handLP.y - initPosL.y) > 0.25 &&  Math.Abs(handRP.y - initPosR.y) > 0.25){
                var delta = new Vector3(0, Math.Min(handLP.y - initPosL.y, handRP.y - initPosR.y)*.05f, 0);
                if(inBounds(orientation.position + delta))
                   { 
                        buzz.pitch = 3;
                        orientation.Translate(delta);
                   }

            }


        }else{
            justGrabbed = false;
            buzz.pitch = initPitch;
            //viewHelper.SetActive(false);

        }
        
    }

    bool inBounds(Vector3 t){
        if (t.x > 20 || t.x < -20){
            return false;
        }
        if(t.y > 25 || t.y < 0){
            return false;
        }
        if(t.z > 13 || t.z < -25){
            return false;
        }
        return true;
    }
}
