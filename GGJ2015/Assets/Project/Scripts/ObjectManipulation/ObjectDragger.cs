using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectDragger : ObjectManipulator 
{
	bool canMove = false;


	public override void UpdateManipulator ()
	{
		canMove = true;
		
		if (LugusInput.use.down)
		{
		//	currentOffset = this.transform.position - LugusInput.use.ScreenTo3DPoint(LugusInput.use.currentPosition, this.transform.position, LugusCamera.game);
		}
	}

	protected void FixedUpdate () 
	{
		if (!canMove)
			return;


//		if (LugusInput.use.dragging || LugusInput.use.down)
//		{
//			// Lerp the translation a bit.
//			Vector3 newPosition = LugusInput.use.ScreenTo3DPoint(LugusInput.use.currentPosition, this.transform.position, LugusCamera.game) + currentOffset;
//			
//			this.transform.position = Vector3.Lerp(this.transform.position, newPosition, Time.deltaTime * smoothMultiplier);
//
//
//		}

	}
	
	public override void Activate()
	{

	}
	
	public override void Deactivate()
	{
		canMove = false;
	}
	
	public override void Reset ()
	{

	}
}
