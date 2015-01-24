using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ManipulatorGroup))]
public abstract class ObjectManipulator : MonoBehaviour 
{
	public float gizmoScale = 1.05f;
	public Collider selectCollider = null;

	public virtual void SetupLocal()
	{

	}
	
	public virtual void SetupGlobal()
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

	public abstract void UpdateManipulator();
	
	public abstract void Activate();

	public abstract void Deactivate();

	public abstract void Reset();
}
