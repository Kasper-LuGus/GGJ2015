using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectMover : ObjectManipulator 
{
	public float smoothMultiplier = 6.0f;
	public float moveSpeed = 3.0f;
		
	protected Transform gizmo = null;
	protected Vector3 originalPosition = Vector3.zero;
	protected Vector3 lastInputPosition = Vector3.zero;
	protected Vector3 currentOffset = Vector3.zero;
	protected Vector3 referencePosition = Vector3.zero;

	protected BoxCollider up = null;
	protected BoxCollider right = null;
	protected BoxCollider forward = null;
	
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

			up = gizmo.gameObject.FindComponentInChildren<BoxCollider>(true, "Up");
			right = gizmo.gameObject.FindComponentInChildren<BoxCollider>(true, "Right");
			forward = gizmo.gameObject.FindComponentInChildren<BoxCollider>(true, "Forward");
			
			gizmo.gameObject.SetActive(false);


		}
		
		
	}
	
	public override void SetupGlobal()
	{
		base.SetupGlobal();
	}

	protected Transform currentGizmo = null;
	public override void UpdateManipulator ()
	{

		this.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"), Space.World);


		this.transform.Translate(LugusCamera.game.transform.right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"), Space.World);
        
	}
	
	public override void Activate()
	{
		gizmo.gameObject.SetActive(true);
	}
	
	public override void Deactivate()
	{
		gizmo.gameObject.SetActive(false);
	}
	
	public override void Reset ()
	{
		this.transform.localPosition = originalPosition;
	}
}
