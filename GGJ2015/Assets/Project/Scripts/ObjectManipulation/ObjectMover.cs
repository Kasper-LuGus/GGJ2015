using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectMover : ObjectManipulator 
{
	public float smoothMultiplier = 6.0f;
		
	protected Transform gizmo = null;
	protected Vector3 originalPosition = Vector3.zero;
	protected Vector3 lastInputPosition = Vector3.zero;
	protected Vector3 currentOffset = Vector3.zero;
	
	public override void SetupLocal()
	{
		base.SetupLocal();
		
		originalPosition = this.transform.localPosition;
		
		if (gizmo == null)
		{
			GameObject gizmoInstance = (GameObject) Instantiate(ManipulationManager.use.translateGizmoPrefab);

			BoxCollider attachedCollider = gizmoInstance.GetComponent<BoxCollider>();
			selectCollider = attachedCollider;

			gizmo = gizmoInstance.transform;
			gizmo.name = this.transform.name + "_ObjectMover_Gizmo";
			gizmo.parent = this.transform;
			
			gizmo.localScale = Vector3.one * gizmoScale;
			gizmo.localPosition = Vector3.zero;
			gizmo.localRotation = Quaternion.identity;
			
			gizmo.renderer.enabled = false;
		}
		
		
	}
	
	public override void SetupGlobal()
	{
		base.SetupGlobal();
	}
	
	public override void UpdateManipulator ()
	{

		if (LugusInput.use.down)
		{
			currentOffset = this.transform.position - LugusInput.use.ScreenTo3DPoint(LugusInput.use.currentPosition, this.transform.position, LugusCamera.game);
		}

		if (LugusInput.use.dragging || LugusInput.use.down)
		{
			// Lerp the translation a bit.
			Vector3 newPosition = LugusInput.use.ScreenTo3DPoint(LugusInput.use.currentPosition, this.transform.position, LugusCamera.game) + currentOffset;

			this.transform.position = Vector3.Lerp(this.transform.position, newPosition, Time.deltaTime * smoothMultiplier);
		}
		
	}
	
	public override void Activate()
	{
		gizmo.renderer.enabled = true;
	}
	
	public override void Deactivate()
	{
		gizmo.renderer.enabled = false;
	}
	
	public override void Reset ()
	{
		this.transform.localPosition = originalPosition;
	}
}
