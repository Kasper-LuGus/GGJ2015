using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectGrabber : ObjectManipulator 
{


	protected Transform originalParent = null;

	public override void UpdateManipulator ()
	{
		if (this.transform.parent != LugusCamera.game.transform)
			this.transform.parent = LugusCamera.game.transform;

		group.attachedRigidbody.constraints = RigidbodyConstraints.FreezePosition;

		//this.collider.enabled = false;
	}


	
	public override void Activate()
	{
		originalParent = this.transform.parent;
	}
	
	public override void Deactivate()
	{
		this.transform.parent = originalParent;
	}
	
	public override void Reset ()
	{

	}
}
