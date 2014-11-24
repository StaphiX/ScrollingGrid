using UnityEngine;
using System.Collections;

public enum EDirection
{
    topLeft,
    topMiddle,
    topRight,
    centerLeft,
    centerMiddle,
    centerRight,
    bottomLeft,
    bottomMiddle,
    bottomRight,
    COUNT,
};

public class Direction {

	public static Vector2 GetVectorFromDirection(EDirection eDir)
    {
        int iX = 0;
        int iY = 0;
        switch(eDir)
        {
            case EDirection.topLeft:
            case EDirection.topMiddle:
            case EDirection.topRight:
                iY = 1;
                break;

            case EDirection.bottomLeft:
            case EDirection.bottomMiddle:
            case EDirection.bottomRight:
                iY = -1;
                break;

            default:
            case EDirection.centerLeft:
            case EDirection.centerMiddle:
            case EDirection.centerRight:
                break;
        }

        switch (eDir)
        {
            case EDirection.topLeft:
            case EDirection.bottomLeft:
            case EDirection.centerLeft:
                iX = -1;
                break;

            case EDirection.topRight:
            case EDirection.bottomRight:
            case EDirection.centerRight:
                iX = 1;
                break;

            default:
            case EDirection.topMiddle:
            case EDirection.bottomMiddle:
            case EDirection.centerMiddle:
                break;
        }

        return new Vector2(iX, iY);
    }
}
