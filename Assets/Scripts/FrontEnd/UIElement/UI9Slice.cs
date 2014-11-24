using UnityEngine;
using System.Collections;

public class UI9Slice : UIElement {

    UISprite[] m_tSprite = new UISprite[(int)EDirection.COUNT];
	float m_fCornerSize = 0.0f;

	public override void Init()
	{

	}

	private void GetSpriteValues(string[] sCornerSprites, string[] sSideSprites, string[] sMiddleSprites,
	                             EDirection eDirection, out string sSprite, out float fRotation)
	{
		int iCornerCount = sCornerSprites == null ? 0 : sCornerSprites.Length;
		int iSideCount = sSideSprites == null ? 0 : sSideSprites.Length;
		int iMiddleCount = sMiddleSprites == null ? 0 : sMiddleSprites.Length;

		switch(eDirection)
		{
		case EDirection.topLeft:
			sSprite = sCornerSprites[0];
			fRotation = 0.0f;
			break;
		case EDirection.topMiddle:
			sSprite = sSideSprites[0];
			fRotation = 0.0f;
			break;
		case EDirection.topRight:
			if(iCornerCount < 2)
			{
				sSprite = sCornerSprites[0];
				fRotation = 90.0f;
			}
			else
			{
				sSprite = sCornerSprites[1];
				fRotation = 0.0f;
			}
			break;
		case EDirection.centerLeft:
			if(iSideCount < 2)
			{
				sSprite = sSideSprites[0];
				fRotation = 270.0f;
			}
			else
			{
				sSprite = sSideSprites[1];
				fRotation = 0.0f;
			}
			break;
		case EDirection.centerMiddle:
			if(iMiddleCount < 1)
			{
				sSprite = sSideSprites[0];
				fRotation = 0.0f;
			}
			else
			{
				sSprite = sMiddleSprites[0];
				fRotation = 0.0f;
			}
			break;
		case EDirection.centerRight:
			if(iSideCount < 3)
			{
				sSprite = sSideSprites[0];
				fRotation = 90.0f;
			}
			else
			{
				sSprite = sSideSprites[2];
				fRotation = 0.0f;
			}
			break;
		case EDirection.bottomLeft:
			if(iCornerCount < 3)
			{
				sSprite = sCornerSprites[0];
				fRotation = 270.0f;
			}
			else
			{
				sSprite = sCornerSprites[2];
				fRotation = 0.0f;
			}
			break;
		case EDirection.bottomMiddle:
			if(iSideCount < 4)
			{
				sSprite = sSideSprites[0];
				fRotation = 180.0f;
			}
			else
			{
				sSprite = sSideSprites[3];
				fRotation = 0.0f;
			}
			break;
		case EDirection.bottomRight:
			if(iCornerCount < 4)
			{
				sSprite = sCornerSprites[0];
				fRotation = 180.0f;
			}
			else
			{
				sSprite = sCornerSprites[3];
				fRotation = 0.0f;
			}
			break;
		default:
			sSprite = "blank";
			fRotation = 0.0f;
			break;
		}
	}

	public void SetSprites(string[] sCornerSprites, string[] sSideSprites, string[] sMiddleSprites, int iCornerSize)
	{
		m_fCornerSize = iCornerSize;

		for (int iDirection = 0; iDirection < (int)EDirection.COUNT; ++iDirection)
		{
			string sSpriteName;
			float fRotation;
			GetSpriteValues(sCornerSprites, sSideSprites, sMiddleSprites, 
			                (EDirection)iDirection, out sSpriteName, out fRotation);

			if(m_tSprite[iDirection] == null)
			{
                m_tSprite[iDirection] = new UISprite(sSpriteName);
				m_tSprite[iDirection].rotation = fRotation;
				AddSprite(m_tSprite[iDirection], 0.5f, 0.5f, 0, 0);
			}
		}

	}
	
	public override void Resize()
	{
		for (int iDirection = 0; iDirection < (int)EDirection.COUNT; ++iDirection) 
		{
			if(m_tSprite[iDirection] == null)
				continue;

			EDirection eDirection = (EDirection)iDirection;
			float fX, fY, fW, fH;
			switch(eDirection)
			{
			case EDirection.bottomLeft: 
			case EDirection.bottomRight:
			case EDirection.topLeft: 
			case EDirection.topRight:
				fW = m_fCornerSize;
				fH = m_fCornerSize;
				break;
			case EDirection.topMiddle:
			case EDirection.bottomMiddle:
				fW = GetRect().width - m_fCornerSize*2;
				fH = m_fCornerSize;
				break;
			case EDirection.centerLeft:
			case EDirection.centerRight:
				fW = m_fCornerSize;
				fH = GetRect().height - m_fCornerSize*2;
				break;
			case EDirection.centerMiddle:
				fW = GetRect().width - m_fCornerSize*2;
				fH = GetRect().height - m_fCornerSize*2;
				break;
			default:
				fW = 0; 
				fH = 0;
				break;
			}

			switch(eDirection)
			{
			case EDirection.bottomLeft: 
			case EDirection.topLeft: 
			case EDirection.centerLeft:
				fX = GetRect().x - GetRect().width/2 + fW/2;
				fX = Mathf.Min (fX, GetRect().x-fW/2);
				break;
			case EDirection.topMiddle:
			case EDirection.bottomMiddle:
			case EDirection.centerMiddle:
				fX = GetRect().x;
				break;
			case EDirection.topRight:
			case EDirection.bottomRight:
			case EDirection.centerRight:
				fX = GetRect().x + GetRect().width/2 - fW/2;
				fX = Mathf.Max(fX, GetRect().x+fW/2);
				break;
			default:
				fX = 0; 
				break;
			}

			switch(eDirection)
			{
			case EDirection.bottomLeft: 
			case EDirection.bottomMiddle:
			case EDirection.bottomRight:
				fY = GetRect().y - GetRect().height/2 + fH/2;
				break;
			case EDirection.topLeft: 
			case EDirection.topMiddle:
			case EDirection.topRight:
				fY = GetRect().y + GetRect().height/2 - fH/2;
				break;
			case EDirection.centerLeft:
			case EDirection.centerMiddle:
			case EDirection.centerRight:
				fY = GetRect().y;
				break;
			default:
				fY = 0; 
				break;
			}

			if(fW < 0)
				fW = 0;
			if(fH < 0)
				fH = 0;

			if(eDirection == EDirection.centerLeft || eDirection == EDirection.centerRight
			   && m_tSprite[iDirection].rotation != 0.0f)
			{
				m_tSprite[iDirection].SetDimensions(fX, fY, fH, fW);
			}
			else
				m_tSprite[iDirection].SetDimensions(fX, fY, fW, fH);
		}
		base.Resize ();
	}
}
