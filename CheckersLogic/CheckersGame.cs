using System.Collections.Generic;
using System.Linq;

namespace CheckersLogic
{
    public class CheckersGame
    {
        private const string k_EmptyCell = "Empty";
        private readonly int r_BoardSize;
        private CheckersPlayer m_Player1;
        private CheckersPlayer m_Player2;
        private CheckersPlayer m_PlayerTurn;
        private CheckersPlayer m_Opponent;
        private bool m_IsPlayerQuit;
        private bool m_IsTie;
        private bool m_IsWin;
        private CheckersBoard m_GameBoard;
        private int m_CurrentTurnIndex;
        private bool m_IsNeedToEatAgain;

        public CheckersGame(int i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
        }

        public bool IsPlayerQuit
        {
            get
            {
                isPlayerCanQuit();

                return m_IsPlayerQuit;
            }
        }

        public bool IsTie
        {
            get
            {
                return m_IsTie;
            }
        }

        public bool IsWin
        {
            get
            {
                return m_IsWin;
            }
        }

        public bool IsNeedToEatAgain
        {
            get
            {
                return m_IsNeedToEatAgain;
            }
        }

        public string WinnerName
        {
            get
            {
                string winnerName = m_Player1.IsWin ? m_Player1.Name : m_Player2.Name;
                m_Player1.IsWin = false;
                m_Player2.IsWin = false;

                return winnerName;
            }
        }

        public string PlayerTurnName
        {
            get
            {
                updatePlayersMembers();

                return m_PlayerTurn.Name;
            }
        }

        public string OpponentName
        {
            get
            {
                updatePlayersMembers();

                return m_Opponent.Name;
            }
        }

        public string Player1Name
        {
            get
            {
                return m_Player1.Name;
            }
        }

        public string Player2Name
        {
            get
            {
                return m_Player2.Name;
            }
        }

        public int Player1Score
        {
            get
            {
                return m_Player1.TotalScore;
            }
        }

        public int Player2Score
        {
            get
            {
                return m_Player2.TotalScore;
            }
        }

        private void isPlayerCanQuit()
        {
            updatePlayersMembers();

            if (isValidQuit(m_PlayerTurn, m_Opponent))
            {
                m_IsPlayerQuit = true;
                m_PlayerTurn.IsWin = false;
                m_Opponent.IsWin = true;
            }
        }

        private void updatePlayersMembers()
        {
            m_PlayerTurn = ((m_CurrentTurnIndex % 2) == 0) ? m_Player1 : m_Player2;
            m_Opponent = ((m_CurrentTurnIndex % 2) == 0) ? m_Player2 : m_Player1;
        }
        
        private bool isValidQuit(CheckersPlayer i_Player, CheckersPlayer i_Opponent)
        {
            return i_Player.IsValidPointsDifference(i_Opponent);
        }

        public void StartGame(string i_Player1Name, string i_Player2Name)
        {
            initializeGameParams(i_Player1Name, i_Player2Name);
            InitializeGame();
        }

        private void initializeGameParams(string i_Player1Name, string i_Player2Name)
        {
            m_Player1 = new CheckersPlayer(i_Player1Name, CheckersPiece.ePieceType.X, CheckersPiece.ePieceType.K);
            m_Player2 = new CheckersPlayer(i_Player2Name, CheckersPiece.ePieceType.O, CheckersPiece.ePieceType.U);
        }

        public void InitializeGame()
        {
            m_IsPlayerQuit = false;
            m_GameBoard = new CheckersBoard(r_BoardSize);
            initilaizePlayerPieces(m_Player1);
            initilaizePlayerPieces(m_Player2);
        }

        private void initilaizePlayerPieces(CheckersPlayer i_Player)
        {
            i_Player.PlayerPieces = new List<CheckersPiece>();

            foreach (CheckersSquare square in m_GameBoard.CheckersGameBoard)
            {
                if (square.Piece != null)
                {
                    CheckersPiece.ePieceType pieceType = square.Piece.PieceType;
                    if (pieceType == i_Player.PawnType)
                    {
                        i_Player.PlayerPieces.Add(square.Piece);
                    }
                }
            }
        }

        private bool isTieCondition()
        {
            return !m_PlayerTurn.IsLegalMovesLeft && !m_Opponent.IsLegalMovesLeft;
        }

        private bool isPlayerWin()
        {
            bool noLegalOpponentMoves = !m_Opponent.IsLegalMovesLeft && m_PlayerTurn.IsLegalMovesLeft;
            bool isWin = !m_Opponent.PlayerPieces.Any() || noLegalOpponentMoves;

            m_PlayerTurn.IsWin = isWin;

            return isWin;
        }

        public void UpdateEndOfCurrentGame()
        {
            updatePlayerScoreInCurrentGame(m_Player1, m_Player2);
            m_Player1.UpdatePlayerScore();
            m_Player2.UpdatePlayerScore();
        }

        private void updatePlayerScoreInCurrentGame(CheckersPlayer i_Player, CheckersPlayer i_Opponent)
        {
            if (i_Opponent.IsWin)
            {
                i_Opponent.ScoreInCurrentGame = i_Opponent.GetPlayerPoints() - i_Player.GetPlayerPoints();
            }
            else
            {
                i_Player.ScoreInCurrentGame = i_Player.GetPlayerPoints() - i_Opponent.GetPlayerPoints();
            }
        }

        public bool IsPerformedHumanMove(int[] i_StartingLocation, int[] i_TargetLocation)
        {
            updatePlayersMembers();

            return isPerformeMove(i_StartingLocation, i_TargetLocation);
        }

        public void PerformComputerMove()
        {
            updatePlayersMembers();
            List<int> randomMove = new CheckersRandomMove().ChooseRandomMove(m_PlayerTurn, m_Opponent, m_GameBoard);
            int[] startingLocation = new int[] { randomMove[0], randomMove[1] };
            int[] targetLocation = new int[] { randomMove[2], randomMove[3] };
            bool performMove = isPerformeMove(startingLocation, targetLocation);
        }

        private bool isPerformeMove(int[] i_StartingLocation, int[] i_TargetLocation)
        {
            CheckersMove newMove = new CheckersMove(m_PlayerTurn, m_Opponent, m_GameBoard, i_StartingLocation, i_TargetLocation);
            bool isPerformeMove = newMove.IsPreformedMove();

            if ((!(m_IsNeedToEatAgain = newMove.IsNeedToEatAgain) && isPerformeMove) || newMove.IsPieceBecomeKing)
            {
                m_CurrentTurnIndex++;
            }

            m_IsTie = isTieCondition();
            m_IsWin = isPlayerWin();

            return isPerformeMove;
        }

        public string GetCellPieceType(int i_Row, int i_Col)
        {
            string pieceType;
            
            if(m_GameBoard.GetCell(i_Row, i_Col).Piece != null)
            {
                pieceType = m_GameBoard.GetCell(i_Row, i_Col).Piece.PieceType.ToString();
            }
            else
            {
                pieceType = k_EmptyCell;
            }

            return pieceType;
        }

        public string GetPlayerPieceType()
        {
            updatePlayersMembers();

            return m_PlayerTurn.PawnType.ToString();
        }
    }
}