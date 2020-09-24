using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;



public class ChessPiece
{

    // properties:
    public ChessAgent agent;
    public List<ChessMoveAbility> moveAbilities = new List<ChessMoveAbility>();
    public List<Tags> tags = new List<Tags>();
    public PieceType Type { get; private set; }
    public int MaxLives { get; private set; }
    public int Lives { get; private set; }
    public Pos2D pos;

    // ENUMS: 
    public enum Tags { ROYAL }
    public enum PieceType { PAWN, KING, QUEEN, KNIGHT, ROOK, BISHOP };

    public ChessPiece(PieceType type_, Pos2D pos_, ChessAgent agent_)
    {
        agent = agent_;
        pos = pos_;
        Type = type_;
        Lives = 1;
        MaxLives = 1;
        switch (type_)
        {
            case PieceType.PAWN:

                moveAbilities.Add(new ChessMoveAbility(1, 0, new ChessMoveAbility.Tags[] { ChessMoveAbility.Tags.FORWARDONLY, ChessMoveAbility.Tags.NOSTRIKE }));
                moveAbilities.Add(new ChessMoveAbility(1, 1, new ChessMoveAbility.Tags[] { ChessMoveAbility.Tags.FORWARDONLY, ChessMoveAbility.Tags.STRIKEONLY }));
                break;
            case PieceType.QUEEN:

                moveAbilities.Add(new ChessMoveAbility(1, 0, new ChessMoveAbility.Tags[] { ChessMoveAbility.Tags.RIDE }));
                moveAbilities.Add(new ChessMoveAbility(1, 1, new ChessMoveAbility.Tags[] { ChessMoveAbility.Tags.RIDE }));
                break;

        }
    }


    public List<ChessMove> GetValidMoves()
    {

        ChessBoard chessBoard = this.agent.ChessBoard;

        List<ChessMove> viableMoves = new List<ChessMove>();
        // get Positions of 1 single step: // for each way in which they can move: 
        foreach (ChessMoveAbility moveAbility in this.moveAbilities)
        {
            List<Pos2D> mirrorSteps = moveAbility.pos.mirrorPositions().ToList();

            // special case for forwardonly: (pawns, striking and moving)  y must be at least 1
            if (moveAbility.hasTag(ChessMoveAbility.Tags.FORWARDONLY))
                mirrorSteps = mirrorSteps.FindAll(p => p.y >= 1);


            foreach (Pos2D p in mirrorSteps)
            {
                Pos2D positionCandidate = p + this.pos;
                ChessBoard.OccupationType occupationType = chessBoard.CheckPosition(positionCandidate, this);
                bool positionIsViable = true;
                ChessMove.MoveType moveType = ChessMove.MoveType.MOVE;
                if (occupationType == ChessBoard.OccupationType.NONACCESS || occupationType == ChessBoard.OccupationType.NONACCESS || occupationType == ChessBoard.OccupationType.SAMEPARTY) positionIsViable = false;
                /* special case for wild horse eg. : */
                if (moveAbility.hasTag(ChessMoveAbility.Tags.WILD) && occupationType == ChessBoard.OccupationType.SAMEPARTY) { positionIsViable = true; moveType = ChessMove.MoveType.CAPTURE; }
                if (moveAbility.hasTag(ChessMoveAbility.Tags.NOSTRIKE) && occupationType == ChessBoard.OccupationType.ENEMY) { positionIsViable = false; moveType = ChessMove.MoveType.CAPTURE; }
                if (moveAbility.hasTag(ChessMoveAbility.Tags.STRIKEONLY) && occupationType == ChessBoard.OccupationType.FREE) { positionIsViable = false; }

                if (positionIsViable)
                {
                    viableMoves.Add(new ChessMove(this, positionCandidate, moveType));
                }

            }
        }

        return viableMoves;

        // if rider: get additional steps until limit distance is reached. 

        // ridercode

    }

    // functions:
    public bool hasTag(Tags tag)
    {
        return tags.Contains(tag);
    }
    public bool hasNotTag(Tags tag)
    {
        return !tags.Contains(tag);
    }
}


public class ChessMoveAbility
{

    public enum Tags { FORWARDONLY, RIDE, LEAP  /*  leap has currently no logical function, maybe just for animation  */, STRIKEONLY, NOSTRIKE, WILD }
    public Pos2D pos;
    public List<Tags> tags = new List<Tags>();

    public ChessMoveAbility(int x, int y, Tags[] tags_)
    {
        pos = new Pos2D(x, y);
        foreach (Tags s in tags_)
        {
            tags.Add(s);
        }
    }

    public bool hasTag(Tags tag)
    {
        return tags.Contains(tag);
    }

    public bool hasNotTag(Tags tag)
    {
        return !tags.Contains(tag);
    }


}