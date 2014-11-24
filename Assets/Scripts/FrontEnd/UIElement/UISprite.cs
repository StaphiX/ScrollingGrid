using UnityEngine;
using System.Collections;

public class UISprite : FSprite 
{
    UIElement m_tParent;
    Rect m_tParentOffsetRect;
    Rect m_tPixelOffsetRect;
    Rect m_tExtraOffsetRect;

    public UISprite(string elementName) : base(elementName)
    {

    }

    public UISprite (FAtlasElement element) : base(element)
    {

    }

    public virtual void SetParent(UIElement tElement, float fXOffset, float fYOffset, float fWOffset, float fHOffset)
    {
        m_tParent = tElement;

        SetParentOffset(fXOffset, fYOffset, fWOffset, fHOffset);
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

    public virtual void SetExtraOffset(float fX, float fY, float fW, float fH)
    {
        m_tExtraOffsetRect.x = fX;
        m_tExtraOffsetRect.y = fY;
        m_tExtraOffsetRect.width = fW;
        m_tExtraOffsetRect.height = fH;
        CalculateRect();
    }

    public virtual void CalculateRect()
    {
        Rect tRect = new Rect(0,0,0,0);
        if (m_tParent != null) //Offset from the center of the parent
        {
            tRect.x = (m_tParent.GetRect().x - m_tParent.GetRect().width / 2) + m_tParent.GetRect().width * m_tParentOffsetRect.x;
            tRect.y = (m_tParent.GetRect().y - m_tParent.GetRect().height / 2) + m_tParent.GetRect().height * m_tParentOffsetRect.y;
            tRect.width = m_tParent.GetRect().width * m_tParentOffsetRect.width;
            tRect.height = m_tParent.GetRect().height * m_tParentOffsetRect.height;
        }
        tRect.x += m_tPixelOffsetRect.x;
        tRect.y += m_tPixelOffsetRect.y;
        tRect.width += m_tPixelOffsetRect.width;
        tRect.height += m_tPixelOffsetRect.height;

        tRect.x += m_tExtraOffsetRect.x;
        tRect.y += m_tExtraOffsetRect.y;
        tRect.width += m_tExtraOffsetRect.width;
        tRect.height += m_tExtraOffsetRect.height;

        SetDimensions(tRect.x, tRect.y, tRect.width, tRect.height);
    }
	
}
