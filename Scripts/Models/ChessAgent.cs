using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


// players, ai, all npcs are chessagents
public class ChessAgent
{
    public string name { get; }
    public Color color;
    public ChessBoard ChessBoard { get; private set; }
    public List<ChessPiece> ChessPieces { get; private set; } = new List<ChessPiece>();
    public bool RegisterPiece(ChessPiece.PieceType type_, Pos2D pos_)
    {
        //Debug.Log("Register " + type_.ToString() + " at " + pos_.ToString());
        if (ChessBoard.CheckPosition(pos_, null) == ChessBoard.OccupationType.FREE)
        {
            ChessPiece piece = new ChessPiece(type_, pos_, this);
            this.ChessPieces.Add(piece);
            return true;
        }
        return false;
    }

    public ChessAgent(string name_, ChessBoard board)
    {
        this.name = name_;
        this.ChessBoard = board;
    }

    public ChessAgent(string name_, Color color_, ChessBoard board)
    {
        this.color = color_;
        this.name = name_;
        this.ChessBoard = board;
    }


}

