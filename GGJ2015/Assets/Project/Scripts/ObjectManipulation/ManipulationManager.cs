using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManipulationManager : LugusSingletonExisting<ManipulationManager>
{
//	public List<ObjectManipulator> manipulators = new List<ObjectManipulator>();

	public GameObject rotateGizmoPrefab = null;
	public GameObject translateGizmoPrefab = null;

	protected ObjectManipulator currentManipulator = null;

	public void SetupLocal()
	{
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
		if (LugusInput.use.down)
		{
			Transform hit = LugusInput.use.RayCastFromMouseDown(LugusCamera.game);

			// If we hit something...
			if (hit != null)
			{

				ObjectManipulator[] manipulators = hit.GetComponentsInParent<ObjectManipulator>(false);
				ObjectManipulator manipulator = null;

				foreach(ObjectManipulator om in manipulators)
				{
					if (om.enabled)
					{
						manipulator = om;
						break;
					}
				}
			
				// Check whether we clicked a manipulator.

				// If yes...
				if (manipulator != null )
				{
					// Is this a different manipulator than the previous one? If no, do nothing. If yes, deactivate the rest and activate the new one.
					if ( manipulator != currentManipulator )
					{
						DeactivateAll();
						currentManipulator = manipulator;
						currentManipulator.Activate();
					}
				}
				else // If no, we hit some other collider. Deactivate all manipulators.
				{
					currentManipulator = null;
					DeactivateAll();
				}
			}
			else // If we hit nothing, deactivate all manipulators.
			{
				currentManipulator = null;
				DeactivateAll();
			}
		}

		if (currentManipulator != null)
			currentManipulator.UpdateManipulator();
	}

	protected void OnGUI()
	{
		if (currentManipulator != null)
		{
			if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 40, 150, 40), "Reset " + currentManipulator.GetType().ToString() ))
			{
				currentManipulator.Reset();
			}
		}
		

	}

	protected void DeactivateAll()
	{
//		foreach(ObjectManipulator manipulator in manipulators)
//		{
//			manipulator.Deactivate();
//		}
	}
}
