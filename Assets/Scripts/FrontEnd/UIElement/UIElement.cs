using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIElement {

	protected Rect m_tRect;
	UIElement m_tParent;
	FStage m_tStage;
	FShader m_tShader;
	List<UIElement> m_tChildren;

	Rect m_tParentOffsetRect;
	Rect m_tPixelOffsetRect;
	Rect m_tExtraOffsetRect;

	public List<UISprite> m_tSprites;
    public List<UILabel> m_tLabels;

	public virtual UIElement GetParent()
	{
		return m_tParent;
	}

	public virtual void SetParent(UIElement tElement, float fXOffset, float fYOffset, float fWOffset, float fHOffset)
	{
		m_tParent = tElement;
		if(m_tParent != null)
			SetStage(m_tParent.GetStage());

		SetParentOffset(fXOffset, fYOffset, fWOffset, fHOffset);
	}

	public virtual void AddChild(UIElement tElement, float fXOffset, float fYOffset, float fWOffset, float fHOffset)
	{
		if(m_tChildren == null)
			m_tChildren = new List<UIElement>();
		m_tChildren.Add(tElement);
		tElement.SetParent(this, fXOffset, fYOffset, fWOffset, fHOffset);
	}

    public virtual void RemoveChild(UIElement tElement)
    {
        if (tElement != null)
        {
            if (m_tChildren == null)
                return;
            m_tChildren.Remove(tElement);
        }
    }

	public virtual Rect GetRect()
	{
		return m_tRect;
	}

	public virtual Rect GetRect(bool bWithExtra)
	{
		if (!bWithExtra)
			return new Rect (m_tRect.x - m_tExtraOffsetRect.x, 
	        m_tRect.y - m_tExtraOffsetRect.y, 
	        m_tRect.width - m_tExtraOffsetRect.width,
	        m_tRect.height - m_tExtraOffsetRect.height);
		else
			return GetRect ();
	}

	public Rect GetExtraOffset()
	{
		return m_tExtraOffsetRect;
	}

	public void SetExtraOffsetX(float fX, bool bAdd)
	{
		if (bAdd)
			SetExtraOffset (m_tExtraOffsetRect.x + fX, m_tExtraOffsetRect.y,
			                m_tExtraOffsetRect.width, m_tExtraOffsetRect.height);
		else
			SetExtraOffset (fX, m_tExtraOffsetRect.y,
			                m_tExtraOffsetRect.width, m_tExtraOffsetRect.height);
	}

	public void SetExtraOffsetY(float fY, bool bAdd)
	{
		if (bAdd)
			SetExtraOffset (m_tExtraOffsetRect.x, m_tExtraOffsetRect.y + fY,
			                m_tExtraOffsetRect.width, m_tExtraOffsetRect.height);
		else
			SetExtraOffset (m_tExtraOffsetRect.x, fY,
			                m_tExtraOffsetRect.width, m_tExtraOffsetRect.height);
	}

	public void SetExtraOffsetWidth(float fW, bool bAdd)
	{
		if (bAdd)
			SetExtraOffset (m_tExtraOffsetRect.x, m_tExtraOffsetRect.y,
               m_tExtraOffsetRect.width + fW, m_tExtraOffsetRect.height);
		else
			SetExtraOffset (m_tExtraOffsetRect.x, m_tExtraOffsetRect.y,
			   fW, m_tExtraOffsetRect.height);
	}

	public void SetExtraOffsetHeight(float fH, bool bAdd)
	{
		if (bAdd)
			SetExtraOffset (m_tExtraOffsetRect.x, m_tExtraOffsetRect.y,
			                m_tExtraOffsetRect.width, m_tExtraOffsetRect.height + fH);
		else
			SetExtraOffset (m_tExtraOffsetRect.x, m_tExtraOffsetRect.y,
			                m_tExtraOffsetRect.width, fH);
	}

	public virtual Vector2 GetPosition()
	{
		return new Vector2(m_tRect.x, m_tRect.y);
	}

	public virtual float GetWidth()
	{
		return m_tRect.width;
	}

	public virtual float GetHeight()
	{
		return m_tRect.height;
	}

	public virtual void SetParentOffset(float fX, float fY, float fW, float fH)
	{
		m_tParentOffsetRect.x = fX;
		m_tParentOffsetRect.y = fY;
		m_tParentOffsetRect.width = fW;
		m_tParentOffsetRect.height = fH;
		CalculateRect();
	}

	public virtual void SetPixelOffset(float fX, float fY, float fW, float fH)
	{
		m_tPixelOffsetRect.x = fX;
		m_tPixelOffsetRect.y = fY;
		m_tPixelOffsetRect.width = fW;
		m_tPixelOffsetRect.height = fH;
		CalculateRect();
	}

    public virtual Rect GetPixelOffset()
    {
        return m_tPixelOffsetRect;
    }

	public virtual void SetExtraOffset(float fX, float fY, float fW, float fH)
	{
		m_tExtraOffsetRect.x = fX;
		m_tExtraOffsetRect.y = fY;
		m_tExtraOffsetRect.width = fW;
		m_tExtraOffsetRect.height = fH;
		CalculateRect();
	}

	public virtual FStage GetStage()
	{
		return m_tStage;
	}

	public virtual void SetStage(FStage tStage)
	{
		if(m_tSprites != null)
		{
			foreach(FNode tNode in m_tSprites)
			{
				tNode.RemoveFromContainer();
				tStage.AddChild(tNode);
			}
		}
		m_tStage = tStage;

		if(m_tChildren != null)
		{
			foreach(UIElement tChild in m_tChildren)
			{
				tChild.SetStage(tStage);
			}
		}
	}

	public virtual void CalculateRect()
	{
		m_tRect.Set(0, 0, 0, 0);
		if(m_tParent != null) //Offset from the center of the parent
		{
			m_tRect.x = (m_tParent.GetRect().x - m_tParent.GetRect().width/2) + m_tParent.GetRect().width * m_tParentOffsetRect.x;
			m_tRect.y = (m_tParent.GetRect().y - m_tParent.GetRect().height/2) + m_tParent.GetRect().height * m_tParentOffsetRect.y;
			m_tRect.width = m_tParent.GetRect().width * m_tParentOffsetRect.width;
			m_tRect.height = m_tParent.GetRect().height * m_tParentOffsetRect.height;
		}
		m_tRect.x += m_tPixelOffsetRect.x;
		m_tRect.y += m_tPixelOffsetRect.y;
		m_tRect.width += m_tPixelOffsetRect.width;
		m_tRect.height += m_tPixelOffsetRect.height;

		m_tRect.x += m_tExtraOffsetRect.x;
		m_tRect.y += m_tExtraOffsetRect.y;
		m_tRect.width += m_tExtraOffsetRect.width;
		m_tRect.height += m_tExtraOffsetRect.height;

		Resize();
		Reposition();
	}

	public virtual void Resize()
	{
		if(m_tChildren != null)
		{
			foreach(UIElement tChild in m_tChildren)
			{
				tChild.CalculateRect();
			}
		}

        if (m_tSprites != null)
        {
            foreach (UISprite tSprite in m_tSprites)
            {
                tSprite.CalculateRect();
            }
        }

        if (m_tLabels != null)
        {
            foreach (UILabel tLabel in m_tLabels)
            {
                tLabel.CalculateRect();
            }
        }
	}

	public virtual void Reposition()
	{

	}

	public virtual void Init()
	{
		
	}

	public virtual void GUIDisplay()
	{

	}

	public virtual void UpdateChildren()
	{
		if(m_tChildren != null)
		{
			int iElementCount = m_tChildren.Count;
			if(iElementCount > 0)
			{
				for(int iElement = 0; iElement < iElementCount; ++iElement)
				{
					m_tChildren[iElement].Update();
					m_tChildren[iElement].UpdateChildren();
				}
			}
		}
	}

	public virtual void Update()
	{

	}

    public virtual void AddSprite(UISprite tSprite)
    {
        AddSprite(tSprite, 0.5f, 0.5f, 1.0f, 1.0f);
    }

    public virtual void AddSprite(UISprite tSprite, float fXOffset, float fYOffset, float fWOffset, float fHOffset)
	{
		if(tSprite != null)
		{
			if(m_tSprites == null)
				m_tSprites = new List<UISprite>();
			m_tSprites.Add(tSprite);
			if(m_tShader != null)
				tSprite.shader = m_tShader;

            tSprite.SetParent(this, fXOffset, fYOffset, fWOffset, fHOffset);

			if(m_tStage != null)
				m_tStage.AddChild(tSprite);
			else
				Futile.stage.AddChild(tSprite);
		}
	}

	public virtual void SetSpriteColours(Color tColour)
	{
		if (m_tSprites == null)
			return;

		foreach (FSprite tSprite in m_tSprites) 
		{
			tSprite.color = tColour;
		}
	}

	public virtual void AddLabel(UILabel tLabel)
	{
		if(tLabel != null)
		{
            if (m_tLabels == null)
                m_tLabels = new List<UILabel>();
            m_tLabels.Add(tLabel);
			ScreenStack.tTextStage.AddChild(tLabel);
		}
	}

	public virtual void RemoveSprite(FSprite tSprite)
	{
		if(tSprite != null)
		{
			if(m_tSprites == null)
				return;
			if(m_tStage != null)
				m_tStage.RemoveChild(tSprite);
			else
				Futile.stage.RemoveChild(tSprite);
		}
	}

	public FShader GetShader( ) {return m_tShader;}

	public void SetShader(FShader fShader, bool bSetChildren)
	{
		m_tShader = fShader;
		if (m_tSprites != null) 
		{
			foreach(FSprite tSprite in m_tSprites)
			{
				tSprite.shader = fShader;
			}
		}
		if(bSetChildren) 
		{
			SetChildShader(fShader, bSetChildren);
		}
	}
	public void SetChildShader(FShader fShader, bool bSetChildren)
	{
		if (m_tChildren != null) 
		{
			foreach(UIElement tElement in m_tChildren)
			{
				tElement.SetShader(fShader, bSetChildren);
			}
		}
	}
}
