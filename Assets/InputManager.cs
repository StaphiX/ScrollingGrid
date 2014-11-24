using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class InputManager
{
	const bool bUseFutileDimensions = true;
	static List<InputTouch> m_tPressedTouch = new List<InputTouch>();
	static List<InputTouch> m_tReleasedTouch = new List<InputTouch>();
	public static float m_fTouchTolerance = 0;
	//int m_iFramesPressed = 0;

	public static void Init () 
	{
		
	}

	public static void Update () 
	{
		ClearPressTouches();
		m_tReleasedTouch.Clear(); //Release touches only exist for a frame
		if(Input.GetMouseButtonUp(0))
		{
			float fX = 0;
			float fY = 0;
			ConvertMousePosition(Input.mousePosition, out fX, out fY);
			AddRelease(new Vector2(fX, fY), 0);
		}

		if(Input.GetMouseButtonDown(0))
		{
			float fX = 0;
			float fY = 0;
			ConvertMousePosition(Input.mousePosition, out fX, out fY);
			AddPress(new Vector2(fX, fY), 0);
		}

		//Setup held touches
		if(m_tPressedTouch.Count > 0 && Input.GetMouseButton(0))
		{
			for(int iPressTouch = 0; iPressTouch < m_tPressedTouch.Count; ++iPressTouch)
			{
				if(m_tPressedTouch[iPressTouch].iTouchIndex == 0)
				{
					float fX = 0;
					float fY = 0;
					ConvertMousePosition(Input.mousePosition, out fX, out fY);
					m_tPressedTouch[iPressTouch].UpdatePosition(new Vector2(fX, fY));
				}
			}
		}
	}

	static void ClearPressTouches() //Remove press touches that have been released
	{
		foreach(InputTouch tTouch in m_tReleasedTouch)
		{
			for(int iPressTouch = 0; iPressTouch < m_tPressedTouch.Count; ++iPressTouch)
			{
				if(m_tPressedTouch[iPressTouch].iTouchIndex == tTouch.iTouchIndex)
				{
					//Chache the prevoius touch position of this index
					tTouch.vPrevPosition = m_tPressedTouch[iPressTouch].vPosition;

					m_tPressedTouch.RemoveAt(iPressTouch);
					iPressTouch--;
				}
			}
		}
	}

	public static void AddRelease(Vector2 vPosition, int iTouch)
	{
		foreach(InputTouch tTouch in m_tReleasedTouch)
		{
			if(tTouch.iTouchIndex == iTouch)
			{
				tTouch.vPosition = vPosition;
				return;
			}
		}
		m_tReleasedTouch.Add(new InputTouch(vPosition, iTouch));
	}

	public static void AddPress(Vector2 vPosition, int iTouch)
	{
		foreach(InputTouch tTouch in m_tPressedTouch)
		{
			if(tTouch.iTouchIndex == iTouch)
			{
				tTouch.vPosition = vPosition;
				return;
			}
		}
		m_tPressedTouch.Add(new InputTouch(vPosition, iTouch));
	}

	public static void AddHeld(Vector2 vPosition, int iTouch)
	{
		foreach(InputTouch tTouch in m_tPressedTouch)
		{
			if(tTouch.iTouchIndex == iTouch)
			{
				tTouch.vPosition = vPosition;
				return;
			}
		}
		m_tPressedTouch.Add(new InputTouch(vPosition, iTouch));
	}


	public static bool IsPressInRect(Rect tRect)
	{
		return IsPressInRect(tRect.x, tRect.y, tRect.width, tRect.height);
	}
	
	public static bool IsPressInRect(float fX, float fY, float fW, float fH)
	{
		if (bUseFutileDimensions) 
		{
			ConvertFutilePosition (new Vector2 (fX, fY), out fX, out fY, fW, fH);
			ConvertFutileDimensions (new Vector2 (fW, fH), out fW, out fH);
		}
		foreach(InputTouch tTouch in m_tPressedTouch)
		{
			if(IsTouchInRect(tTouch, fX, fY, fW, fH))
				return true;
		}
		return false;
	}

	public static bool IsReleaseInRect(Rect tRect)
	{
		return IsReleaseInRect(tRect.x, tRect.y, tRect.width, tRect.height);
	}

	public static bool IsReleaseInRect(float fX, float fY, float fW, float fH)
	{
		return (GetReleaseInRect (fX, fY, fW, fH) != null);
	}

	public static InputTouch GetReleaseInRect(Rect tRect)
	{
		return GetReleaseInRect(tRect.x, tRect.y, tRect.width, tRect.height);
	}

	public static InputTouch GetReleaseInRect(float fX, float fY, float fW, float fH)
	{
		if (bUseFutileDimensions) 
		{
			ConvertFutilePosition (new Vector2 (fX, fY), out fX, out fY, fW, fH);
			ConvertFutileDimensions (new Vector2 (fW, fH), out fW, out fH);
		}

		foreach(InputTouch tTouch in m_tReleasedTouch)
		{
			if(IsTouchInRect(tTouch, fX, fY, fW, fH))
				return tTouch;
		}
		return null;

	}

	public static InputTouch GetHeldInRect(Rect tRect)
	{
		return GetHeldInRect(tRect.x, tRect.y, tRect.width, tRect.height);
	}

	public static InputTouch GetHeldInRect(float fX, float fY, float fW, float fH)
	{
		if (bUseFutileDimensions) 
		{
			ConvertFutilePosition (new Vector2 (fX, fY), out fX, out fY, fW, fH);
			ConvertFutileDimensions (new Vector2 (fW, fH), out fW, out fH);
		}
		foreach (InputTouch tTouch in m_tPressedTouch) 
		{
			if(IsTouchInRect(tTouch, fX, fY, fW, fH))
			{
				return tTouch;
			}
		}
		return null;
	}

	static bool IsTouchInRect(InputTouch tTouch, float fX, float fY, float fW, float fH)
	{
		if(tTouch.vTouchPosition.x >= 0 && tTouch.vTouchPosition.y >= 0)
		{
			if(tTouch.vTouchPosition.x > fX - m_fTouchTolerance)
			{
				if(tTouch.vTouchPosition.y > fY - m_fTouchTolerance)
				{
					if(tTouch.vTouchPosition.x < fX + fW + m_fTouchTolerance)
					{
						if(tTouch.vTouchPosition.y < fY + fH + m_fTouchTolerance)
						{
							return true;
						}	
					}	
				}
			}
		}
		return false;
	}

	public static bool IsKeyReleased(KeyCode eKey)
	{
		return Input.GetKeyUp(eKey);
	}

	public static bool IsKeyDown(KeyCode eKey)
	{
		return Input.GetKey(eKey);
	}


	public static void ConvertFutilePosition(Vector2 vPos, out float fX,out float fY)
	{
		ConvertFutilePosition(vPos, out fX, out fY, 0, 0);
	}

	public static void ConvertFutilePosition(Vector2 vPos, out float fX,out float fY, float fW, float fH)
	{
		//Futile uses world position offset from center
		//Offset From top edge instead
		vPos.y *= -1;
		//Offset these values by half to compensate for centering
		vPos.x -= fW/2;
		vPos.y -= fH/2;
		vPos = Futile.stage.LocalToScreen(vPos);
		fX = vPos.x;
		fY = vPos.y;
	}

	public static void ConvertFutileDimensions(Vector2 vDim, out float fX,out float fY)
	{
		fX = vDim.x;
		fY = vDim.y;
	}

	public static void ConvertMousePosition(Vector2 vPos, out float fX,out float fY)
	{
		//Mouse uses bottom left as 0,0
		//Unity uses top left as 0,0
		fX = vPos.x;
		fY = vPos.y * -1 + Screen.height;
	}
}

public class InputTouch
{
	public Vector2 vPrevPosition;
	public Vector2 vPosition;
	public Vector2 vTouchPosition;
	public int iTouchIndex = 0;
	public int iNumFrames = 0;

	public InputTouch(Vector2 _vPosition, int _iTouch)
	{
		vTouchPosition = _vPosition;
		vPosition = vTouchPosition;
		iTouchIndex = _iTouch;
	}

	public void UpdatePosition(Vector2 _vPosition)
	{
		iNumFrames++;
		vPrevPosition = vPosition;
		vPosition = _vPosition;
	}

	public Vector2 GetTouchOffset()
	{
		if(iNumFrames > 0)
		{
			return new Vector2(vPosition.x - vPrevPosition.x, vPosition.y - vPrevPosition.y);
		}
		else return Vector2.zero;
	}

	public static InputTouch zero
	{
		get
		{
			return new InputTouch(-Vector2.one, 0);
		}
	}

}

