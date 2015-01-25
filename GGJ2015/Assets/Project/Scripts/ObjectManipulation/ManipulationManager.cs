using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManipulationManager : LugusSingletonExisting<ManipulationManager>
{
	public float maxDetectDistance = 2.0f;
	public GameObject rotateGizmoPrefab = null;
	public GameObject translateGizmoPrefab = null;
	public ManipulationMenu menuPrefab = null;

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


	protected ManipulatorGroup.ManipulationType currentManipulation = ManipulatorGroup.ManipulationType.None;
	protected void Update()
	{

		if (currentManipulatorGroup != null && Input.GetKeyDown(KeyCode.Escape))
		{
			if (currentManipulation != ManipulatorGroup.ManipulationType.None)
			{
				currentManipulation = ManipulatorGroup.ManipulationType.None;
				
				if (PlayerStateManager.use.state == PlayerStateManager.PlayerState.Manipulating)
					PlayerStateManager.use.state = PlayerStateManager.PlayerState.Free;

				if (currentManipulatorGroup.transform.parent == LugusCamera.game.transform)
					currentManipulatorGroup.transform.parent = null;
            }
        }
		else if (currentManipulatorGroup != null && Input.GetKeyDown(KeyCode.M))
		{
//			if (currentManipulation == ManipulatorGroup.ManipulationType.Move)
//			{
//				Debug.Log("end Move");
//				PlayerStateManager.use.state = PlayerStateManager.PlayerState.None;
//				currentManipulation = ManipulatorGroup.ManipulationType.None;
//			}
//			else
//			{
				Debug.Log("Move");
				PlayerStateManager.use.state = PlayerStateManager.PlayerState.Manipulating;
				currentManipulation = ManipulatorGroup.ManipulationType.Move;
			//}
		}
		else if (currentManipulatorGroup != null && Input.GetKeyDown(KeyCode.R))
		{
//			if (currentManipulation == ManipulatorGroup.ManipulationType.Rotate)
//			{
//				Debug.Log("end Rotate");
//				PlayerStateManager.use.state = PlayerStateManager.PlayerState.None;
//				currentManipulation = ManipulatorGroup.ManipulationType.None;
//			}
//			else
//			{
				Debug.Log("Rotate");
				PlayerStateManager.use.state = PlayerStateManager.PlayerState.Manipulating;
				currentManipulation = ManipulatorGroup.ManipulationType.Rotate;
			//}
        }
		else if (currentManipulatorGroup != null && Input.GetKeyDown(KeyCode.G))
		{
//			if (currentManipulation == ManipulatorGroup.ManipulationType.Grab)
//			{
//				Debug.Log("end Grab");
//				PlayerStateManager.use.state = PlayerStateManager.PlayerState.None;
//				currentManipulation = ManipulatorGroup.ManipulationType.None;
//
//				if (currentManipulatorGroup.transform.parent == LugusCamera.game.transform)
//					currentManipulatorGroup.transform.parent = null;
//			}
//			else
//			{
				Debug.Log("Grab");
				//	PlayerStateManager.use.state = PlayerStateManager.PlayerState.Manipulating;
				currentManipulation = ManipulatorGroup.ManipulationType.Grab;
			//}
        }
        else if (currentManipulatorGroup != null && Input.GetKeyDown(KeyCode.L))
		{
			currentManipulatorGroup.locked = !currentManipulatorGroup.locked ;

			if (currentManipulatorGroup.locked)
			    Debug.Log("Locked");
			else
				Debug.Log("Unlocked");


			currentManipulation = ManipulatorGroup.ManipulationType.None;
			
			if (PlayerStateManager.use.state == PlayerStateManager.PlayerState.Manipulating)
				PlayerStateManager.use.state = PlayerStateManager.PlayerState.Free;

			currentManipulation = ManipulatorGroup.ManipulationType.None;
        }
        
        
        if (currentManipulatorGroup != null)
			currentManipulatorGroup.menu.UpdateMenu();
        
		if (currentManipulation != ManipulatorGroup.ManipulationType.None)
		{
			if (currentManipulatorGroup != null && !currentManipulatorGroup.locked)
				currentManipulatorGroup.UpdateManipulators(currentManipulation);
            
		//	return;
		}




		RaycastHit hit;

		if (!Physics.Raycast(LugusCamera.game.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit, maxDetectDistance))
		 {

			// If nothing hit, deactive group hit in last frame.
			if (currentManipulatorGroup != null)
			{
				currentManipulatorGroup.Deactivate();
				
				currentManipulatorGroup = null;
				
				if (PlayerStateManager.use.state == PlayerStateManager.PlayerState.Manipulating)
                	PlayerStateManager.use.state = PlayerStateManager.PlayerState.Free;

				currentManipulation = ManipulatorGroup.ManipulationType.None;
			}

			return;
		}

		ManipulatorGroup group = hit.collider.GetComponentInParent<ManipulatorGroup>();
        
        
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

			if (PlayerStateManager.use.state == PlayerStateManager.PlayerState.Manipulating)
				PlayerStateManager.use.state = PlayerStateManager.PlayerState.Free;
		}





//		if (LugusInput.use.down && !guiRect.Contains(LugusInput.use.currentPosition))
//		{
//			Transform hit = LugusInput.use.RayCastFromMouseDown(LugusCamera.game);
//
//			// If we hit something...
//			if (hit != null)
//			{
//				// Select which manipulator to use...
//				// TODO: Make this work better.
//				ManipulatorGroup group = hit.GetComponentInParent<ManipulatorGroup>();
//			
//				// Check whether we clicked a manipulator.
//
//				// If yes...
//				if (group != null )
//				{
//					PlayerStateManager.use.state = PlayerStateManager.PlayerState.Manipulating;
//
//					// Is this a different manipulator than the previous one? If no, do nothing. If yes, deactivate the rest and activate the new one.
//					if ( group != currentManipulatorGroup )
//					{
//						if (currentManipulatorGroup != null)
//							currentManipulatorGroup.Deactivate();
//
//						currentManipulatorGroup = group;
//						currentManipulatorGroup.Activate();
//					}
//				}
//				else // If no, we hit some other collider. Deactivate all manipulators.
//				{
//					if (currentManipulatorGroup != null)
//						currentManipulatorGroup.Deactivate();
//
//					currentManipulatorGroup = null;
//
//					if (PlayerStateManager.use.state == PlayerStateManager.PlayerState.Manipulating)
//						PlayerStateManager.use.state = PlayerStateManager.PlayerState.Free;
//                }
//			}
//			// FIXME: This should work, but doesn't currently because the OnGUI stuffs also triggers down event when you click on it.
//			else // If we hit nothing, deactivate all manipulators.
//			{
//				if (currentManipulatorGroup != null)
//					currentManipulatorGroup.Deactivate();
//
//				currentManipulatorGroup = null;
//
//				if (PlayerStateManager.use.state == PlayerStateManager.PlayerState.Manipulating)
//					PlayerStateManager.use.state = PlayerStateManager.PlayerState.Free;
//			}
//		}

//		if (currentManipulatorGroup != null && !currentManipulatorGroup.locked)
//			currentManipulatorGroup.UpdateManipulators();
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
