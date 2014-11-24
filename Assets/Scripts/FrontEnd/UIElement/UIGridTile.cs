using UnityEngine;
using System.Collections;

public class UIGridTile : UIElement 
{
	UISprite tBackground;
    Color m_tDefaultColor = Color.white;

    public override void Init()
    {

    }

    public override void Update()
    {
        if (tBackground == null)
            return;
        if(InputManager.IsPressInRect(GetRect()))
        {
            tBackground.color = Color.white;
        }
        else
        {
            tBackground.color = m_tDefaultColor;
        }
    }

	public void SetBackground(Color tBackgroundColor, int iPadding)
	{
        m_tDefaultColor = tBackgroundColor;
        if (tBackground == null)
        {
            tBackground = new UISprite("blank");
            AddSprite(tBackground);
            tBackground.SetPixelOffset(-iPadding, -iPadding, -iPadding * 2, -iPadding * 2);
        }

		tBackground.color = tBackgroundColor;
    }
}
