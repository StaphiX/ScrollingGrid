using UnityEngine;
using System.Collections;

public class Frontend : MonoBehaviour {

	public int iScreenHeight = 0;
	public int iScreenWidth = 0;
	public string sStartScreen = "Main";
	// Use this for initialization
	void Start () {
	
		InitFutile();
		ScreenStack.Init();

		if (sStartScreen == "Main")
			ScreenStack.Add(new UIMainScreen("UIMain"));
	}
	
	// Update is called once per frame
	void Update () {
	
		InputManager.Update(); //Gets all the inputs for this frame
		ScreenStack.Update();
		if (iScreenHeight != Screen.height || iScreenWidth != Screen.width) 
		{
			iScreenHeight = Screen.height;
			iScreenWidth = Screen.width;
			ScreenStack.ResizeScreens(iScreenWidth, iScreenHeight);
		}

	}

	void OnGUI()
	{
		ScreenStack.GUIDisplay();
	}

	
	void InitFutile()
	{
		FutileParams tParams = new FutileParams (false, false, true, false);
		tParams.AddResolutionLevel (Screen.height, 1.0f, 1.0f, "");
		Futile.instance.Init(tParams);
		
		FutileLoadAssets();
	}
	
	void FutileLoadAssets()
	{
		Futile.atlasManager.LoadAllTextures();
		Futile.atlasManager.LoadAllAtlases();
	}

	void OnApplicationQuit()
	{
	}
}
