using UnityEngine;
using System.Collections;

public class UIButton : UIElement 
{
	UI9Slice m_t9Slice = new UI9Slice();
	UITransition m_tTransition;

	public override void Init()
	{
		m_t9Slice.Init();
		AddChild(m_t9Slice, 0.5f, 0.5f, 1.0f, 1.0f);

		m_tTransition = new UITransition(ETransition.IN, null, Transition, null, 0.5f);
	}

	public override void Update()
	{
		if(InputManager.IsReleaseInRect(GetRect()))
		{
			if(GetRect().width < 100)
				m_tTransition.Init(ETransition.IN, null, Transition, null, 0.5f);
			else
				m_tTransition.Init(ETransition.OUT, null, Transition, null, 0.5f);
		}

		m_tTransition.UpdateTransition();
	}

	public void SetSprites(string[] sCornerSprites, string[] sSideSprites, string[] sMiddleSprites, int iCornerSize)
	{
		m_t9Slice.SetSprites(sCornerSprites, sSideSprites, sMiddleSprites, iCornerSize);
	}

	public void SetColour(Color tColour)
	{
		m_t9Slice.SetSpriteColours(tColour);
	}

	public override void Resize ()
	{
		base.Resize ();
	}

	public bool Transition(float fTransitionTime, float fTransitionDuration)
	{
		float fTransW = GetRect(false).width - 40;
		double dButtonW = (float)Easing.Ease(fTransitionTime, -fTransW, fTransW, fTransitionDuration, Easing.EaseType.ExpoEaseOut);
		SetExtraOffsetWidth((float)dButtonW, false);
		return false;
	}
}
