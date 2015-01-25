using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManipulationMenu : MonoBehaviour 
{
	protected Transform lockThis = null;
	protected Transform unlockThis = null;
	protected Transform move = null;
	protected Transform rotate = null;
	protected Transform grab = null;
	public ManipulatorGroup group;

	public void SetupLocal()
	{
		lockThis = transform.FindChild("Lock");
		unlockThis = transform.FindChild("Unlock");
		move = transform.FindChild("Move");
		rotate = transform.FindChild("Rotate");
		grab = transform.FindChild("Grab");

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
		if (group == null)
			return;

		Vector3 newPos = LugusCamera.game.WorldToViewportPoint(group.transform.position);
		newPos = LugusCamera.ui.ViewportToWorldPoint(newPos).zAdd(10);

		this.transform.position = newPos;
		//this.transform.LookAt(LugusCamera.game.transform);
	}

	public void UpdateMenu()
	{
		if (group.locked)
		{
			lockThis.gameObject.SetActive(false);
			unlockThis.gameObject.SetActive(true);
		}
		else
		{
			lockThis.gameObject.SetActive(true);
			unlockThis.gameObject.SetActive(false);
		}

		if (group.locked)
		{
			move.gameObject.SetActive(false);
			rotate.gameObject.SetActive(false);
			grab.gameObject.SetActive(false);
		}
		else
		{
			move.gameObject.SetActive(true);
			rotate.gameObject.SetActive(true);
			grab.gameObject.SetActive(true);
		}
	}
}
