using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManipulatorGroup : MonoBehaviour 
{
	public bool locked = false;

	protected Collider mainCollider = null;
	protected List<ObjectManipulator> manipulators = new List<ObjectManipulator>();
	protected Rigidbody attachedRigidbody = null;

	protected Color oldOutline = Color.black;

	public enum ManipulationType
	{
		None = 0,
		Move = 1,
		Rotate = 2,
		Grab = 3
	}

	public void SetupLocal()
	{
		manipulators.Clear();
		manipulators.AddRange(gameObject.FindComponentsInChildren<ObjectManipulator>(true));

		foreach(ObjectManipulator om in manipulators)
		{
			om.group = this;
		}

		gameObject.CacheComponent<Collider>(ref mainCollider);

		// Rigidbody is not per se required. Not having one could be expected behavior.
		if (attachedRigidbody == null)
		{
			attachedRigidbody = GetComponent<Rigidbody>();
		}
	}
	
	public void SetupGlobal()
	{
		// lookup references to objects / scripts outside of this script
	}
	
	protected void Awake()
	{
		SetupLocal();
	}

	protected void Start() 
	{
		SetupGlobal();
	}
	
	protected void Update() 
	{
	
	}

	public void Activate()
	{
		if (rigidbody != null)
			rigidbody.isKinematic = true;

		foreach(ObjectManipulator om in manipulators)
		{
			om.Activate();
		}

		oldOutline = this.renderer.sharedMaterial.GetColor("_OutlineColor");
		this.renderer.material.SetColor("_OutlineColor", Color.white);
	}

	public void Deactivate()
	{
		foreach(ObjectManipulator om in manipulators)
		{
			om.Deactivate();
		}

		if (rigidbody != null && locked == false)
		{
			rigidbody.isKinematic = false;
			rigidbody.WakeUp();
		}

		this.renderer.material.SetColor("_OutlineColor", oldOutline);
	}

	public void UpdateManipulators(ManipulationType type)
	{
		// TODO FIXME: This makes very little sense...
		if (type == ManipulationType.Move)
		{
			
			foreach(ObjectManipulator om in manipulators)
			{
				if (om is ObjectMover)
					om.UpdateManipulator();
			}
		}
		else if  (type == ManipulationType.Rotate)
		{
			foreach(ObjectManipulator om in manipulators)
			{
				if (om is ObjectRotator)
					om.UpdateManipulator();
			}
		}
		else if  (type == ManipulationType.Grab)
		{
			foreach(ObjectManipulator om in manipulators)
			{
				if (om is ObjectGrabber)
					om.UpdateManipulator();
			}
		}


//		if (manipulators.Count > 0)
//		{
//			manipulators[0].UpdateManipulator();
//		}
	}

	public void Reset()
	{
		foreach(ObjectManipulator om in manipulators)
		{
			om.Reset();
		}
	}
}
