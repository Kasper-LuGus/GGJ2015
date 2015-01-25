using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectGrabber : ObjectManipulator 
{
	bool canMove = false;

	protected Transform originalParent = null;

	public override void UpdateManipulator ()
	{
		print ("hgkcv");

		if (this.transform.parent != LugusCamera.game.transform)
			this.transform.parent = LugusCamera.game.transform;


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
