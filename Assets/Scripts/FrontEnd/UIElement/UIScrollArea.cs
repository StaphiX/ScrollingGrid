using UnityEngine;
using System.Collections;

public class UIScrollArea : UIElement 
{
	bool m_bShouldScrollY = false;
	bool m_bShouldScrollX = false;

	Rect m_tTouchArea;
	
	float m_fScrollXMax = 0.0f;
	float m_fScrollYMax = 0.0f;
	float m_fVelocityX = 0.0f;
	float m_fVelocityY = 0.0f;
	bool  m_bTouching = false;

	//Scrolling constants
	float fMinVelocity = 0.01f;
	float fMinVelocityForDeceleration = 1.0f;
	float fDecelerationFrictionFactor = 0.95f;
	float fSpringConstant =  0.55f;

	public override void AddChild(UIElement tElement, float fXOffset, float fYOffset, float fWOffset, float fHOffset)
	{
		base.AddChild(tElement, fXOffset, fYOffset, fWOffset, fHOffset);
		tElement.SetShader(GetShader(), true);
		UpdateElement(tElement);
	}

	public void UpdateElement(UIElement tElement)
	{
		Rect tRect = GetRect();
		Rect tElementRect = tElement.GetRect();

		float fBottom = tRect.y - tRect.height / 2;
		float fElementBottom = tElementRect.y - tElementRect.height / 2;

		float fRight = tRect.x + tRect.width / 2;
		float fElementRight = tElementRect.x + tElementRect.width / 2;

		if (fElementBottom < fBottom) 
		{
			m_bShouldScrollY = true;
			m_fScrollYMax = Mathf.Max(m_fScrollYMax, Mathf.Abs(fBottom - fElementBottom));
		}

		if (fElementRight > fRight) 
		{
			m_bShouldScrollX = true;
			m_fScrollXMax = Mathf.Max(m_fScrollYMax, Mathf.Abs(fRight - fElementRight));
		}
	}

	public override void Update()
	{
		UpdateScrolling();
		if (!m_bTouching) 
		{
			if (m_fVelocityX != 0.0f || m_fVelocityY != 0.0f)
				AddVelocity ();
			SnapToEdges();
		}
	}

	void UpdateScrolling()
	{
		InputTouch tTouch = InputManager.GetHeldInRect(m_tTouchArea);
		if (tTouch != null) 
		{
			Vector2 vOffset = tTouch.GetTouchOffset();
			SetOffsets (vOffset.x, -vOffset.y, true);
			SetVelocity(vOffset.x, -vOffset.y);
			m_bTouching = true;
		} 
		else 
		{
			m_bTouching = false;
		}

	}

	public float GetBoundryDistanceX()
	{
		if (GetExtraOffset().x < 0.0f) 
		{
			return 0.0f - GetExtraOffset().x;
		}
		else if (GetExtraOffset().x > m_fScrollXMax) 
		{
			return GetExtraOffset().x - m_fScrollXMax;
		} else return 0.0f;
	}

	public float GetBoundryDistanceY()
	{
		if (GetExtraOffset().y < 0.0f) 
		{
			return GetExtraOffset().y;
		}
		else if (GetExtraOffset().y > m_fScrollYMax) 
		{
			return GetExtraOffset().y - m_fScrollYMax;
		} else return 0.0f;
	}


	public void SetOffsets(float fX, float fY, bool bAdd)
	{
		if (bAdd) 
		{
			//Check if we are in the snap boundry and scale offsets
			float fBoundryDistX = GetBoundryDistanceX();
			float fBoundryDistY = GetBoundryDistanceY();
			if(fBoundryDistX != 0.0f)
			{
				float fScale = 1.0f - (Mathf.Abs(fBoundryDistX) / (fSpringConstant * m_tTouchArea.width));
				fScale = Mathf.Clamp(fScale, 0.0f, 1.0f);
				if((fBoundryDistX < 0 && fX < 0) || (fBoundryDistX > 0 && fX > 0))
					fX *= fScale;
			}

			if(fBoundryDistY  != 0.0f)
			{
				float fScale = 1.0f - (Mathf.Abs(fBoundryDistY) / (fSpringConstant * m_tTouchArea.height));
				fScale = Mathf.Clamp(fScale, 0.0f, 1.0f);

				if((fBoundryDistY < 0 && fY < 0) || (fBoundryDistY > 0 && fY > 0))
					fY *= fScale;
			}

			if(m_bShouldScrollX)
				SetExtraOffsetX(fX, true);
			if(m_bShouldScrollY)
				SetExtraOffsetY(fY, true);
		} 
		else 
		{
			if(m_bShouldScrollX)
				SetExtraOffsetX(fX, false);
			if(m_bShouldScrollY)
				SetExtraOffsetY(fY, false);
		}

		CalculateRect();
	}

