using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectRotator : ObjectManipulator 
{
	public float rotateSpeed = 5.0f;

	protected Transform gizmo = null;
	protected Quaternion originalRotation = Quaternion.identity;



	public override void SetupLocal()
	{
		base.SetupLocal();

		originalRotation = this.transform.localRotation;

		if (gizmo == null)
		{
			GameObject gizmoInstance = (GameObject) Instantiate(ManipulationManager.use.rotateGizmoPrefab);

			SphereCollider attachedCollider = gizmoInstance.GetComponent<SphereCollider>();
			selectCollider = attachedCollider;

			gizmo = gizmoInstance.transform;
			gizmo.name = this.transform.name + "_Gizmo";
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

		if (!LugusInput.use.dragging)
			return;

		transform.Rotate(LugusCamera.game.transform.up, -1.0f * Input.GetAxis("Mouse X") * rotateSpeed, Space.World );
		transform.Rotate(LugusCamera.game.transform.right, Input.GetAxis("Mouse Y") * rotateSpeed, Space.World );

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
		this.transform.localRotation = originalRotation;
	}
}
