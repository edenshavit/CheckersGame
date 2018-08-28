using System.Windows.Forms;
using System.Drawing;
using CheckerWindowsUI.Properties;

namespace CheckersWindowsUI
{
    public class BoardSquare : PictureBox
    {
        public enum ePieceType
        {
            BlackPawn,
            BlackKing,
            WhitePawn,
            WhiteKing,
            Empty
        }

        private readonly int r_SquareRow;
        private readonly int r_SquareCol;
        private ePieceType m_PieceType;

        public BoardSquare(ePieceType i_PieceType, bool i_IsSquareActive, int i_SquareRow, int i_SquareCol)
        {
            this.Width = 50;
            this.Height = 50;
            this.r_SquareRow = i_SquareRow;
            this.r_SquareCol = i_SquareCol;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.m_PieceType = i_PieceType;

            initializeBoardSquare(i_IsSquareActive);
        }

        private void initializeBoardSquare(bool i_IsSquareActive)
        {
            if (i_IsSquareActive)
            {
                SetImageInSquare(m_PieceType);
                this.BackColor = Color.WhiteSmoke;
                this.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                this.BackColor = Color.DarkGray;
                this.Enabled = false;
            }
        }

        public ePieceType SquarePieceType
        {
            get
            {
                return m_PieceType;
            }
        }

        public int SquareRow
        {
            get
            {
                return r_SquareRow;
            }
        }

        public int SquareCol
        {
            get
            {
                return r_SquareCol;
            }
        }

        public void SquareInAction()
        {
            this.BackColor = Color.FromArgb(176, 224, 230);
        }

        public void ReturnSquareToBeActive()
        {
            this.BackColor = Color.WhiteSmoke;
        }

        public void SetImageInSquare(ePieceType i_BoardSquareType)
        {
            if (i_BoardSquareType.Equals(ePieceType.BlackPawn))
            {
                this.m_PieceType = ePieceType.BlackPawn;
                this.Image = Resources.black_man;
            }
            else if (i_BoardSquareType.Equals(ePieceType.BlackKing))
            {
                this.m_PieceType = ePieceType.BlackKing;
                this.Image = Resources.black_cro;
            }
            else if (i_BoardSquareType.Equals(ePieceType.WhitePawn))
            {
                this.m_PieceType = ePieceType.WhitePawn;
                this.Image = Resources.white_man;
            }
            else if (i_BoardSquareType.Equals(ePieceType.WhiteKing))
            {
                this.m_PieceType = ePieceType.WhiteKing;
                this.Image = Resources.white_cro;
            }
            else if (i_BoardSquareType.Equals(ePieceType.Empty))
            {
                this.m_PieceType = ePieceType.Empty;
                if (this.Image != null)
                {
                    this.Image.Dispose();
                    this.Image = null;
                }
            }
        }
    }
}
