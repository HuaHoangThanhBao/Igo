using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public Vector3 pos;
}

public class Board
{
    public MapEdge[,] board;
    public MapEdge player;
    public MapEdge opponent;

    public void Init(GameObject point_prefab, Transform parent)
    {
        board = new MapEdge[9, 9];

        float x = -0.5f;
        float y = 0.5f;
        float z = -0.5f;

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                GameObject point = MonoBehaviour.Instantiate(point_prefab, parent);
                Vector3 pos = new Vector3(x + i, y, z + j);
                point.GetComponent<MapEdge>().SetPos(pos);
                point.transform.localPosition = pos;
                board[i, j] = point.GetComponent<MapEdge>();
            }
        }
    }

    public void Placed_Chess(MapEdge current, GameObject clone, bool isBlack)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == current)
                {
                    board[i, j].chess = clone;
                    board[i, j].placed = true;
                    board[i, j].isBlack = isBlack;

                    if (isBlack)
                        player = current;
                    else
                        opponent = current;

                    break;
                }
            }
        }
    }

    public MapEdge Find_Chess(Vector3 pos)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j].pos == pos)
                    if (board[i, j].placed)
                        return board[i, j];
            }
        }
        return null;
    }

    public MapEdge Find_Pos(Vector3 pos)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j].pos == pos)
                    return board[i, j];
            }
        }
        return null;
    }

    public bool isFull()
    {
        int count = 0;

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j].placed)
                    count++;
            }
        }

        if (count == board.Length)
            return true;
        else return false;
    }

    public int GetLength()
    {
        return board.Length;
    }
}
