using Chess.Scripts.Core;
using UnityEngine;

namespace Chess.Scripts.Core.PiecesMoveableAreaScripts
{
    public class KnightMoveableAreaScript : MonoBehaviour
    {
        private ChessPlayerPlacementHandler m_PlayerPlacementHandler;
        private ChessBoardPlacementHandler m_BoardPlacementHandler;
        private int row, col;

        private void Start()
        {
            m_PlayerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();
            m_BoardPlacementHandler = GameObject.FindGameObjectWithTag("ChessBoard").GetComponent<ChessBoardPlacementHandler>();

            m_PlayerPlacementHandler.OnSelectedValueChange += KnightMovements;
        }

        private void Update()
        {
            row = m_PlayerPlacementHandler.row;
            col = m_PlayerPlacementHandler.column;
        }

        private void KnightMovements(bool isSelected)
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
            // Clear existing highlights
            m_BoardPlacementHandler.ClearHighlights();

            // Check all possible knight move positions
            CheckKnightMove(row + 2, col + 1);
            CheckKnightMove(row + 2, col - 1);
            CheckKnightMove(row - 2, col + 1);
            CheckKnightMove(row - 2, col - 1);
            CheckKnightMove(row + 1, col + 2);
            CheckKnightMove(row + 1, col - 2);
            CheckKnightMove(row - 1, col + 2);
            CheckKnightMove(row - 1, col - 2);
        }

        private void CheckKnightMove(int newRow, int newCol)
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
            m_PlayerPlacementHandler.OnSelectedValueChange -= KnightMovements;
        }
    }
}