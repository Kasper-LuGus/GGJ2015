using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManipulatorGroup : MonoBehaviour 
{
	public bool locked = false;

	protected Collider mainCollider = null;
	protected List<ObjectManipulator> manipulators = new List<ObjectManipulator>();
	protected Rigidbody attachedRigidbody = null;

	public void SetupLocal()
	{
		manipulators.Clear();
		manipulators.AddRange(gameObject.FindComponentsInChildren<ObjectManipulator>(true));

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
	}

	public void UpdateManipulators()
	{
		if (manipulators.Count > 0)
		{
			manipulators[0].UpdateManipulator();
		}
	}

	public void Reset()
	{
		foreach(ObjectManipulator om in manipulators)
		{
			om.Reset();
		}
	}
}
