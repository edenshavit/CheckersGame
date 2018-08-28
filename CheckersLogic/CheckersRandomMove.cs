using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckersLogic
{
    internal class CheckersRandomMove
    {
        private Random m_Random = new Random();
        private CheckersMove m_CurrentMove;

        internal List<int> ChooseRandomMove(CheckersPlayer i_Player, CheckersPlayer i_Opponent, CheckersBoard i_GameBoard)
        {
            m_CurrentMove = new CheckersMove(i_Player, i_Opponent, i_GameBoard);

            m_CurrentMove.UpdateAllPlayerPieces(i_Player);

            List<int> choosenMove = null;
            bool mustEat = m_CurrentMove.IsPlayerMustEat(i_Player);

            List<CheckersPiece> playerPieces = i_Player.PlayerPieces;
            shuffle(playerPieces);

            foreach (CheckersPiece piece in playerPieces)
            {
                int randomLocation;
                choosenMove = new List<int> { piece.Location[0], piece.Location[1] };

                if (mustEat)
                {
                    if (piece.PossibleEatMoves.Any())
                    {
                        randomLocation = m_Random.Next(piece.PossibleEatMoves.Count);
                        choosenMove.Add(piece.PossibleEatMoves[randomLocation][0]);
                        choosenMove.Add(piece.PossibleEatMoves[randomLocation][1]);
                        break;
                    }
                }
                else if (piece.PossibleSimpleMoves.Any())
                {
                    randomLocation = m_Random.Next(piece.PossibleSimpleMoves.Count);
                    choosenMove.Add(piece.PossibleSimpleMoves[randomLocation][0]);
                    choosenMove.Add(piece.PossibleSimpleMoves[randomLocation][1]);
                    break;
                }
            }

            return choosenMove;
        }

        private void shuffle<T>(List<T> i_List)
        {
            int counter = i_List.Count;
            while (counter > 1)
            {
                counter--;
                int randomIndex = m_Random.Next(counter + 1);
                T value = i_List[randomIndex];
                i_List[randomIndex] = i_List[counter];
                i_List[counter] = value;
            }
        }
    }
}
