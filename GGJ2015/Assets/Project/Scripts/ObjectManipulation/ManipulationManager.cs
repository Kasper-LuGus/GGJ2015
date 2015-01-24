using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManipulationManager : LugusSingletonExisting<ManipulationManager>
{
	public GameObject rotateGizmoPrefab = null;
	public GameObject translateGizmoPrefab = null;

	protected ManipulatorGroup currentManipulatorGroup = null;
	protected Rect guiRect = new Rect(Screen.width- 200, 0, 200, Screen.height);

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
		if (LugusInput.use.down && !guiRect.Contains(LugusInput.use.currentPosition))
		{
			Transform hit = LugusInput.use.RayCastFromMouseDown(LugusCamera.game);

			// If we hit something...
			if (hit != null)
			{
				// Select which manipulator to use...
				// TODO: Make this work better.
				ManipulatorGroup group = hit.GetComponent<ManipulatorGroup>();
			
				// Check whether we clicked a manipulator.

				// If yes...
				if (group != null )
				{
					// Is this a different manipulator than the previous one? If no, do nothing. If yes, deactivate the rest and activate the new one.
					if ( group != currentManipulatorGroup )
					{
						if (currentManipulatorGroup != null)
							currentManipulatorGroup.Deactivate();

						currentManipulatorGroup = group;
						currentManipulatorGroup.Activate();
					}
				}
				else // If no, we hit some other collider. Deactivate all manipulators.
				{
					if (currentManipulatorGroup != null)
						currentManipulatorGroup.Deactivate();

					currentManipulatorGroup = null;
				}
			}
			// FIXME: This should work, but doesn't currently because the OnGUI stuffs also triggers down event when you click on it.
			else // If we hit nothing, deactivate all manipulators.
			{
				if (currentManipulatorGroup != null)
					currentManipulatorGroup.Deactivate();

				currentManipulatorGroup = null;
			}
		}

		if (currentManipulatorGroup != null && !currentManipulatorGroup.locked)
			currentManipulatorGroup.UpdateManipulators();
	}

	protected void OnGUI()
	{
		if (currentManipulatorGroup != null)
		{
			GUI.Box(guiRect, "");

			GUILayout.BeginArea(guiRect);

			if (GUILayout.Button("Reset " + currentManipulatorGroup.GetType().ToString() ))
			{
				currentManipulatorGroup.Reset();
			}

			string toggleText = "Lock";

			if (currentManipulatorGroup.locked)
				toggleText = "Unlock";

			if (GUILayout.Button(toggleText + " " + currentManipulatorGroup.GetType().ToString() ))
			{
				currentManipulatorGroup.locked = !currentManipulatorGroup.locked;
			}

			GUILayout.EndArea();
		}
		

	}
}