	public override void Resize()
	{
		m_tTouchArea = GetRect(false);
		FClipShader tClipShader = (FClipShader)GetShader();
		if (tClipShader == null) 
		{
			SetShader (new FClipShader (m_tTouchArea), true);
			tClipShader = (FClipShader)GetShader();
		}
		tClipShader.tClipRect = m_tTouchArea;
		base.Resize();
	}

	public void SetVelocity(float fX, float fY)
	{
		if(m_bShouldScrollX && Mathf.Abs(fX) > fMinVelocityForDeceleration)
			m_fVelocityX = fX;
		else
			m_fVelocityX = 0.0f;

		if(m_bShouldScrollY && Mathf.Abs(fY) > fMinVelocityForDeceleration)
			m_fVelocityY = fY;
		else
			m_fVelocityY = 0.0f;
	}

	public void Decelerate()
	{
		float fAbsVelocityX = Mathf.Abs (m_fVelocityX);
		float fAbsVelocityY = Mathf.Abs (m_fVelocityY);
		if (fAbsVelocityY > fMinVelocityForDeceleration) 
		{
			m_fVelocityY *= fDecelerationFrictionFactor;
			if(m_fVelocityY < 0.0f)
				m_fVelocityY = Mathf.Min(-fMinVelocity, m_fVelocityY);
			else
				m_fVelocityY = Mathf.Max(m_fVelocityY, fMinVelocity);
		} 
		else 
		{
			m_fVelocityY = 0.0f;
		}

		if (fAbsVelocityX > fMinVelocityForDeceleration) 
		{
			m_fVelocityX *= fDecelerationFrictionFactor;
			if(m_fVelocityX < 0.0f)
				m_fVelocityX = Mathf.Min(-fMinVelocity, m_fVelocityX);
			else
				m_fVelocityX = Mathf.Max(m_fVelocityX, fMinVelocity);
		} 
		else 
		{
			m_fVelocityX = 0.0f;
		}
	}

	public void AddVelocity()
	{
		Decelerate();
		SetOffsets(m_fVelocityX, m_fVelocityY, true);
	}

	public void SnapToEdges()
	{
		//Check if we are in the snap boundry and scale offsets
		float fBoundryDistX = GetBoundryDistanceX();
		float fBoundryDistY = GetBoundryDistanceY();

		if (fBoundryDistX != 0.0f) 
		{
			float fSnapAmount = (1.0f - (1.0f / ((fBoundryDistX * fSpringConstant / m_tTouchArea.width) + 1.0f))) * m_tTouchArea.width;
			if(fBoundryDistX < 0)
			{
				fSnapAmount = Mathf.Clamp(fSnapAmount, fBoundryDistX, 0.0f);
			}
			else
			{
				fSnapAmount = Mathf.Clamp(fSnapAmount, 0.0f, fBoundryDistX);
			}
			SetOffsets(-fSnapAmount, 0, true);
		}
		if (fBoundryDistY != 0.0f) 
		{
			float fSnapAmount = (1.0f - (1.0f / ((fBoundryDistY * fSpringConstant / m_tTouchArea.height) + 1.0f))) * m_tTouchArea.height;
			if(fBoundryDistY < 0)
			{
				fSnapAmount = Mathf.Clamp(fSnapAmount, fBoundryDistY, 0.0f);
			}
			else
			{
				fSnapAmount = Mathf.Clamp(fSnapAmount, 0.0f, fBoundryDistY);
			}
			SetOffsets(0, -fSnapAmount, true);
		}
	}
}
