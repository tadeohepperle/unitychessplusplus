using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChessGameManager : Singleton<ChessGameManager>
{

    public int Turn { get; private set; }
    public ChessBoard chessBoard;
    public int sizex = 8;
    public int sizey = 8;
    public delegate void BoardAction();
    public static event BoardAction OnBoardChange;



    public void FireBoardEvent(BoardAction OnSomething)
    {
        if (OnSomething != null)
        {
            OnSomething();
        }

    }


    // Update is called once per frame

    void Start()
    {
        this.chessBoard = new ChessBoard(this);
        GamestateVisualizer.Instance.Startup();

    }



}
