using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    public bool isWhite;
    public bool isCrossJumpUse;
    public bool isLineJumpUse;

    
    public void ToggleLineJump(bool value)
    {
        isLineJumpUse = value;
    }
    public void ToggleCrossJump(bool value)
    {
        isCrossJumpUse = value;
    }

    private void Update()
    {
        ToggleLineJump(isLineJumpUse);
        ToggleCrossJump(isCrossJumpUse);
    }
    public bool ValidMove(Piece[,] board, int x1, int y1, int x2, int y2)
    {

        if (board[x2, y2] != null)
            return false;


        int deltaMoveX = Mathf.Abs(x1 - x2);
        int deltaMoveY = y1 - y2;
        if (StandartRules(deltaMoveX, deltaMoveY) ||
        (CrossJump(board, deltaMoveX, deltaMoveY, x1, x2, y1, y2, isCrossJumpUse) ||
        LineJump(board, deltaMoveX, deltaMoveY, x1, x2, y1, y2, isLineJumpUse)))
        {
            return true;
        }
        else return false;
  
    }
    private bool StandartRules(int deltaMoveX, int deltaMoveY)
    {
        if (isWhite ? ((deltaMoveX == 1 && deltaMoveY == 0) || (deltaMoveX == 0 && deltaMoveY == -1)) : ((deltaMoveX == 1 && deltaMoveY == 0) || (deltaMoveX == 0 && deltaMoveY == 1)))
        {

            return true;

        }
        else return false;
    }
    private bool CrossJump(Piece[,] board,int deltaMoveX, int deltaMoveY, int x1, int x2, int y1, int y2, bool isCrossJumpUse )
    {
        if (isCrossJumpUse)
        {
            if (isWhite ? (deltaMoveY == -2 && deltaMoveX == 2) : (deltaMoveY == 2 && deltaMoveX == 2))
            {

                Piece p = board[(x1 + x2) / 2, (y1 + y2) / 2];
                if (p != null && p.isWhite != isWhite)
                    return true;
                else return false;
            }
            else return false;
        }
        else return false;

    }
    private bool LineJump(Piece[,] board, int deltaMoveX, int deltaMoveY, int x1, int x2, int y1, int y2,bool isLineJumpUse)
    {
        if (isLineJumpUse)
        { 
        if (isWhite ? ((deltaMoveY == -2 && deltaMoveX == 0) || (deltaMoveX == 2 && deltaMoveY == 0)) : ((deltaMoveY == 2 && deltaMoveX == 0) || (deltaMoveX == 2 && deltaMoveY == 0)))
        {

            Piece p = board[(x1 + x2) / 2, (y1 + y2) / 2];
            if (p != null && p.isWhite != isWhite)
                return true;
            else return false;
        }
        else return false;
        }
        else return false;
    }
}
