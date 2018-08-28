using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CheckersLogic;

namespace CheckersWindowsUI
{
    public partial class GameForm : Form
    {
        // Nested enum
        public enum eBoardSize
        {
            x6 = 6,
            x8 = 8,
            x10 = 10
        }

        private const int k_InitialBoardTop = 60;
        private readonly List<BoardSquare> r_CheckersBoard;
        private readonly CheckersGame r_CheckersLogic;
        private readonly bool r_IsPlayer2Mode;
        private readonly string r_Player1Name;
        private readonly string r_Player2Name;
        private readonly int r_BoardSize;
        private readonly Timer r_Timer;
        private BoardSquare boardSquareActive;
        private Label labelCurrentPlayer;
        private Label labelPlayer1;
        private Label labelPlayer2;
        private Button buttonQuit;

        public GameForm(int i_BoardSize, bool i_IsPlayer2Mode, string i_Player1Name, string i_Player2Name)
        {
            r_BoardSize = i_BoardSize;
            r_CheckersLogic = new CheckersGame(r_BoardSize);
            r_IsPlayer2Mode = i_IsPlayer2Mode;
            r_Player1Name = i_Player1Name;
            r_Player2Name = i_Player2Name;
            r_CheckersBoard = new List<BoardSquare>(r_BoardSize * r_BoardSize);
            this.BackColor = Color.FromArgb(40, 79, 79);
            this.boardSquareActive = null;
            r_CheckersLogic.StartGame(r_Player1Name, r_Player2Name);
            createGame();

            r_Timer = new Timer();
            initializeTimer();

            InitializeComponent();
        }

        private void initializeTimer()
        {
            r_Timer.Interval = 450;
            r_Timer.Tick += new EventHandler(timer_Tick);
        }

        private void createGame()
        {
            buildCheckersBoard();
            initializeUserScreen();
        }

        private void initializeUserScreen()
        {
            this.Size = new Size((r_CheckersBoard[0].Width * r_BoardSize) + 80, (r_CheckersBoard[0].Height * r_BoardSize) + 180);

            initializePlayer1Label();
            initializePlayer2Label();
            initializeQuitButton();
            initializeCurrentPlayerLabel();    
        }

        private void initializePlayer1Label()
        {
            string textLabelPlayer1 = string.Format("{0}: {1}", r_CheckersLogic.Player1Name, r_CheckersLogic.Player1Score.ToString());
            labelPlayer1 = new Label();
            labelPlayer1.Text = textLabelPlayer1;
            labelPlayer1.Font = new Font("Consolas", 10, FontStyle.Bold);
            labelPlayer1.AutoSize = true;
            labelPlayer1.ForeColor = Color.WhiteSmoke;
            labelPlayer1.BackColor = Color.Transparent;
            labelPlayer1.TextAlign = ContentAlignment.MiddleLeft;
            labelPlayer1.Top = r_CheckersBoard[0].Bounds.Top - labelPlayer1.Height - 10;
            labelPlayer1.Left = r_CheckersBoard[0].Bounds.Left;

            this.Controls.Add(labelPlayer1);
        }

        private void initializeQuitButton()
        {
            buttonQuit = new Button();
            buttonQuit.Text = "Quit";
            buttonQuit.ForeColor = Color.Black;
            buttonQuit.AutoSize = true;
            buttonQuit.Click += quitButton_Click;
            buttonQuit.TextAlign = ContentAlignment.MiddleCenter;
            buttonQuit.BackColor = Color.LightGray;
            buttonQuit.FlatStyle = FlatStyle.Popup;
            buttonQuit.Top = r_CheckersBoard[(r_BoardSize * r_BoardSize) - r_BoardSize - 1].Bottom + 80;
            buttonQuit.Left = r_CheckersBoard[r_BoardSize - 1].Right - buttonQuit.Size.Width;

            this.Controls.Add(buttonQuit);
        }

