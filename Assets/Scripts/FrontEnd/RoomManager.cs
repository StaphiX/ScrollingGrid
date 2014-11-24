using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager 
{
    List<List<UIGridRoom>> m_tRooms = new List<List<UIGridRoom>>();

    public void CreateRooms(UIScreen tScreen, int iDefaultRoomX, int iDefaultRoomY)
    {
        //Add 5 Rows
        for (int iCol = 0; iCol < 5; ++iCol)
        {
            m_tRooms.Add(new List<UIGridRoom>());
            //Add 5 Rooms to Row
            for (int iRow = 0; iRow < 5; ++iRow)
            {
                UIGridRoom tRoom = new UIGridRoom();

                tScreen.AddChild(tRoom, 0.5f, 0.5f, 0, 0);
                m_tRooms[iCol].Add(tRoom);
                SetupRoom(iRow, iCol);
            }
        }
    }

    public void SetupRoom(int iRow, int iCol)
    {
        float fXOffset = 0;
        float fYOffset = 0;

        if (iCol > 0)
            fXOffset = m_tRooms[iCol - 1][iRow].GetRect().x + m_tRooms[iCol - 1][iRow].GetRect().width;
        if (iRow > 0)
            fYOffset = m_tRooms[iCol][iRow - 1].GetRect().y + m_tRooms[iCol][iRow - 1].GetRect().height;

        m_tRooms[iCol][iRow].SetPixelOffset(fXOffset, fYOffset, 0, 0);
        m_tRooms[iCol][iRow].SetupRoom(7, 7, (int)(Screen.width * 0.9f / 7), (int)(Screen.width * 0.9f / 7)); 
    }

    public void OffsetRooms(int iCenterRoomX, int iCenterRoomY)
    {
        int iXCount = m_tRooms.Count;
        if (iXCount < 0 || iCenterRoomX < 0 || iCenterRoomX >= iXCount)
            return ;

        Rect tRoomPos = m_tRooms[iCenterRoomX][iCenterRoomY].GetRect();

        for(int iRoomX = 0; iRoomX < iXCount; ++iRoomX)
        {
            int iYCount = m_tRooms[iRoomX].Count;
            if (iYCount < 0 || iCenterRoomY < 0 || iCenterRoomY >= iYCount)
                continue;

            for (int iRoomY = 0; iRoomY < iYCount; ++iRoomY)
            {
                Rect tPixelOffset = m_tRooms[iRoomX][iRoomY].GetPixelOffset();
                m_tRooms[iRoomX][iRoomY].SetTransition(new Vector2(tPixelOffset.x, tPixelOffset.y), 1.0f);

                tPixelOffset.x -= tRoomPos.x;
                tPixelOffset.y -= tRoomPos.y;

                m_tRooms[iRoomX][iRoomY].SetPixelOffset(tPixelOffset.x, tPixelOffset.y, tPixelOffset.width, tPixelOffset.height);
            }
        }
    }


    public UIGridRoom GetRoomNoRef(int iX,int iY)
    {
        int iLocalX = iX;
        int iLocalY = iY;
        return GetRoom(ref iLocalX, ref iLocalY);
    }

    public UIGridRoom GetRoom(ref int iX,ref int iY)
    {
        int iXCount = m_tRooms.Count;
        iX = Mathf.Clamp(iX, 0, iXCount-1);

        if (iXCount < 0)
            return null;

        int iYCount = m_tRooms[iX].Count;
        iY = Mathf.Clamp(iY, 0, iYCount-1);

        if (iYCount < 0)
            return null;

        return m_tRooms[iX][iY];
    }

}
