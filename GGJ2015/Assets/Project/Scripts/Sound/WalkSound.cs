using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkSound : MonoBehaviour 
{
	protected Vector3 previousPoint = Vector3.zero;
	public AudioClip[] walkSounds = null;

	protected ILugusAudioTrack track = null;
	bool walking = false;

	public void SetupLocal()
	{
		previousPoint = this.transform.position;
	}
	
	public void SetupGlobal()
	{
		LugusCoroutines.use.StartRoutine(WalkSoundLoop());
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
		float distance = Vector3.Distance(this.transform.position, previousPoint.y(this.transform.position.y));

		if (distance > 0.5f * Time.deltaTime)
			walking = true;
		else
			walking = false;

		previousPoint = this.transform.position;
	}

	protected IEnumerator WalkSoundLoop()
	{
		while (Application.isPlaying)
		{
			yield return new WaitForSeconds(0.5f);

			if (walking)
				LugusAudio.use.SFX().Play(walkSounds[Random.Range(0, walkSounds.Length)], false, new LugusAudioTrackSettings().Volume(0.25f));

		}
	}
}
