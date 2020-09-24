using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChessMove
{
    public ChessPiece chessPiece;
    public Pos2D targetPosition;
    public MoveType type;

    public enum MoveType
    { CAPTURE, MOVE, SPECIAL }

    public ChessMove(ChessPiece chessPiece, Pos2D targetPosition, MoveType type)
    {
        this.chessPiece = chessPiece;
        this.targetPosition = targetPosition;
        this.type = type;


    }
    public override string ToString()
    {
        return String.Format("[{4}: {0} ({1}) from {2} to {3}]",
        this.chessPiece.Type.ToString(),
        this.chessPiece.agent.name,
        this.chessPiece.pos.ToString(),
        this.targetPosition.ToString(),
        this.type.ToString()
        );
    }

}