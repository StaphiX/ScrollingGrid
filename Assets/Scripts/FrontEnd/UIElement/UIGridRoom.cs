using UnityEngine;
using System.Collections;

public class UIGridRoom : UIElement 
{
    UIGridTile[,] tTile;
    UIGridTile[] tWallLeft;
    UIGridTile[] tWallRight;
    UIGridTile[] tWallTop;
    UIGridTile[] tWallBottom;

    UITransition m_tTransition = new UITransition();
    Vector2 m_vPreviousPosition;

    public override void Init()
    {

    }

    public void SetupRoom(int iXCount, int iYCount, int iTileW, int iTileH)
    {
        if (tTile == null)
        {
            tTile = new UIGridTile[iXCount, iYCount];
        }
        else if (tTile.GetLength(0) != iXCount || tTile.GetLength(1) != iYCount)
        {
            
        }

        Color tColor = ColourHelper.RANDOM();
        int iW = iTileW;
        int iH = iW;
        int iTotalW = iW * iXCount;
        int iTotalH = iH * iYCount;

        int iStartXPos = -iTotalW/2;
        int iStartYPos = -iTotalH/2;

        float fEndXPos = 0;
        float fEndYPos = 0;

        for (int iX = 0; iX < iXCount; ++iX)
        {
            for (int iY = 0; iY < iYCount; ++iY)
            {
                if (tTile[iX, iY] == null)
                {
                    tTile[iX, iY] = new UIGridTile();
                    AddChild(tTile[iX, iY], 0.5f, 0.5f, 0, 0);
                }
                int iXPos = iStartXPos + iX * iW + iW / 2;
                int iYPos = iStartYPos + iY * iH + iH / 2;
                tTile[iX, iY].SetPixelOffset(iXPos, iYPos, iW, iH);
                tTile[iX, iY].SetBackground(tColor, 1);

                //Set the width and height of the rooms dynamically
                if (iXPos + iW / 2 > fEndXPos)
                    fEndXPos = iXPos + iW / 2;

                if (iYPos + iH / 2 > fEndYPos)
                    fEndYPos = iYPos + iH / 2;
            }
        }
        SetPixelOffset(GetPixelOffset().x, GetPixelOffset().y, fEndXPos - iStartXPos, fEndYPos - iStartYPos);
       // SetupWalls();
    }

    private void SetupWalls()
    {
        Color tColour = ColourHelper.RANDOM();
        SetupWallLeftRight(tColour);
        SetupWallTopBottom(tColour);
    }

    private void SetupWallLeftRight(Color tColour)
    {
        if (tTile == null)
            return;

        int iTileCountX = tTile.GetLength(0);
        int iTileCountY = tTile.GetLength(1);

        if (tWallLeft == null)
        {
            tWallLeft = new UIGridTile[iTileCountY];
        }
        if (tWallRight == null)
        {
            tWallRight = new UIGridTile[iTileCountY];
        }

        for (int iY = 0; iY < iTileCountY; ++iY)
        {
            if (tWallLeft[iY] == null)
            {
                tWallLeft[iY] = new UIGridTile();
                AddChild(tWallLeft[iY], 0, 0, 0, 0);
            }

            if (tWallRight[iY] == null)
            {
                tWallRight[iY] = new UIGridTile();
                AddChild(tWallRight[iY], 0, 0, 0, 0);
            }

            int iW = (int)(tTile[0, 0].GetRect().width * 0.5f);
            int iH = (int)tTile[0, iY].GetRect().height;
            int iXPos = (int)(tTile[0, 0].GetRect().x - (iW + tTile[0, 0].GetRect().width)/2);
            int iYPos = (int)tTile[0, iY].GetRect().y;

            Rect tLeftRect = tWallLeft[iY].GetRect();
            tWallLeft[iY].SetPixelOffset(iXPos - tLeftRect.x, iYPos - tLeftRect.y, iW, iH);
            Rect tRightRect = tWallRight[iY].GetRect();
            iXPos = (int)(tTile[iTileCountX - 1, 0].GetRect().x + (iW + tTile[iTileCountX - 1, 0].GetRect().width) / 2);
            tWallRight[iY].SetPixelOffset(iXPos - tRightRect.x, iYPos - tRightRect.y, iW, iH);

            tWallLeft[iY].SetBackground(tColour, 1);
            tWallRight[iY].SetBackground(tColour, 1);
        }
    }

