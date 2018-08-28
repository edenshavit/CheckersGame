using System.Collections.Generic;

namespace CheckersLogic
{
    internal class CheckersPiece
    {
        // Nested enum
        internal enum ePieceType
        {
            X,
            O,
            K,
            U
        }

        private readonly List<int[]> r_PossibleSimpleMoves;
        private readonly List<int[]> r_PossibleEatMoves;
        private ePieceType m_PieceType;
        private int[] m_Location;

        internal CheckersPiece(ePieceType i_PieceType, int[] i_Location)
        {
            m_PieceType = i_PieceType;
            r_PossibleSimpleMoves = new List<int[]>();
            r_PossibleEatMoves = new List<int[]>();
            m_Location = i_Location;
        }

        internal ePieceType PieceType
        {
            get
            {
                return m_PieceType;
            }

            set
            {
                m_PieceType = value;
            }
        }

        internal List<int[]> PossibleSimpleMoves
        {
            get
            {
                return r_PossibleSimpleMoves;
            }
        }

        internal List<int[]> PossibleEatMoves
        {
            get
            {
                return r_PossibleEatMoves;
            }
        }

        internal int[] Location
        {
            get
            {
                return m_Location;
            }

            set
            {
                m_Location = value;
            }
        }
    }
}
