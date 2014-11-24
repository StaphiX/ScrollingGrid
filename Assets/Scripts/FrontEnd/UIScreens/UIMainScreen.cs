using UnityEngine;
using System.Collections;

public class UIMainScreen : UIScreen
{
	public UIMainScreen(string sName): base(sName) {}

    RoomManager m_tRoomManager;
    Character m_tCharacter;

    public override void Init()
    {
        m_tRoomManager = new RoomManager();
        m_tRoomManager.CreateRooms(this, 0, 0);

        m_tCharacter = new Character("blank");
        m_tCharacter.SetupPosition(m_tRoomManager, 0, 0, 0, 0);
    }

	public override void Update()
	{
        m_tCharacter.Update();
	}

}
