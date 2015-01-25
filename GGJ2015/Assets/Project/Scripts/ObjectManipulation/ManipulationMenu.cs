using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManipulationMenu : LugusSingletonExisting<ManipulationMenu>
{
	protected Transform lockThis = null;
	protected Transform unlockThis = null;
	protected Transform move = null;
	protected Transform rotate = null;
	protected Transform grab = null;
	protected Transform exit = null;

	public void SetupLocal()
	{
		lockThis = transform.FindChild("Lock");
		unlockThis = transform.FindChild("Unlock");
		move = transform.FindChild("Move");
		rotate = transform.FindChild("Rotate");
		grab = transform.FindChild("Grab");
		exit = transform.FindChild("Exit");
	}

	public void Hide()
	{
		foreach(Renderer r in GetComponentsInChildren<Renderer>(false))
		{
			r.enabled = false;
		}
	}

	public void Show()
	{
		foreach(Renderer r in GetComponentsInChildren<Renderer>(false))
		{
			r.enabled = true;
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


	public void UpdateMenu(ManipulatorGroup group)
	{
		Vector3 newPos = LugusCamera.game.WorldToViewportPoint(group.transform.position);
		newPos = LugusCamera.ui.ViewportToWorldPoint(newPos).zAdd(10);
		
		//this.transform.position = Vector3.Lerp(this.transform.position, newPos, Time.deltaTime) * 0.25f;

		this.transform.position = newPos;

		if (ManipulationManager.use.currentManipulation != ManipulatorGroup.ManipulationType.None)
		{
			exit.gameObject.SetActive(true);
		}
		else
		{
			exit.gameObject.SetActive(false);
		}

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
