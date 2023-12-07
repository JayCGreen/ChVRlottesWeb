using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SimpleAttach : MonoBehaviour
{
	private Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
		interactable = GetComponent<Interactable>();
    }

	// Update is called once per frame

	//-------------------------------------------------
	private void HandHoverUpdate(Hand hand)
	{
		GrabTypes grabType = hand.GetGrabStarting();
		bool isGrabEnding = hand.IsGrabEnding(gameObject);

		if (interactable.attachedToHand == null && grabType != GrabTypes.None)
        {
			if (transform.childCount > 1)
				transform.GetChild(1).gameObject.SetActive(false);
			hand.AttachObject(gameObject, grabType);
			hand.HoverLock(interactable);
        }

		else if (isGrabEnding)
        {
			if (transform.childCount > 1){
				transform.GetChild(1)?.gameObject.SetActive(true);
				transform.Translate(new Vector3(0.5f, 0.5f, 0f));
			}
				
			hand.DetachObject(gameObject);
			hand.HoverUnlock(interactable);
        }
	}
}
