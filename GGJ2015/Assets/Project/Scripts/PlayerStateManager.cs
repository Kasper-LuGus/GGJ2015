using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateManager : LugusSingletonExisting<PlayerStateManager>
{
	public enum PlayerState
	{
		None = 0,
		Manipulating = 1,
		Free = 2
	}

	public PlayerState state 
	{
		get
		{
			return _state;
		}
		set
		{
			_state = value;

			if (onStateChanged != null)
			{
				onStateChanged(_state);
			}
		}

	}

	protected PlayerState _state =  PlayerState.Free;
	public delegate void OnStateChanged(PlayerState newState);
	public OnStateChanged onStateChanged = null;

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
	
	}
}
