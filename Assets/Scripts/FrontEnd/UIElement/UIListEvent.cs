using UnityEngine;
using System.Collections;

public class UIListEvent : UIElement 
{
    UIArea tBackground = new UIArea();
    UISprite tThumbnail = null;
    UISprite tDarken = null;
    UITransition tTransition = null;

    public override void Init()
    {
        AddChild(tBackground, 0.5f, 0.5f, 1.0f, 1.0f);
        tBackground.SetBackground(Color.white);
        SetupThumbnail();
    }

    public override void Update()
    {
        if(InputManager.IsReleaseInRect(GetRect()))
            DarkenArea();
    }

    public void SetupThumbnail()
    {
        Rect tRect = GetRect();
        if(tThumbnail == null)
        {
            tThumbnail = new UISprite("blank");
            AddSprite(tThumbnail);
        }
        
        tThumbnail.color = Color.cyan;
        tThumbnail.MoveToFront();
    }

    public void SetupDarken()
    {
        Rect tRect = GetRect();
        if (tDarken == null)
        {
            tDarken = new UISprite("blank");
            
            AddSprite(tDarken);
        }
        tDarken.MoveToFront();
    }

    public void DarkenArea()
    {
        if (tTransition == null)
            tTransition = new UITransition(ETransition.IN, null, DoDarken, null, 1.0f);
        else if(tDarken.alpha <= 0.0f)
            tTransition.Init(ETransition.IN, null, DoDarken, null, 1.0f);
        else
            tTransition.Init(ETransition.OUT, null, DoDarken, null, 1.0f);
    }

    private bool DoDarken(float fTransitionTime, float fTransitionDuration)
    {
        if (tDarken == null)
            return true;

        float fAlpha = (float)Easing.Ease(fTransitionTime, 0.0f, 0.8f, fTransitionDuration, Easing.EaseType.QuartEaseIn);
        tDarken.color = new Color(0, 0, 0, fAlpha);

        return false;
    }
}
