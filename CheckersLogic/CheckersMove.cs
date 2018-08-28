using System.Collections.Generic;
using System.Linq;

namespace CheckersLogic
{
    internal class CheckersMove
    {
        private const int k_SimpleMove = 1;
        private const int k_EatMove = 2;
        private readonly int r_CurremtRow;
        private readonly int r_CurrentColumn;
        private readonly int r_TargetRow;
        private readonly int r_TargetColumn;
        private readonly CheckersPlayer r_Player;
        private readonly CheckersPlayer r_Opponent;
        private readonly CheckersBoard r_GameBoard;
        private bool m_IsPieceBecomeKing;
        private bool m_IsNeedToEatAgain;

        internal CheckersMove(CheckersBoard i_GameBoard)
        {
            r_GameBoard = i_GameBoard;
            m_IsPieceBecomeKing = false;
            m_IsNeedToEatAgain = false;
        }

        internal CheckersMove(CheckersPlayer i_Player, CheckersPlayer i_Opponent, CheckersBoard i_GameBoard)
        {
            r_Player = i_Player;
            r_Opponent = i_Opponent;
            r_GameBoard = i_GameBoard;
            m_IsPieceBecomeKing = false;
            m_IsNeedToEatAgain = false;
        }

        internal CheckersMove(CheckersPlayer i_Player, CheckersPlayer i_Opponent, CheckersBoard i_GameBoard, int[] i_StartingLocation, int[] i_DestinationLocation)
        {
            r_Player = i_Player;
            r_Opponent = i_Opponent;
            r_CurremtRow = i_StartingLocation[0];
            r_CurrentColumn = i_StartingLocation[1];
            r_TargetRow = i_DestinationLocation[0];
            r_TargetColumn = i_DestinationLocation[1];
            r_GameBoard = i_GameBoard;
            m_IsPieceBecomeKing = false;
            m_IsNeedToEatAgain = false;
        }

        internal bool IsPreformedMove()
        {
            CheckersPiece pieceToMove = r_GameBoard.GetCell(r_CurremtRow, r_CurrentColumn).Piece;
            bool isLegalMove = false;
            m_IsNeedToEatAgain = false;

            UpdateAllPlayerPieces(r_Player);

            bool isValidPlayerPiece = isValidPiece(pieceToMove);

            if (isValidPlayerPiece && IsPlayerMustEat(r_Player))
            {
                if (isLegalMove = isMoveInList(pieceToMove.PossibleEatMoves))
                {
                    makeEatMove(pieceToMove);
                    m_IsNeedToEatAgain = pieceToMove.PossibleEatMoves.Any();
                }
            }
            else if (isValidPlayerPiece && isSimpleMoveLeft() && !m_IsNeedToEatAgain)
            {
                if (isLegalMove = isMoveInList(pieceToMove.PossibleSimpleMoves))
                {
                    makeSimpleMove(pieceToMove);
                }
            }

            updateEndOfMove();

            return isLegalMove;
        }

        internal bool IsNeedToEatAgain
        {
            get { return m_IsNeedToEatAgain; }
        }

        private void updateEndOfMove()
        {
            UpdateAllPlayerPieces(r_Player);
            UpdateAllPlayerPieces(r_Opponent);
            updateIsLegalMovesLeft(r_Player);
            updateIsLegalMovesLeft(r_Opponent);
        }

        internal void UpdateAllPlayerPieces(CheckersPlayer i_Player)
        {
            foreach (CheckersPiece piece in i_Player.PlayerPieces)
            {
                UpdatePossibleSimpleMoves(piece);
                updatePossibleEatMoves(piece);
            }
        }

        internal void UpdatePossibleSimpleMoves(CheckersPiece i_Piece)
        {
            i_Piece.PossibleSimpleMoves.Clear();
            int moveToNegativSide = -1 * k_SimpleMove;
            int moveToPositiveSide = k_SimpleMove;

            if (isValidMove(i_Piece, moveToPositiveSide, moveToPositiveSide, k_SimpleMove))
            {
                addMove(i_Piece, moveToPositiveSide, moveToPositiveSide, k_SimpleMove);
            }

            if (isValidMove(i_Piece, moveToNegativSide, moveToNegativSide, k_SimpleMove))
            {
                addMove(i_Piece, moveToNegativSide, moveToNegativSide, k_SimpleMove);
            }

            if (isValidMove(i_Piece, moveToPositiveSide, moveToNegativSide, k_SimpleMove))
            {
                addMove(i_Piece, moveToPositiveSide, moveToNegativSide, k_SimpleMove);
            }

            if (isValidMove(i_Piece, moveToNegativSide, moveToPositiveSide, k_SimpleMove))
            {
                addMove(i_Piece, moveToNegativSide, moveToPositiveSide, k_SimpleMove);
            }
        }

