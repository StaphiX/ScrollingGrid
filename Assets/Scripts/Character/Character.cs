using UnityEngine;
using System.Collections;

public class Character {

    UICharacter m_tCharacter;
    UITransition m_tTransition;
    RoomManager m_tRoomManager;
    Vector2 m_vPreviousPosition;
    int m_iRoomIndexX = 0;
    int m_iRoomIndexY = 0;
    int m_iGridIndexX = 0;
    int m_iGridIndexY = 0;

    public Character(string sSprite)
    {
        m_tCharacter = new UICharacter();
        m_tCharacter.SetSprite(sSprite);
        m_tTransition = new UITransition();
    }

    public void Update()
    {
        if (m_tTransition != null && m_tTransition.GetTransitionMode() != ETransition.NONE)
        {
            m_tTransition.UpdateTransition();
            return;
        }
        UIGridRoom tRoom = m_tRoomManager.GetRoomNoRef(m_iRoomIndexX, m_iRoomIndexY);
        if (tRoom != null && tRoom.GetTransitionMode() != ETransition.NONE)
            return;

        if (InputManager.IsKeyReleased(KeyCode.UpArrow))
            MovePosition(EDirection.topMiddle);
        if (InputManager.IsKeyReleased(KeyCode.DownArrow))
            MovePosition(EDirection.bottomMiddle);
        if (InputManager.IsKeyReleased(KeyCode.LeftArrow))
            MovePosition(EDirection.centerLeft);
        if (InputManager.IsKeyReleased(KeyCode.RightArrow))
            MovePosition(EDirection.centerRight);
    }

    public void SetupPosition(RoomManager tRoomManager, int iRoomX, int iRoomY, int iTileX, int iTileY)
    {
        m_iGridIndexX = iTileX;
        m_iGridIndexY = iTileY;
        m_iRoomIndexX = iRoomX;
        m_iRoomIndexY = iRoomY;
        m_tRoomManager = tRoomManager;
        UIGridTile tTile = GetRoom().GetTile(iTileX, iTileY);
        tTile.AddChild(m_tCharacter, 0.5f, 0.5f, 0.65f, 0.65f);
        m_vPreviousPosition = new Vector2(m_tCharacter.GetRect().x, m_tCharacter.GetRect().y);
    }

    public UIGridRoom GetRoom()
    {
        UIGridRoom tRoom = m_tRoomManager.GetRoom(ref m_iRoomIndexX,ref m_iRoomIndexY);
        return tRoom;
    }

    public void MovePosition(EDirection eDir)
    {
        Vector2 vDir = Direction.GetVectorFromDirection(eDir);
        int iNewTileX = m_iGridIndexX + (int)vDir.x;
        int iNewTileY = m_iGridIndexY + (int)vDir.y;

        
        UIGridTile tTile = GetRoom().GetTile(iNewTileX, iNewTileY);

        //This tile is not in this room
        if(tTile == null)
        {
            UIGridRoom tPrevRoom = GetRoom();
            m_iRoomIndexX += (int)vDir.x;
            m_iRoomIndexY += (int)vDir.y;

            //Haven't or can't move rooms
            if (tPrevRoom == GetRoom())
                return;

            tTile = GetRoom().GetTileFromDirection(eDir, ref iNewTileX, ref iNewTileY);

            if (tTile == null)
                return;

            //Should be done before we offset rooms
            m_vPreviousPosition = new Vector2(m_tCharacter.GetRect().x, m_tCharacter.GetRect().y);
            m_tRoomManager.OffsetRooms(m_iRoomIndexX, m_iRoomIndexY);
        }
        else
            m_vPreviousPosition = new Vector2(m_tCharacter.GetRect().x, m_tCharacter.GetRect().y);

        m_iGridIndexX = iNewTileX;
        m_iGridIndexY = iNewTileY;

        m_tCharacter.GetParent().RemoveChild(m_tCharacter);
        tTile.AddChild(m_tCharacter, 0.5f, 0.5f, 0.65f, 0.65f);

        m_tTransition.Init(ETransition.IN, MoveTransitionSetup, MoveTransition, null, 0.1f);
    }

    private bool MoveTransitionSetup(float fTransitionTime, float fTransitionDuration)
    {
        Rect tCharacterRect = m_tCharacter.GetRect(false);

        m_tCharacter.SetExtraOffsetX(m_vPreviousPosition.x - tCharacterRect.x, false);
        m_tCharacter.SetExtraOffsetY(m_vPreviousPosition.y - tCharacterRect.y, false);

        return false;
    }

    private bool MoveTransition(float fTransitionTime, float fTransitionDuration)
    {
        Rect tCharacterRect = m_tCharacter.GetRect(false);
        float fPosX = (float)Easing.Ease(fTransitionTime, m_vPreviousPosition.x - tCharacterRect.x, -(m_vPreviousPosition.x - tCharacterRect.x), fTransitionDuration, Easing.EaseType.CubicEaseInOut);
        float fPosY = (float)Easing.Ease(fTransitionTime, m_vPreviousPosition.y - tCharacterRect.y, -(m_vPreviousPosition.y - tCharacterRect.y), fTransitionDuration, Easing.EaseType.CubicEaseInOut);

        m_tCharacter.SetExtraOffsetX(fPosX, false);
        m_tCharacter.SetExtraOffsetY(fPosY, false);

        return false;
    }
}
