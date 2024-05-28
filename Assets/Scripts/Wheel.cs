using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Wheel : MonoBehaviour
{
    private Interactable interactable;
    private Vector3 initPos;
    private Quaternion initRot;
    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        initPos = transform.localPosition;
        initRot =  transform.localRotation;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable.attachedToHand == null){
            transform.localPosition = initPos;
            transform.localRotation = initRot;
        }
        
    }
}
