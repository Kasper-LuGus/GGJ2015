using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelection : MonoBehaviour 
{
	public float maxSelectDistance = 2.0f;

	public void SetupLocal()
	{
		// assign variables that have to do with this class only
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
		RaycastHit hit;

		if (!Physics.Raycast(LugusCamera.game.transform.position, LugusCamera.game.transform.forward, out hit, maxSelectDistance))
		{
			return;
		}

		ManipulatorGroup group = hit.collider.GetComponent<ManipulatorGroup>();

		if (group != null)
		{

		}
	}
}
