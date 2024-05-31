using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Steer : MonoBehaviour
{
    public GameObject rider;
    
    public GameObject mount;

    public GameObject mountedRider;
    public bool isSteering = false;
    private Vector3 restPosition;
    private Quaternion restRotation;
    public float turnThreshold;
    public float turnFactor;
    public float speedFactor;
    private float convertedSpeed;
    private float[] speeds = {0, 1, 1.5f, 2};
    private int currSpeed = 0;
    private Hand grabbing;
    public bool crash;

    private int period;

    // Start is called before the first frame update
    void Start()
    {
        convertedSpeed = speedFactor/(30);
        period = -1;
        restPosition = transform.localPosition;
        restRotation = transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(convertedSpeed*speeds[currSpeed]);
        period = currSpeed > 0 ? (period + 1)%10: 0;
        if (crash)
        {
            Debug.Log("Crashed");
            currSpeed = 0;
            crash = false;
        }
        
        mountedRider.transform.Translate(new Vector3(0, currSpeed > 0 ? (period <= 4 ? .05f: -.05f) : 0, convertedSpeed*speeds[currSpeed]));
        if (isSteering){
            //Gear Shift
            if (transform.parent.GetComponent<Hand>()!=null){
                    //Debug.Log("say somethin not given up on yuo");
                    grabbing = transform.parent.GetComponent<Hand>();
                    if(grabbing.grabGripAction.state){
                        if(grabbing.grabPinchAction.stateDown){
                            currSpeed = (currSpeed+1)%4;
                        }
                    }
                    if(period==4){
                        grabbing.TriggerHapticPulse(10);
                    }
                    //grabbing.
                }
            if (Math.Abs(rider.transform.rotation.eulerAngles.y) < 30){
                //Mount and player facing the same direction, go forward or backwards
                
            }
            else{
                if(Math.Abs(transform.parent.localPosition.x) > turnThreshold){
                    if(transform.parent.transform.localPosition.x > 0 ){
                        mountedRider.transform.Rotate(new Vector3(0 , turnFactor, 0));
                    }
                    else {
                        mountedRider.transform.Rotate(new Vector3(0 , -turnFactor, 0));
                    }
                }


            }
        } else{
            transform.SetPositionAndRotation(restPosition, restRotation);
        }
        
    }

}
