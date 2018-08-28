namespace CheckersLogic
{
    internal class CheckersBoard
    {
        private readonly int r_BoardSize;
        private readonly CheckersSquare[,] r_CheckersBoard;

        internal CheckersBoard(int i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            r_CheckersBoard = new CheckersSquare[r_BoardSize, r_BoardSize];
            initializeGameBoard();
        }

        internal int BoardSize
        {
            get
            {
                return r_BoardSize;
            }
        }

        internal CheckersSquare[,] CheckersGameBoard
        {
            get
            {
                return r_CheckersBoard;
            }
        }

        internal CheckersSquare GetCell(int i_Row, int i_Column)
        {
            return r_CheckersBoard[i_Row, i_Column];
        }

        private void initializeGameBoard()
        {
            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int column = 0; column < r_BoardSize; column++)
                {
                    r_CheckersBoard[row, column] = new CheckersSquare();
                    r_CheckersBoard[row, column].Location = new int[2] { row, column };
                }
            }

            initLPieceLocations(CheckersPiece.ePieceType.O);
            initLPieceLocations(CheckersPiece.ePieceType.X);
        }

        private void initLPieceLocations(CheckersPiece.ePieceType i_PieceType)
        {
            int sumOfRowsToLocate = (r_BoardSize / 2) - 1;
            CheckersMove currentMove = new CheckersMove(this);

            for (int row = 0; row < sumOfRowsToLocate; row++)
            {
                int currentRow = (i_PieceType == CheckersPiece.ePieceType.X) ? row + sumOfRowsToLocate + 2 : row;
                int curretnColumn = (currentRow % 2 == 0) ? 1 : 0;

                for (int column = curretnColumn; column < r_BoardSize; column += 2)
                {
                    r_CheckersBoard[currentRow, column].Piece = new CheckersPiece(i_PieceType, new int[2] { currentRow, column });
                    currentMove.UpdatePossibleSimpleMoves(r_CheckersBoard[currentRow, column].Piece);
                }
            }
        }

        internal void UpdatePieceInBoard(CheckersPlayer i_Player, int i_CurrentRow, int i_CurrentColumn, int i_TargetRow, int i_TargetColumn)
        {
            CheckersPiece pieceToMove = r_CheckersBoard[i_CurrentRow, i_CurrentColumn].Piece;

            r_CheckersBoard[i_TargetRow, i_TargetColumn].Piece = pieceToMove;
            r_CheckersBoard[i_CurrentRow, i_CurrentColumn].Piece = null;
            r_CheckersBoard[i_TargetRow, i_TargetColumn].Piece.Location = new int[2] { i_TargetRow, i_TargetColumn };

            changePieceToKingIfNeeded(i_Player, pieceToMove, i_TargetRow);
        }

        private void changePieceToKingIfNeeded(CheckersPlayer i_Player, CheckersPiece i_Piece, int i_TargetRow)
        {
            if (i_TargetRow == 0)
            {
                if (i_Player.PawnType == CheckersPiece.ePieceType.X)
                {
                    i_Piece.PieceType = CheckersPiece.ePieceType.K;
                }
            }
            else if (i_TargetRow == r_BoardSize - 1)
            {
                if (i_Player.PawnType == CheckersPiece.ePieceType.O)
                {
                    i_Piece.PieceType = CheckersPiece.ePieceType.U;
                }
            }
        }

        internal void RemoveOpponentPiece(int i_CurrentRow, int i_CurrentColumn, int i_TargetRow, int i_TargetColumn, CheckersPlayer i_Opponent)
        {
            int PorgressRowDirection = (i_TargetRow - i_CurrentRow) / 2;
            int PorgressColumnDirection = (i_TargetColumn - i_CurrentColumn) / 2;

            int PieceToDeleteRowLocation = i_CurrentRow + PorgressRowDirection;
            int PieceToDeleteColumnLocation = i_CurrentColumn + PorgressColumnDirection;

            CheckersPiece eatenPiece = r_CheckersBoard[PieceToDeleteRowLocation, PieceToDeleteColumnLocation].Piece;

            i_Opponent.PlayerPieces.Remove(eatenPiece);

            r_CheckersBoard[PieceToDeleteRowLocation, PieceToDeleteColumnLocation].Piece = null;
        }
    }
}
