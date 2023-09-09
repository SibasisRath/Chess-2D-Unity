using Chess.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess.Scripts.Core.PiecesMoveableAreaScripts
{
    public class KingMoveableAreaScript : MonoBehaviour
    {
        private ChessPlayerPlacementHandler m_PlayerPlacementHandler;
        private ChessBoardPlacementHandler m_BoardPlacementHandler;
        private int row, col;

        private void Start()
        {
            m_PlayerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();
            m_BoardPlacementHandler = GameObject.FindGameObjectWithTag("ChessBoard").GetComponent<ChessBoardPlacementHandler>();


            m_PlayerPlacementHandler.OnSelectedValueChange += KingMovements;
        }

        private void Update()
        {
            row = m_PlayerPlacementHandler.row;
            col = m_PlayerPlacementHandler.column;
        }

        private void KingMovements(bool isSelected)
        {
            if (isSelected == true)
            {
                CalculateMoveableAreaTiles();
            }
            if (isSelected == false)
            {
                m_BoardPlacementHandler.ClearHighlights();
            }
        }


        private void CalculateMoveableAreaTiles()
        {
            // Check all possible knight move positions
            CheckKingMove(row + 1, col + 0);
            CheckKingMove(row + 1, col + 1);
            CheckKingMove(row + 1, col - 1);
            CheckKingMove(row + 0, col + 1);
            CheckKingMove(row + 0, col - 1);
            CheckKingMove(row - 1, col + 0);
            CheckKingMove(row - 1, col + 1);
            CheckKingMove(row - 1, col - 1);

        }

        private void CheckKingMove(int newRow, int newCol)
        {
            if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                var tile = m_BoardPlacementHandler.GetTile(newRow, newCol);
                if (tile.GetComponent<TileScript>().IsEmpty)
                {
                    m_BoardPlacementHandler.Highlight(newRow, newCol);
                }
                else
                {
                    if (!tile.GetComponent<TileScript>().Piece.CompareTag(gameObject.transform.tag))
                    {
                        m_BoardPlacementHandler.HighlightRed(newRow, newCol);
                    }
                }
            }
        }

        private void OnDestroy()
        {
            m_PlayerPlacementHandler.OnSelectedValueChange -= KingMovements;
        }
    }
}