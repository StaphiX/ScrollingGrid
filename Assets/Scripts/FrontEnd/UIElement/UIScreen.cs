using UnityEngine;
using System.Collections;

public class UIScreen : UIElement 
{
	public UIScreen(string sName)
	{
		SetStage(new FStage (sName));
		SetPixelOffset (0, 0, Screen.width, Screen.height);
	}

	public virtual void AddElement(UIElement tElement, float fXOffset, float fYOffset, float fWOffset, float fHOffset)
	{
		AddChild(tElement, fXOffset, fYOffset, fWOffset, fHOffset);
		tElement.Init();
	}
}