        private void updatePossibleEatMoves(CheckersPiece i_Piece)
        {
            i_Piece.PossibleEatMoves.Clear();
            int negativMove = -1 * k_EatMove;
            int positiveMove = k_EatMove;

            if (isValidMove(i_Piece, positiveMove, positiveMove, k_EatMove) && isEatOverOpponent(i_Piece, positiveMove, positiveMove))
            {
                addMove(i_Piece, positiveMove, positiveMove, k_EatMove);
            }

            if (isValidMove(i_Piece, negativMove, negativMove, k_EatMove) && isEatOverOpponent(i_Piece, negativMove, negativMove))
            {
                addMove(i_Piece, negativMove, negativMove, k_EatMove);
            }

            if (isValidMove(i_Piece, positiveMove, negativMove, k_EatMove) && isEatOverOpponent(i_Piece, positiveMove, negativMove))
            {
                addMove(i_Piece, positiveMove, negativMove, k_EatMove);
            }

            if (isValidMove(i_Piece, negativMove, positiveMove, k_EatMove) && isEatOverOpponent(i_Piece, negativMove, positiveMove))
            {
                addMove(i_Piece, negativMove, positiveMove, k_EatMove);
            }
        }

        private void addMove(CheckersPiece i_Piece, int i_RowMove, int i_ColumnMove, int i_MoveType)
        {
            int[] newMove = new int[2];
            newMove[0] = i_Piece.Location[0] + i_RowMove;
            newMove[1] = i_Piece.Location[1] + i_ColumnMove;

            if (i_MoveType == k_SimpleMove)
            {
                i_Piece.PossibleSimpleMoves.Add(newMove);
            }
            else
            {
                i_Piece.PossibleEatMoves.Add(newMove);
            }
        }

        private bool isEatOverOpponent(CheckersPiece i_Piece, int i_RowMove, int i_ColumnMove)
        {
            int rowToCheck = i_Piece.Location[0] + (i_RowMove / 2);
            int columnToCheck = i_Piece.Location[1] + (i_ColumnMove / 2);
            bool isCellEmpty = r_GameBoard.GetCell(rowToCheck, columnToCheck).Piece != null;
            bool isEatOverOpponent = false;

            if (isCellEmpty)
            {
                isEatOverOpponent = r_GameBoard.GetCell(rowToCheck, columnToCheck).Piece.PieceType == r_Opponent.PawnType;
                isEatOverOpponent = isEatOverOpponent || r_GameBoard.GetCell(rowToCheck, columnToCheck).Piece.PieceType == r_Opponent.KingType;
            }

            return isEatOverOpponent;
        }

        private bool isValidMove(CheckersPiece i_Piece, int i_RowTargetMove, int i_ColumnTargetMove, int i_MoveType)
        {
            int targetRow = i_Piece.Location[0] + i_RowTargetMove;
            int targetColumn = i_Piece.Location[1] + i_ColumnTargetMove;
            bool isValidMove = isMoveInBounderies(targetRow, targetColumn) && isMoveInBounds(i_MoveType, i_Piece, targetRow, targetColumn);

            return isValidMove && isTargetEmpty(targetRow, targetColumn);
        }

        private bool isMoveInBounderies(int i_TargetRow, int i_TargetColumn)
        {
            int boardSize = r_GameBoard.BoardSize;

            return (i_TargetRow >= 0) && (i_TargetRow < boardSize) && (i_TargetColumn >= 0) && (i_TargetColumn < boardSize);
        }

        private bool isMoveInBounds(int i_MoveType, CheckersPiece i_Piece, int i_TargetRow, int i_TargetColumn)
        {
            int currentRow = i_Piece.Location[0];
            int currentColumn = i_Piece.Location[1];

            return isValidRowMove(i_MoveType, currentRow, i_TargetRow, i_Piece.PieceType) && isValidColumnMove(i_MoveType, currentColumn, i_TargetColumn);
        }

