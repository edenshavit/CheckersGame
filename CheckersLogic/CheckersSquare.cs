namespace CheckersLogic
{
    internal class CheckersSquare
    {
        private int[] m_Location;
        private CheckersPiece m_Piece;

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

        internal CheckersPiece Piece
        {
            get
            {
                return m_Piece;
            }

            set
            {
                m_Piece = value;
            }
        }
    }
}
