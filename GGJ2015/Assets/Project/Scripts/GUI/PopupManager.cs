using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopupManager : LugusSingletonExisting<PopupManager>
{
	public PopupType test = PopupType.None;
	public string testText = "";


	protected Transform popupParent = null;
	protected Vector3 originalLocation = Vector3.zero;
	protected Vector3 hiddenLocation = Vector3.zero;
	protected Transform imagesParent = null;
	protected TextMeshWrapper textDisplay = null;
	protected bool up = false;

	public enum PopupType
	{
		None = 0,
		Bully = 1,
		MeanGirl = 2,
		Grandma = 3,
		Dog = 4
	}

	public void SetupLocal()
	{
		popupParent = transform.FindChild("Parent");

		originalLocation = popupParent.transform.localPosition;

		hiddenLocation = popupParent.transform.up * -1.0f * 110;

		imagesParent = popupParent.FindChild("Images");

		foreach(Transform t in imagesParent)
		{
			t.gameObject.SetActive(false);
		}

		textDisplay = gameObject.FindComponentInChildren<TextMeshWrapper>(true, "Text");

		Hide(true);
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
		if (up)
		{
			if (LugusInput.use.KeyDown(KeyCode.Return) || LugusInput.use.KeyDown(KeyCode.KeypadEnter))
			{
				Hide(false);
			}
		}

//		if (LugusInput.use.KeyDown(KeyCode.P))
//		{
//			ShowPopup(testText, test);
//		}
	}

	public void ShowPopup(PopupType popup)
	{
		ShowPopup("", popup);
	}

	public void ShowPopup(string text, PopupType popup)
	{
		up = true;

		iTween.Stop(popupParent.gameObject);
		
		popupParent.transform.localPosition = hiddenLocation;
		
		popupParent.gameObject.MoveTo(originalLocation).Time(0.7f).IsLocal(true).EaseType(iTween.EaseType.easeOutBack).Execute();
		
		foreach(Transform t in imagesParent)
		{
			if (t.name == popup.ToString())
				t.gameObject.SetActive(true);
			else
				t.gameObject.SetActive(false);
		}
	
		textDisplay.SetText(text);
	}

	public void Hide(bool immediate)
	{
		up = false;

		iTween.Stop(popupParent.gameObject);

		if (immediate == false)
		{
			popupParent.transform.localPosition = originalLocation;	
			popupParent.gameObject.MoveTo(hiddenLocation).Time(0.7f).IsLocal(true).EaseType(iTween.EaseType.easeInBack).Execute();
		}
		else
		{
			popupParent.transform.localPosition = hiddenLocation;	
		}

	}
}
