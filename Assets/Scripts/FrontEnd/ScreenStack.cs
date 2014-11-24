using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScreenStack {
	
	public static List<UIScreen> tStack = new List<UIScreen>();
	public static FStage tBackgroundStage = new FStage("BACKGROUND");
	public static FStage tTextStage = new FStage("TEXT");

	public static void Init()
	{

	}

	public static void ResizeScreens(int iScreenW, int iScreenH)
	{
		for (int iScreen = 0; iScreen < tStack.Count; ++iScreen) 
		{
			UIScreen tScreen = tStack[iScreen];
			tScreen.SetPixelOffset(0,0,iScreenW, iScreenH);
		}
	}

	public static void Add(UIScreen tScreen)
	{
		tScreen.Init();
		tStack.Add(tScreen);
		Futile.AddStage(tScreen.GetStage ());
	}

	public static void Update()
	{
		if(tStack.Count > 0)
		{
			tStack[0].Update();
			tStack[0].UpdateChildren();
		}
	}

	public static void GUIDisplay()
	{
		if(tStack.Count > 0)
		{
			tStack[0].GUIDisplay();
		}
	}
	

}