        private void initializePlayer2Label()
        {
            string textLabelPlayer2 = string.Format("{0}: {1}", r_CheckersLogic.Player2Name, r_CheckersLogic.Player2Score);
            labelPlayer2 = new Label();
            labelPlayer2.Text = textLabelPlayer2;
            labelPlayer2.Font = new Font("Consolas", 10, FontStyle.Bold);
            labelPlayer2.ForeColor = Color.WhiteSmoke;
            labelPlayer2.AutoSize = true;
            labelPlayer2.BackColor = Color.Transparent;
            labelPlayer2.TextAlign = ContentAlignment.MiddleLeft;
            labelPlayer2.Top = r_CheckersBoard[r_BoardSize - 2].Bounds.Top - labelPlayer2.Height - 10;
            labelPlayer2.Left = r_CheckersBoard[r_BoardSize - 2].Bounds.Left;

            this.Controls.Add(labelPlayer2);
        }

        private void initializeCurrentPlayerLabel()
        {
            string textLabelCurrentPlayer = string.Format("{0}'s Turn ({1})", r_CheckersLogic.PlayerTurnName, getPlayerColor());
            labelCurrentPlayer = new Label();
            labelCurrentPlayer.Font = new Font("Consolas", 10, FontStyle.Bold);
            labelCurrentPlayer.ForeColor = Color.WhiteSmoke;
            labelCurrentPlayer.AutoSize = true;
            labelCurrentPlayer.BackColor = Color.Transparent;
            labelCurrentPlayer.TextAlign = ContentAlignment.MiddleLeft;
            labelCurrentPlayer.Text = textLabelCurrentPlayer;
            labelCurrentPlayer.Top = r_CheckersBoard[(r_BoardSize * r_BoardSize) - r_BoardSize - 1].Bottom + 80;
            labelCurrentPlayer.Left = r_CheckersBoard[(r_BoardSize * r_BoardSize) - r_BoardSize].Bounds.Left;

            this.Controls.Add(labelCurrentPlayer);
        }

        private string getPlayerColor()
        {
            string playerPieceType = r_CheckersLogic.GetPlayerPieceType();

            if(playerPieceType.Equals("O") || playerPieceType.Equals("U"))
            {
                playerPieceType = "White";
            }
            else
            {
                playerPieceType = "Black";
            }

            return playerPieceType;
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            if (r_CheckersLogic.IsPlayerQuit)
            {
                handleQuit();
            }
            else
            {
                MessageBox.Show("Current player cannot Quit!", this.Text);
            }
        }

        private void handleQuit()
        {
            if (playerWonMessageBox(r_CheckersLogic.OpponentName))
            {
                initializeNewRound();
            }
            else
            {
                this.Close();
            }
        }

        private void buildCheckersBoard()
        {
            initializeBoard();
            placeSquaresOnBoard();
        }