    private void SetupWallTopBottom(Color tColour)
    {
        if (tTile == null)
            return;

        int iTileCountX = tTile.GetLength(0);
        int iTileCountY = tTile.GetLength(1);

        if (tWallTop == null)
        {
            tWallTop = new UIGridTile[iTileCountX];
        }
        if (tWallBottom == null)
        {
            tWallBottom = new UIGridTile[iTileCountX];
        }

        for (int iX = 0; iX < iTileCountX; ++iX)
        {
            if(tWallTop[iX] == null)
            {
                tWallTop[iX] = new UIGridTile();
                AddChild(tWallTop[iX], 0, 0, 0, 0);
            }

            if (tWallBottom[iX] == null)
            {
                tWallBottom[iX] = new UIGridTile();
                AddChild(tWallBottom[iX], 0, 0, 0, 0);
            }

            int iW = (int)tTile[iX, 0].GetRect().width;
            int iH = (int)(tTile[0, 0].GetRect().height*0.5f);
            int iXPos = (int)tTile[iX, 0].GetRect().x;
            int iYPos = (int)(tTile[0, iTileCountY-1].GetRect().y + (iH + tTile[0, 0].GetRect().height)/2);

            Rect tTopRect = tWallTop[iX].GetRect();
            tWallTop[iX].SetPixelOffset(iXPos - tTopRect.x, iYPos - tTopRect.y, iW, iH);
            Rect tBottomRect = tWallBottom[iX].GetRect();
            iYPos = (int)(tTile[0, 0].GetRect().y - (iH + tTile[0, 0].GetRect().height)/2);
            tWallBottom[iX].SetPixelOffset(iXPos - tBottomRect.x, iYPos - tBottomRect.y, iW, iH);

            tWallTop[iX].SetBackground(tColour, 1);
            tWallBottom[iX].SetBackground(tColour, 1);
        }
    }

    public UIGridTile GetTile(int iX, int iY)
    {
        if (tTile == null)
            return null;

        int iXCount = tTile.GetLength(0);

        if (iXCount < 0 || iX < 0 || iX >= iXCount)
            return null;

        int iYCount = tTile.GetLength(1);

        if (iYCount < 0 || iY < 0 || iY >= iYCount)
            return null;

        return tTile[iX,iY];
    }

    public UIGridTile GetTileFromDirection(EDirection eDir, ref int iXIndex, ref int iYIndex)
    {
        Vector2 vDir = Direction.GetVectorFromDirection(eDir);
        int iXCount = tTile.GetLength(0);

        if (iXCount < 0)
            return null;

        int iYCount = tTile.GetLength(1);

        if (iYCount < 0)
            return null;

        iXIndex = Mathf.Clamp(iXIndex, 0, iXCount - 1);
        iYIndex = Mathf.Clamp(iYIndex, 0, iYCount - 1);
        if(vDir.x == 1)
        {
            iXIndex = 0;
        }
        else if (vDir.x == -1)
        {
            iXIndex = iXCount - 1;
        }
        if (vDir.y == 1)
        {
            iYIndex = 0;
        }
        else if (vDir.y == -1)
        {
            iYIndex = iYCount - 1;
        }

        return tTile[iXIndex, iYIndex];
    }

    public void SetTransition(Vector2 vPreviousPos, float fDuration)
    {
        m_vPreviousPosition = vPreviousPos;
        m_tTransition.Init(ETransition.IN, MoveTransitionSetup, MoveTransition, null, fDuration);
    }

    public override void Update()
    {
        if(m_tTransition != null && m_tTransition.GetTransitionMode() != ETransition.NONE)
        {
            m_tTransition.UpdateTransition();
            return;
        }
    }

    public ETransition GetTransitionMode()
    {
        return m_tTransition.GetTransitionMode();
    }

    private bool MoveTransitionSetup(float fTransitionTime, float fTransitionDuration)
    {
        Rect tRoomRect = GetRect(false);

        SetExtraOffsetX(m_vPreviousPosition.x - tRoomRect.x, false);
        SetExtraOffsetY(m_vPreviousPosition.y - tRoomRect.y, false);

        return false;
    }

    private bool MoveTransition(float fTransitionTime, float fTransitionDuration)
    {
        Rect tRoomRect = GetRect(false);
        float fPosX = (float)Easing.Ease(fTransitionTime, m_vPreviousPosition.x - tRoomRect.x, -(m_vPreviousPosition.x - tRoomRect.x), fTransitionDuration, Easing.EaseType.CubicEaseInOut);
        float fPosY = (float)Easing.Ease(fTransitionTime, m_vPreviousPosition.y - tRoomRect.y, -(m_vPreviousPosition.y - tRoomRect.y), fTransitionDuration, Easing.EaseType.CubicEaseInOut);

        SetExtraOffsetX(fPosX, false);
        SetExtraOffsetY(fPosY, false);

        return false;
    }

    public override void Resize()
    {
        base.Resize();
    }

}
