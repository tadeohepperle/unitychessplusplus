using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ChessField
{
    public Pos2D pos;
    public bool accessible = true;

    public ChessField(int x, int y)
    {
        pos.x = x;
        pos.y = y;
        accessible = true;
    }
    public ChessField(int x, int y, bool isAccessable)
    {
        pos.x = x;
        pos.y = y;
        accessible = isAccessable;
    }
}

public class ChessBoard
{
    public int sizex;
    public int sizey;

    public ChessField[,] chessFields;
    public List<ChessAgent> chessAgents = new List<ChessAgent>();
    public ChessGameManager chessGameManager;

    #region Constructors
    public ChessBoard(int x, int y, ChessGameManager chessGameManager)
    {
        this.chessGameManager = chessGameManager;
        sizex = x;
        sizey = y;
        chessFields = new ChessField[sizex, sizey];
        for (int i = 0; i < sizex; i++)
        {
            for (int j = 0; j < sizey; j++)
            {
                chessFields[i, j] = new ChessField(i, j);
            }
        }
    }

    public ChessBoard(ChessGameManager chessGameManager)
    {
        this.chessGameManager = chessGameManager;
        sizex = 8;
        sizey = 8;
        // creates the standardchessboardwith all pieces;
        chessFields = new ChessField[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                chessFields[i, j] = new ChessField(i, j);
            }
        }


        ChessAgent playerAgent = RegisterAgent("Player", Color.white);
        ChessAgent computerAgent = RegisterAgent("Computergegner", Color.black);

        // register pawns:
        for (int i = 0; i < 8; i++)
        {
            playerAgent.RegisterPiece(ChessPiece.PieceType.PAWN, new Pos2D(i, 1));
            computerAgent.RegisterPiece(ChessPiece.PieceType.PAWN, new Pos2D(i, 6));

        }
        playerAgent.RegisterPiece(ChessPiece.PieceType.ROOK, new Pos2D(0, 0));
        playerAgent.RegisterPiece(ChessPiece.PieceType.ROOK, new Pos2D(7, 0));
        playerAgent.RegisterPiece(ChessPiece.PieceType.KNIGHT, new Pos2D(1, 0));
        playerAgent.RegisterPiece(ChessPiece.PieceType.KNIGHT, new Pos2D(6, 0));
        playerAgent.RegisterPiece(ChessPiece.PieceType.BISHOP, new Pos2D(2, 0));
        playerAgent.RegisterPiece(ChessPiece.PieceType.BISHOP, new Pos2D(5, 0));
        playerAgent.RegisterPiece(ChessPiece.PieceType.QUEEN, new Pos2D(3, 0));
        playerAgent.RegisterPiece(ChessPiece.PieceType.KING, new Pos2D(4, 0));

        computerAgent.RegisterPiece(ChessPiece.PieceType.ROOK, new Pos2D(0, 7));
        computerAgent.RegisterPiece(ChessPiece.PieceType.ROOK, new Pos2D(7, 7));
        computerAgent.RegisterPiece(ChessPiece.PieceType.KNIGHT, new Pos2D(1, 7));
        computerAgent.RegisterPiece(ChessPiece.PieceType.KNIGHT, new Pos2D(6, 7));
        computerAgent.RegisterPiece(ChessPiece.PieceType.BISHOP, new Pos2D(2, 7));
        computerAgent.RegisterPiece(ChessPiece.PieceType.BISHOP, new Pos2D(5, 7));
        computerAgent.RegisterPiece(ChessPiece.PieceType.QUEEN, new Pos2D(3, 7));
        computerAgent.RegisterPiece(ChessPiece.PieceType.KING, new Pos2D(4, 7));

    }

    #endregion



    public List<ChessPiece> GetAllPieces()
    {
        List<ChessPiece> pieces = new List<ChessPiece>();
        foreach (var ca in this.chessAgents)
        {
            pieces.AddRange(ca.ChessPieces);
        }
        return pieces;
    }

    public ChessAgent RegisterAgent(string name, Color color)
    {
        ChessAgent ca = new ChessAgent(name, color, this);
        chessAgents.Add(ca);
        return ca;
    }

    public enum OccupationType { FREE, SAMEPARTY, ENEMY, OUT, NONACCESS }


    public OccupationType CheckPosition(Pos2D pos, ChessPiece inquisitor)
    {
        // broad check if on board:
        if (pos.x < 0 || pos.x > sizex - 1 || pos.y < 0 || pos.y > sizey - 1) return OccupationType.OUT;
        // check if field is accesseble:
        if (!chessFields[pos.x, pos.y].accessible) return OccupationType.NONACCESS;
        // loop over other pieces:

        foreach (ChessAgent a in chessAgents)
        {
            foreach (ChessPiece cp in a.ChessPieces)
            {
                if (cp.pos == pos)
                {
                    if (inquisitor != null && cp.agent == inquisitor.agent) return OccupationType.SAMEPARTY;
                    else return OccupationType.ENEMY;
                }
            }
        }
        return OccupationType.FREE;

    }

}