        private void initializeBoard()
        {
            initializeEmptyBoardSquares();

            bool v_SquareActive = true;
            int halfBoardSize = r_BoardSize / 2;
            BoardSquare boardSquar;

            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    if ((row >= halfBoardSize + 1) && ((row % 2 == 0 && col % 2 == 1) || (row % 2 == 1 && col % 2 == 0)))
                    {
                        r_CheckersBoard[(row * r_BoardSize) + col].Visible = false;
                        boardSquar = new BoardSquare(BoardSquare.ePieceType.BlackPawn, v_SquareActive, row, col);
                        r_CheckersBoard[(row * r_BoardSize) + col] = boardSquar;
                        this.Controls.Add(r_CheckersBoard[(row * r_BoardSize) + col]);
                        addSquareToClick(row, col);
                    }
                    else if ((row < halfBoardSize - 1) && ((row % 2 == 0 && col % 2 == 1) || (row % 2 == 1 && col % 2 == 0)))
                    {
                        r_CheckersBoard[(row * r_BoardSize) + col].Visible = false;
                        boardSquar = new BoardSquare(BoardSquare.ePieceType.WhitePawn, v_SquareActive, row, col);
                        r_CheckersBoard[(row * r_BoardSize) + col] = boardSquar;
                        this.Controls.Add(r_CheckersBoard[(row * r_BoardSize) + col]);
                        addSquareToClick(row, col);
                    }
                }
            }
        }

        private void initializeEmptyBoardSquares()
        {
            bool v_SquareActive = true;
            int halfBoardSize = r_BoardSize / 2;
            BoardSquare boardSquar;

            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    if ((row % 2 == 0 && col % 2 == 0) || (row % 2 == 1 && col % 2 == 1))
                    {
                        boardSquar = new BoardSquare(BoardSquare.ePieceType.Empty, !v_SquareActive, row, col);
                        r_CheckersBoard.Add(boardSquar);
                        this.Controls.Add(r_CheckersBoard[(row * r_BoardSize) + col]);
                    }
                    else
                    {
                        boardSquar = new BoardSquare(BoardSquare.ePieceType.Empty, v_SquareActive, row, col);
                        r_CheckersBoard.Add(boardSquar);
                        this.Controls.Add(r_CheckersBoard[(row * r_BoardSize) + col]);
                    }

                    addSquareToClick(row, col);
                }
            }
        }

        private void addSquareToClick(int i_SqureRow, int i_SquareCol)
        {
            if (r_CheckersBoard[(i_SqureRow * r_BoardSize) + i_SquareCol] != null)
            {
                r_CheckersBoard[(i_SqureRow * r_BoardSize) + i_SquareCol].Click += new EventHandler(boardSquare_Clicked);
            }
        }

        private void placeSquaresOnBoard()
        {
            int squareHeight = r_CheckersBoard[0].Height;
            int squareWidth = r_CheckersBoard[0].Width;
            int cuurentSquareTop = k_InitialBoardTop;

            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    r_CheckersBoard[(row * r_BoardSize) + col].Top = cuurentSquareTop;
                    r_CheckersBoard[(row * r_BoardSize) + col].Left = squareHeight + (col * squareWidth) - 20;
                }

                cuurentSquareTop = cuurentSquareTop + squareHeight;
            }
        }

        private void boardSquare_Clicked(object sender, EventArgs e)
        {
            BoardSquare clickedSquare = sender as BoardSquare;

            if (clickedSquare != null && clickedSquare.Enabled)
            {
                if (boardSquareActive == null && clickedSquare.SquarePieceType != BoardSquare.ePieceType.Empty)
                { 
                    clickedSquare.SquareInAction();
                    boardSquareActive = clickedSquare;
                }
                else if (boardSquareActive != null)
                {
                    if (clickedSquare == boardSquareActive)
                    {
                        clickedSquare.ReturnSquareToBeActive();
                        boardSquareActive = null;
                    }
                    else
                    {
                        makeTurn(clickedSquare);
                    }
                }
            }
        }

        private void makeTurn(BoardSquare i_ClickedSquare)
        {
            humanTurn(i_ClickedSquare);

            if (!r_IsPlayer2Mode)
            {
                r_Timer.Start();
            }

            updateGraphics();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (r_CheckersLogic.PlayerTurnName.Equals("Computer"))
            {  
                computerTurn();

                if (!r_CheckersLogic.IsNeedToEatAgain)
                {
                    r_Timer.Stop();
                }

                updateGraphics();
            }
        }

        private void humanTurn(BoardSquare i_ClickedSquare)
        {
            if (checkIsValidTurnAndMove(i_ClickedSquare.SquareRow, i_ClickedSquare.SquareCol) && isItEndOfGame())
            {
                handleEndOfGame();
            }

            updateCurrentTurnLabel();
        }

        private void computerTurn()
        {
            r_CheckersLogic.PerformComputerMove();

            if (isItEndOfGame())
            {
                handleEndOfGame();
            }

            updateCurrentTurnLabel();
        }

        private void handleEndOfGame()
        {
            r_CheckersLogic.UpdateEndOfCurrentGame();
            exitMessage();
        }

        private bool checkIsValidTurnAndMove(int i_RowDestination, int i_ColomnDestination)
        {
            int[] startLocation = new int[] { boardSquareActive.SquareRow, boardSquareActive.SquareCol };
            int[] destinationLocation = new int[] { i_RowDestination, i_ColomnDestination };

            bool isValidMove = r_CheckersLogic.IsPerformedHumanMove(startLocation, destinationLocation);

            if (!isValidMove)
            {
                inValidMessageBox();
            }

            return isValidMove;
        }

        private void inValidMessageBox()
        {
            string messageBox = string.Format(
                @"Invalid move!
Please try again.");
            MessageBox.Show(messageBox, this.Text);
            boardSquareActive.ReturnSquareToBeActive();
            boardSquareActive = null;
        }

        private bool isItEndOfGame()
        {
            return r_CheckersLogic.IsTie || r_CheckersLogic.IsWin;
        }

        private void exitMessage()
        {
            bool isAnotherRound = false;

            if (r_CheckersLogic.IsTie)
            {
                isAnotherRound = tieMessageBox();
            }
            else if (r_CheckersLogic.IsWin)
            {
                isAnotherRound = playerWonMessageBox(r_CheckersLogic.WinnerName);
            }

            if (isAnotherRound)
            {
                initializeNewRound();
            }
            else
            {
                this.Close();
            }
        }

        private void updateCurrentTurnLabel()
        {
            string textLabelCurrentPlayer = string.Format("{0}'s Turn ({1})", r_CheckersLogic.PlayerTurnName, getPlayerColor());

            labelCurrentPlayer.Text = textLabelCurrentPlayer;
        }

        private bool playerWonMessageBox(string i_WinnerName)
        {
            updateGraphics();
            bool isAnotherRound = false;
            string messageBox = string.Format(
                @"{0} Won!
Another Round?", 
                i_WinnerName);

            if (MessageBox.Show(messageBox, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                isAnotherRound = true;
            }

            return isAnotherRound;
        }

        private bool tieMessageBox()
        {
            updateGraphics();
            bool isAnotherRound = false;
            string messageBox = string.Format(
    @"Tie!
Another Round?");

            if (MessageBox.Show(messageBox, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                isAnotherRound = true;
            }

            return isAnotherRound;
        }

        private void updateScoreLabels()
        {
            updateScoreLabelPlayer1();
            updateScoreLabelPlayer2();
        }

        private void updateScoreLabelPlayer1()
        {
            string textLabelPlayer1 = string.Format("{0}: {1}", r_CheckersLogic.Player1Name, r_CheckersLogic.Player1Score);

            labelPlayer1.Text = textLabelPlayer1.ToString();
        }

        private void updateScoreLabelPlayer2()
        {
            string textLabelPlayer2 = string.Format("{0}: {1}", r_CheckersLogic.Player2Name, r_CheckersLogic.Player2Score);

            labelPlayer2.Text = textLabelPlayer2.ToString();
        }

        private void initializeNewRound()
        {
            updateScoreLabels();
            r_CheckersLogic.InitializeGame();
            updateGraphics();
        }

        private void updateGraphics()
        {
            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    if (r_CheckersLogic.GetCellPieceType(row, col).Equals("O"))
                    {
                        r_CheckersBoard[(row * r_BoardSize) + col].SetImageInSquare(BoardSquare.ePieceType.WhitePawn);
                    }
                    else if (r_CheckersLogic.GetCellPieceType(row, col).Equals("U"))
                    {
                        r_CheckersBoard[(row * r_BoardSize) + col].SetImageInSquare(BoardSquare.ePieceType.WhiteKing);
                    }
                    else if (r_CheckersLogic.GetCellPieceType(row, col).Equals("X"))
                    {
                        r_CheckersBoard[(row * r_BoardSize) + col].SetImageInSquare(BoardSquare.ePieceType.BlackPawn);
                    }
                    else if (r_CheckersLogic.GetCellPieceType(row, col).Equals("K"))
                    {
                        r_CheckersBoard[(row * r_BoardSize) + col].SetImageInSquare(BoardSquare.ePieceType.BlackKing);
                    }
                    else
                    {
                        r_CheckersBoard[(row * r_BoardSize) + col].SetImageInSquare(BoardSquare.ePieceType.Empty);
                    }
                }
            }

            if (boardSquareActive != null)
            {
                boardSquareActive.ReturnSquareToBeActive();
                boardSquareActive = null;
            }
        }
    }
}
