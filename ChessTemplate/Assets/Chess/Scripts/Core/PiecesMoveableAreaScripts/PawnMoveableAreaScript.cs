using Chess.Scripts.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core.PiecesMoveableAreaScripts
{
    public class PawnMoveableAreaScript : MonoBehaviour
    {
        private ChessPlayerPlacementHandler m_PlayerPlacementHandler;
        private ChessBoardPlacementHandler m_BoardPlacementHandler;
        private int row, col;

        private void Start()
        {
            m_PlayerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();
            m_BoardPlacementHandler = GameObject.FindGameObjectWithTag("ChessBoard").GetComponent<ChessBoardPlacementHandler>();



            m_PlayerPlacementHandler.OnSelectedValueChange += PawnMovements;

        }
        private void Update()
        {
            row = m_PlayerPlacementHandler.row;
            col = m_PlayerPlacementHandler.column;
        }


        private void PawnMovements(bool isSelected)
        {
            if (isSelected)
            {
                CalculateMoveableAreaTiles();
            }
            else
            {
                m_BoardPlacementHandler.ClearHighlights();
            }
        }

        private void CalculateMoveableAreaTiles()
        {
            if (gameObject.name == "WhitePawn")
            {
                if (row == 6)
                {
                    var tile = m_BoardPlacementHandler.GetTile(row - 1, col);
                    if (tile.GetComponent<TileScript>().IsEmpty)
                    {
                        m_BoardPlacementHandler.Highlight(row - 1, col);
                        var tile2 = m_BoardPlacementHandler.GetTile(row - 2, col);
                        if (tile2.GetComponent<TileScript>().IsEmpty)
                        {
                            m_BoardPlacementHandler.Highlight(row - 2, col);
                        }
                    }
                }
                else
                {
                    CheckPawnMove(row - 1, col);
                }

                CheckPawnAttack(row - 1, col + 1);
                CheckPawnAttack(row - 1, col - 1);

            }

            if (gameObject.name == "BlackPawn")
            {
                if (row == 1)
                {
                    var tile = m_BoardPlacementHandler.GetTile(row + 1, col);
                    if (tile.GetComponent<TileScript>().IsEmpty)
                    {
                        m_BoardPlacementHandler.Highlight(row + 1, col);
                        var tile2 = m_BoardPlacementHandler.GetTile(row + 2, col);
                        if (tile2.GetComponent<TileScript>().IsEmpty)
                        {
                            m_BoardPlacementHandler.Highlight(row + 2, col);
                        }
                    }
                }
                else
                {
                    CheckPawnMove(row + 1, col);
                }

                CheckPawnAttack(row + 1, col + 1);
                CheckPawnAttack(row + 1, col - 1);
            }
        }

        private void CheckPawnMove(int newRow, int newCol)
        {
            if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                var tile = m_BoardPlacementHandler.GetTile(newRow, newCol);
                if (tile.GetComponent<TileScript>().IsEmpty)
                {
                    m_BoardPlacementHandler.Highlight(newRow, newCol);
                }
            }
        }

        private void CheckPawnAttack(int newRow, int newCol)
        {
            if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                var tile = m_BoardPlacementHandler.GetTile(newRow, newCol);
                if (!tile.GetComponent<TileScript>().IsEmpty)
                {
                    // There is a piece on the target tile.
                    if (!tile.GetComponent<TileScript>().Piece.CompareTag(gameObject.transform.tag))
                    {
                        // The piece on the target tile is of a different tag (an enemy).
                        m_BoardPlacementHandler.HighlightRed(newRow, newCol);
                    }
                }
            }
        }


        private void OnDestroy()
        {
            m_PlayerPlacementHandler.OnSelectedValueChange -= PawnMovements;
        }
    }
}