        private bool isValidRowMove(int i_TypeMove, int i_CurrentRow, int i_TargetRow, CheckersPiece.ePieceType i_PieceType)
        {
            bool isValidTargetRow;

            if (i_PieceType == CheckersPiece.ePieceType.O)
            {
                isValidTargetRow = i_TargetRow == i_CurrentRow + i_TypeMove;
            }
            else if (i_PieceType == CheckersPiece.ePieceType.X)
            {
                isValidTargetRow = i_TargetRow == i_CurrentRow - i_TypeMove;
            }
            else
            {
                isValidTargetRow = (i_TargetRow == i_CurrentRow + i_TypeMove) || (i_TargetRow == i_CurrentRow - i_TypeMove);
            }

            return isValidTargetRow;
        }

        private bool isValidColumnMove(int i_TypeMove, int i_CourrentColumn, int i_TargetColumn)
        {
            bool isValidTargetColumn = (i_TargetColumn == i_CourrentColumn + i_TypeMove) || (i_TargetColumn == i_CourrentColumn - i_TypeMove);

            return isValidTargetColumn;
        }

        private bool isTargetEmpty(int i_TargetRow, int i_TargetColumn)
        {
            bool isCellEmpty = r_GameBoard.GetCell(i_TargetRow, i_TargetColumn).Piece == null;

            return isCellEmpty;
        }

        private bool isValidPiece(CheckersPiece i_Piece)
        {
            bool isValidPiece = (i_Piece != null) ? (i_Piece.PieceType.Equals(r_Player.PawnType) || i_Piece.PieceType.Equals(r_Player.KingType)) : false;

            return isValidPiece;
        }

        internal bool IsPlayerMustEat(CheckersPlayer i_Player)
        {
            bool isMustEat = false;

            foreach (CheckersPiece piece in i_Player.PlayerPieces)
            {
                isMustEat = piece.PossibleEatMoves.Any();

                if (isMustEat)
                {
                    break;
                }
            }

            return isMustEat;
        }

        private bool isMoveInList(List<int[]> i_PossibleMoves)
        {
            bool isMoveInList = false;

            foreach (int[] pieceLocation in i_PossibleMoves)
            {
                isMoveInList = isMoveInList || (r_TargetRow == pieceLocation[0] && r_TargetColumn == pieceLocation[1]);
            }

            return isMoveInList;
        }

        private void makeEatMove(CheckersPiece i_Piece)
        {
            m_IsPieceBecomeKing = isPieceGoingToBecomeKing(i_Piece);
            r_GameBoard.RemoveOpponentPiece(r_CurremtRow, r_CurrentColumn, r_TargetRow, r_TargetColumn, r_Opponent);
            r_GameBoard.UpdatePieceInBoard(r_Player, r_CurremtRow, r_CurrentColumn, r_TargetRow, r_TargetColumn);
            updatePossibleEatMoves(i_Piece);
        }

        private void makeSimpleMove(CheckersPiece i_Piece)
        {
            r_GameBoard.UpdatePieceInBoard(r_Player, r_CurremtRow, r_CurrentColumn, r_TargetRow, r_TargetColumn);
        }

        private void updateIsLegalMovesLeft(CheckersPlayer i_Player)
        {
            i_Player.IsLegalMovesLeft = false;

            foreach (CheckersPiece piece in i_Player.PlayerPieces)
            {
                i_Player.IsLegalMovesLeft = i_Player.IsLegalMovesLeft || piece.PossibleSimpleMoves.Any() || piece.PossibleEatMoves.Any();
            }
        }

        private bool isSimpleMoveLeft()
        {
            bool isSimpleMoveLeft = false;

            foreach (CheckersPiece piece in r_Player.PlayerPieces)
            {
                isSimpleMoveLeft = piece.PossibleSimpleMoves.Any();

                if (isSimpleMoveLeft)
                {
                    break;
                }
            }

            return isSimpleMoveLeft;
        }

        private bool isPieceGoingToBecomeKing(CheckersPiece i_Piece)
        {
            bool isBecomingKing;

            if (r_Player.PawnType.Equals(CheckersPiece.ePieceType.X))
            {
                isBecomingKing = i_Piece.PieceType.Equals(CheckersPiece.ePieceType.X) && (r_TargetRow == 0);
            }
            else
            {
                isBecomingKing = i_Piece.PieceType.Equals(CheckersPiece.ePieceType.O) && (r_TargetRow == r_GameBoard.BoardSize - 1);
            }
            
            return isBecomingKing;
        }

        internal bool IsPieceBecomeKing
        {
            get
            {
                return m_IsPieceBecomeKing;
            }
        }
    }
}
