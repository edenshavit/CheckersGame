using System;
using System.Collections.Generic;

namespace CheckersLogic
{
    internal class CheckersPlayer
    {
        private string m_Name;
        private List<CheckersPiece> m_PlayerPieces;
        private CheckersPiece.ePieceType m_PawnType;
        private CheckersPiece.ePieceType m_KingType;
        private bool m_IsLegalMovesLeft;
        private int m_ScoreInCurrentGame;
        private int m_TotalScore;
        private bool m_IsWin = false;

        internal CheckersPlayer(string i_PlayerName, CheckersPiece.ePieceType i_PawnType, CheckersPiece.ePieceType i_KingType)
        {
            m_Name = i_PlayerName;
            m_PawnType = i_PawnType;
            m_KingType = i_KingType;
            m_IsLegalMovesLeft = true;
        }

        internal string Name
        {
            get
            {
                return m_Name;
            }
        }

        internal List<CheckersPiece> PlayerPieces
        {
            get
            {
                return m_PlayerPieces;
            }

            set
            {
                m_PlayerPieces = value;
            }
        }

        internal CheckersPiece.ePieceType PawnType
        {
            get
            {
                return m_PawnType;
            }
        }

        internal CheckersPiece.ePieceType KingType
        {
            get
            {
                return m_KingType;
            }
        }

        internal bool IsLegalMovesLeft
        {
            get
            {
                return m_IsLegalMovesLeft;
            }

            set
            {
                m_IsLegalMovesLeft = value;
            }
        }

        internal int ScoreInCurrentGame
        {
            get
            {
                return m_ScoreInCurrentGame;
            }

            set
            {
                m_ScoreInCurrentGame = value;
            }
        }

        internal int TotalScore
        {
            get
            {
                return m_TotalScore;
            }

            set
            {
                m_TotalScore = value;
            }
        }

        internal bool IsWin
        {
            get
            {
                return m_IsWin;
            }

            set
            {
                m_IsWin = value;
            }
        }

        internal bool IsValidPointsDifference(CheckersPlayer i_Opponent)
        {
            int sumOfPlayerPoints = this.GetPlayerPoints();
            int sumOfOpponentPoints = i_Opponent.GetPlayerPoints();
            bool isValidPointsForQuit = (sumOfPlayerPoints - sumOfOpponentPoints) <= 0;

            return isValidPointsForQuit;
        }

        internal int GetPlayerPoints()
        {
            int sumOfPlayerPoints = 0;

            foreach (CheckersPiece piece in this.m_PlayerPieces)
            {
                if (piece.PieceType.Equals(this.m_KingType))
                {
                    sumOfPlayerPoints += 4;
                }
                else
                {
                    sumOfPlayerPoints++;
                }
            }

            return sumOfPlayerPoints;
        }

        internal void UpdatePlayerScore()
        {
            this.m_TotalScore = this.m_TotalScore + this.m_ScoreInCurrentGame;
            this.m_ScoreInCurrentGame = 0;
        }
    }
}
