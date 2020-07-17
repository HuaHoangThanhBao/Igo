using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBot: Board
{
    public int evaluate()
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == player)
                    return +10;
                else return -10;
            }
        }
        // Else if none of them have won then return 0 
        return 0;
    }

    public int minimax(MapEdge[,] board, int depth, bool isMax)
    {
        int score = evaluate();

        // If Maximizer has won the game  
        // return his/her evaluated score 
        if (score == 10)
            return score;

        // If Minimizer has won the game  
        // return his/her evaluated score 
        if (score == -10)
            return score;

        // If there are no more moves and  
        // no winner then it is a tie 
        if (isFull())
            return 0;

        // If this maximizer's move 
        if (isMax)
        {
            int best = -1000;

            // Traverse all cells 
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    // Check if cell is empty 
                    if (board[i, j] == null)
                    {
                        // Make the move 
                        //board[i, j] = player;

                        // Call minimax recursively and choose 
                        // the maximum value 
                        best = Math.Max(best, minimax(board,
                                        depth + 1, !isMax));

                        // Undo the move 
                        board[i, j].placed = false;
                    }
                }
            }
            return best;
        }
        // If this minimizer's move 
        else
        {
            int best = 1000;

            // Traverse all cells 
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    // Check if cell is empty 
                    if (board[i, j] == null)
                    {
                        // Make the move 
                        //board[i, j] = opponent;

                        // Call minimax recursively and choose 
                        // the minimum value 
                        best = Math.Min(best, minimax(board,
                                        depth + 1, !isMax));

                        // Undo the move 
                        board[i, j].placed = false;
                    }
                }
            }
            return best;
        }
    }

    public Move findBestMove()
    {
        int bestVal = -1000;
        Move bestMove = new Move();
        bestMove.pos.x = -0.5f;
        bestMove.pos.y = 0.5f;
        bestMove.pos.z = -0.5f;

        // Traverse all cells, evaluate minimax function  
        // for all empty cells. And return the cell  
        // with optimal value. 
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                // Check if cell is empty 
                if (board[i, j].chess == null)
                {
                    // Make the move 
                    //board[i, j] = player;

                    // compute evaluation function for this 
                    // move. 
                    int moveVal = minimax(board, 0, false);

                    // Undo the move 
                    board[i, j].placed = false;

                    // If the value of the current move is 
                    // more than the best value, then update 
                    // best/ 
                    if (moveVal > bestVal)
                    {
                        bestMove.pos.x += i;
                        bestMove.pos.z += j;
                        bestVal = moveVal;
                    }
                }
            }
        }
        return bestMove;
    }
}
