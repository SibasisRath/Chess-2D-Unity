using Chess.Scripts.Core;
using UnityEngine;

namespace Chess.Scripts.Core.PiecesMoveableAreaScripts
{
    public class QueenMoveableAreaScript : MonoBehaviour
    {
        private ChessPlayerPlacementHandler m_PlayerPlacementHandler;
        private ChessBoardPlacementHandler m_BoardPlacementHandler;
        private int row, col;

        private void Start()
        {
            m_PlayerPlacementHandler = GetComponent<ChessPlayerPlacementHandler>();
            m_BoardPlacementHandler = GameObject.FindGameObjectWithTag("ChessBoard").GetComponent<ChessBoardPlacementHandler>();

            m_PlayerPlacementHandler.OnSelectedValueChange += QueenMovements;
        }

        private void Update()
        {
            row = m_PlayerPlacementHandler.row;
            col = m_PlayerPlacementHandler.column;
        }

        private void QueenMovements(bool isSelected)
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
            HighlightHorizontalAndVerticalTiles(row + 1, col, 1, 0); // Up
            HighlightHorizontalAndVerticalTiles(row - 1, col, -1, 0); // Down
            HighlightHorizontalAndVerticalTiles(row, col + 1, 0, 1); // Right
            HighlightHorizontalAndVerticalTiles(row, col - 1, 0, -1); // Left
            HighlightHorizontalAndVerticalTiles(row + 1, col + 1, 1, 1); // Up - Right
            HighlightHorizontalAndVerticalTiles(row - 1, col + 1, -1, 1); // Down - Right
            HighlightHorizontalAndVerticalTiles(row + 1, col - 1, 1, -1); // Up - Left
            HighlightHorizontalAndVerticalTiles(row - 1, col - 1, -1, -1); // Down - Left
        }

        private void HighlightHorizontalAndVerticalTiles(int startRow, int startCol, int rowStep, int colStep)
        {
            int updatedRow = startRow;
            int updatedCol = startCol;

            while (IsInBounds(updatedRow, updatedCol) && CheckForAnyPieceInQueensPath(updatedRow, updatedCol))
            {
                updatedRow += rowStep;
                updatedCol += colStep;
            }
        }

        private bool IsInBounds(int row, int col)
        {
            return row >= 0 && row < 8 && col >= 0 && col < 8;
        }

        private bool CheckForAnyPieceInQueensPath(int newRow, int newCol)
        {
            var tile = m_BoardPlacementHandler.GetTile(newRow, newCol);
            if (tile.GetComponent<TileScript>().IsEmpty)
            {
                m_BoardPlacementHandler.Highlight(newRow, newCol);
                return true;
            }
            else
            {
                if (!tile.GetComponent<TileScript>().Piece.CompareTag(gameObject.transform.tag))
                {
                    m_BoardPlacementHandler.HighlightRed(newRow, newCol);
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }


        private void OnDestroy()
        {
            m_PlayerPlacementHandler.OnSelectedValueChange -= QueenMovements;
        }
    }
}