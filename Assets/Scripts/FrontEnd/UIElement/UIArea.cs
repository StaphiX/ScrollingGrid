using UnityEngine;
using System.Collections;

public class UIArea : UIElement 
{
	UISprite tBackground;

	public void SetBackground(Color tBackgroundColor)
	{
        if (tBackground == null)
        {
            tBackground = new UISprite("blank");
            AddSprite(tBackground);
        }

		tBackground.color = tBackgroundColor;
	}

}
