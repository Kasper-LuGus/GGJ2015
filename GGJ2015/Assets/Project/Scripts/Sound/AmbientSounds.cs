using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmbientSounds : MonoBehaviour 
{
	public List<AudioClip> ambientLoops = new List<AudioClip>();

	public void SetupLocal()
	{
		// assign variables that have to do with this class only
	}
	
	public void SetupGlobal()
	{
		foreach(AudioClip clip in ambientLoops)
		{
			LugusAudio.use.Ambient().Play(clip, false, new LugusAudioTrackSettings().Loop(true));
		}
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
