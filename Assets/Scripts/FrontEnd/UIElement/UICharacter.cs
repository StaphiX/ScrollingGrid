using UnityEngine;
using System.Collections;

public class UICharacter : UIElement {

    UISprite m_tSprite = null;

    public void SetSprite(string sSprite)
    {
        if (m_tSprite == null)
        {
            m_tSprite = new UISprite(sSprite);
            AddSprite(m_tSprite, 0.5f, 0.5f, 1.0f, 1.0f);
        }
    }
}
