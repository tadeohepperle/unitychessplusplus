using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestateVisualizer : Singleton<GamestateVisualizer>
{



    public List<ChessPieceRepresentation> representationObjects = new List<ChessPieceRepresentation>();

    public void Startup()
    {
        InstatiateChessPiecesAsObjects(); InstatiateGameBoardObject();
    }

    void InstatiateChessPiecesAsObjects()
    {

        GameObject parent = new GameObject("ChessPieces");
        List<ChessPiece> pieces = ChessGameManager.Instance.chessBoard.GetAllPieces();
        foreach (var piece in pieces)
        {
            GameObject obj = CreateChessPieceRepresentationObject(piece);
            obj.transform.SetParent(parent.transform);
        }
    }

    GameObject CreateChessPieceRepresentationObject(ChessPiece piece)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = piece.agent.name + " " + piece.Type.ToString();
        obj.transform.position = RepresentationMapping.MapBoardPos2D(piece.pos);
        obj.transform.localScale = Vector3.one * 0.7f;
        obj.GetComponent<MeshRenderer>().material.color = piece.agent.color;

        GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        head.transform.position = RepresentationMapping.MapBoardPos2D(piece.pos) + Vector3.up * 0.3f;
        head.transform.localScale = Vector3.one * 0.5f;
        head.GetComponent<MeshRenderer>().material.color = ColorFromPieceType(piece.Type);
        head.transform.SetParent(obj.transform);
        Destroy(head.GetComponent<SphereCollider>());


        ChessPieceRepresentation cpr = obj.AddComponent<ChessPieceRepresentation>();
        cpr.chessPiece = piece;
        representationObjects.Add(cpr);

        return obj;

    }

    Color ColorFromPieceType(ChessPiece.PieceType type)
    {
        switch (type)
        {
            case ChessPiece.PieceType.BISHOP: return Color.magenta;
            case ChessPiece.PieceType.KING: return Color.red;
            case ChessPiece.PieceType.KNIGHT: return Color.green;
            case ChessPiece.PieceType.PAWN: return Color.yellow;
            case ChessPiece.PieceType.QUEEN: return Color.cyan;
            case ChessPiece.PieceType.ROOK: return Color.grey;
            default: return Color.grey;
        }
    }

    void UpdatePositionOfRepresentationObjects()
    {
        foreach (var cpr in representationObjects)
        {
            cpr.gameObject.transform.position = RepresentationMapping.MapBoardPos2D(cpr.chessPiece.pos);
        }

    }

    void InstatiateGameBoardObject()
    {

        GameObject parent = new GameObject("Schachbrett");

        ChessField[,] fields = ChessGameManager.Instance.chessBoard.chessFields;
        for (int x = 0; x < fields.GetLength(0); x++)
        {
            for (int y = 0; y < fields.GetLength(1); y++)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = RepresentationMapping.MapBoardPos2D(new Pos2D(x, y)) + Vector3.down;
                obj.GetComponent<MeshRenderer>().material.color = (x + y) % 2 == 0 ? Color.black : Color.white;
                obj.transform.SetParent(parent.transform);
            }
        }

    }


    /*
        void OnDrawGizmos() // draw the state of the board and pieces
        {
            return;
            if (ChessGameManager.Instance == null) return;
            ChessBoard chessboard = ChessGameManager.Instance.chessBoard;
            // draw Chessboard
            if (chessboard == null) return;
            for (int x = 0; x < chessboard.chessFields.GetLength(0); x++)
            {
                for (int y = 0; y < chessboard.chessFields.GetLength(1); y++)
                {
                    Gizmos.color = ((x + y) % 2 == 0) ? Color.white : Color.black;
                    Gizmos.DrawCube(new Vector3(x, 0, y), Vector3.one);
                }
            }
            // draw figures:
            if (chessboard.chessAgents != null)
            {

                foreach (ChessAgent ca in chessboard.chessAgents)
                {
                    foreach (ChessPiece piece in ca.ChessPieces)
                    {
                        GizmosDrawChessPiece(piece);
                    }
                }
            }
        }
    */




    void GizmosDrawChessPiece(ChessPiece piece)
    {
        Color savedColor = Gizmos.color;
        Pos2D pos = piece.pos;
        Gizmos.color = piece.agent.color;

        switch (piece.Type)
        {
            case ChessPiece.PieceType.PAWN:
                Gizmos.DrawWireSphere(Vector3.up + pos, 0.5f);
                Gizmos.DrawWireSphere(Vector3.up * 1.7f + pos, 0.3f);
                break;
            case ChessPiece.PieceType.QUEEN:
                Gizmos.DrawWireSphere(Vector3.up + pos, 0.5f);
                Gizmos.DrawWireSphere(Vector3.up * 1.7f + pos, 0.2f);
                Gizmos.DrawWireSphere(Vector3.up * 2.0f + pos, 0.3f);
                Gizmos.DrawWireSphere(Vector3.up * 2.5f + pos, 0.3f);
                break;
            case ChessPiece.PieceType.KING:
                Gizmos.DrawWireSphere(Vector3.up + pos, 0.5f);
                Gizmos.DrawWireSphere(Vector3.up * 1.7f + pos, 0.2f);
                Gizmos.DrawWireSphere(Vector3.up * 2.0f + pos, 0.3f);
                Gizmos.DrawWireCube(Vector3.up * 2.5f + pos, Vector3.one * 0.3f);
                break;
            case ChessPiece.PieceType.ROOK:
                Gizmos.DrawWireCube(Vector3.up + pos, Vector3.one * 0.7f);
                Gizmos.DrawWireCube(Vector3.up * 1.2f + pos, Vector3.one * 0.5f);
                Gizmos.DrawWireCube(Vector3.up * 1.6f + pos, Vector3.one * 0.6f);
                break;
            case ChessPiece.PieceType.KNIGHT:
                Gizmos.DrawWireCube(Vector3.up + pos, new Vector3(0.3f, 0.8f, 0.5f));
                Gizmos.DrawWireCube(Vector3.up * 1.5f + pos, new Vector3(0.4f, 0.8f, 0.5f) * 0.4f);
                Gizmos.DrawWireSphere(Vector3.up * 1.6f + pos, 0.5f);
                break;
            case ChessPiece.PieceType.BISHOP:
                Gizmos.DrawWireSphere(Vector3.up + pos, 0.5f);
                Gizmos.DrawWireSphere(Vector3.up * 1.6f + pos, 0.4f);
                Gizmos.DrawWireSphere(Vector3.up * 2f + pos, 0.3f);
                break;

        }

        Gizmos.color = savedColor;
    }



}
