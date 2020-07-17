using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ChangeType
{
    public Material black_mat;
    public Material white_mat;
    public List<MapEdge> next_edge_list;

    public void Update_Chess(MapEdge current)
    {
        Left_Right(current);
        Right_Left(current);
        Top(current);
        Down(current);
        Left_Right_Top(current);
        Left_Right_Down(current);
        Right_Left_Down(current);
        Right_Left_Top(current);
    }

    private void Left_Right(MapEdge current)
    {
        next_edge_list = new List<MapEdge>();
        int i = 1;
        while(current.pos.x + i <= 7.5f)
        {
            Vector3 next = new Vector3(current.pos.x + i, current.pos.y, current.pos.z);
            if (HoverClick.instance.bot.placed_pos.Any(m => m.pos == next))
            {
                MapEdge next_edge = HoverClick.instance.bot.placed_pos.Where(m => m.pos == next).Single();
                if (next_edge != null)
                {
                    if (next_edge.placed)
                    {
                        next_edge_list.Add(next_edge);
                        if (next_edge.isBlack == current.isBlack)
                        {
                            if (current.isBlack)
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = black_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            else
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = white_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else break;
            i += 1;
        }
    }

    private void Right_Left(MapEdge current)
    {
        next_edge_list = new List<MapEdge>();
        int i = 1;
        while (current.pos.x - i >= -7.5f)
        {
            Vector3 next = new Vector3(current.pos.x - i, current.pos.y, current.pos.z);
            if (HoverClick.instance.bot.placed_pos.Any(m => m.pos == next))
            {
                MapEdge next_edge = HoverClick.instance.bot.placed_pos.Where(m => m.pos == next).Single();
                if (next_edge != null)
                {
                    if (next_edge.placed)
                    {
                        next_edge_list.Add(next_edge);
                        if (next_edge.isBlack == current.isBlack)
                        {
                            if (current.isBlack)
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = black_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            else
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = white_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else break;
            i += 1;
        }
    }

    private void Top(MapEdge current)
    {
        next_edge_list = new List<MapEdge>();
        int i = 1;
        while (current.pos.y + i <= 7.5f)
        {
            Vector3 next = new Vector3(current.pos.x, current.pos.y, current.pos.z + i);
            if (HoverClick.instance.bot.placed_pos.Any(m => m.pos == next))
            {
                MapEdge next_edge = HoverClick.instance.bot.placed_pos.Where(m => m.pos == next).Single();
                if (next_edge != null)
                {
                    if (next_edge.placed)
                    {
                        next_edge_list.Add(next_edge);
                        if (next_edge.isBlack == current.isBlack)
                        {
                            if (current.isBlack)
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = black_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            else
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = white_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else break;
            i += 1;
        }
    }

    private void Down(MapEdge current)
    {
        next_edge_list = new List<MapEdge>();
        int i = 1;
        while (current.pos.y + i <= 7.5f)
        {
            Vector3 next = new Vector3(current.pos.x, current.pos.y, current.pos.z - i);
            if (HoverClick.instance.bot.placed_pos.Any(m => m.pos == next))
            {
                MapEdge next_edge = HoverClick.instance.bot.placed_pos.Where(m => m.pos == next).Single();
                if (next_edge != null)
                {
                    if (next_edge.placed)
                    {
                        next_edge_list.Add(next_edge);
                        if (next_edge.isBlack == current.isBlack)
                        {
                            if (current.isBlack)
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = black_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            else
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = white_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else break;
            i += 1;
        }
    }

    private void Left_Right_Top(MapEdge current)
    {
        next_edge_list = new List<MapEdge>();
        int i = 1;
        while (current.pos.x + i <= 7.5f && current.pos.z + i <= 7.5)
        {
            Vector3 next = new Vector3(current.pos.x + i, current.pos.y, current.pos.z + i);
            if (HoverClick.instance.bot.placed_pos.Any(m => m.pos == next))
            {
                MapEdge next_edge = HoverClick.instance.bot.placed_pos.Where(m => m.pos == next).Single();
                if (next_edge != null)
                {
                    if (next_edge.placed)
                    {
                        next_edge_list.Add(next_edge);
                        if (next_edge.isBlack == current.isBlack)
                        {
                            if (current.isBlack)
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = black_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            else
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = white_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else break;
            i += 1;
        }
    }

    private void Left_Right_Down(MapEdge current)
    {
        next_edge_list = new List<MapEdge>();
        int i = 1;
        while (current.pos.x + i <= 7.5f && current.pos.z - i >= -7.5f)
        {
            Vector3 next = new Vector3(current.pos.x + i, current.pos.y, current.pos.z - i);
            if (HoverClick.instance.bot.placed_pos.Any(m => m.pos == next))
            {
                MapEdge next_edge = HoverClick.instance.bot.placed_pos.Where(m => m.pos == next).Single();
                if (next_edge != null)
                {
                    if (next_edge.placed)
                    {
                        next_edge_list.Add(next_edge);
                        if (next_edge.isBlack == current.isBlack)
                        {
                            if (current.isBlack)
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = black_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            else
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = white_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else break;
            i += 1;
        }
    }

    private void Right_Left_Down(MapEdge current)
    {
        next_edge_list = new List<MapEdge>();
        int i = 1;
        while (current.pos.x - i >= -7.5f && current.pos.z - i >= -7.5f)
        {
            Vector3 next = new Vector3(current.pos.x - i, current.pos.y, current.pos.z - i);
            if (HoverClick.instance.bot.placed_pos.Any(m => m.pos == next))
            {
                MapEdge next_edge = HoverClick.instance.bot.placed_pos.Where(m => m.pos == next).Single();
                if (next_edge != null)
                {
                    if (next_edge.placed)
                    {
                        next_edge_list.Add(next_edge);
                        if (next_edge.isBlack == current.isBlack)
                        {
                            if (current.isBlack)
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = black_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            else
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = white_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else break;
            i += 1;
        }
    }

    private void Right_Left_Top(MapEdge current)
    {
        next_edge_list = new List<MapEdge>();
        int i = 1;
        while (current.pos.x - i >= -7.5f && current.pos.z + i <= 7.5)
        {
            Vector3 next = new Vector3(current.pos.x - i, current.pos.y, current.pos.z + i);
            if (HoverClick.instance.bot.placed_pos.Any(m => m.pos == next))
            {
                MapEdge next_edge = HoverClick.instance.bot.placed_pos.Where(m => m.pos == next).Single();
                if (next_edge != null)
                {
                    if (next_edge.placed)
                    {
                        next_edge_list.Add(next_edge);
                        if (next_edge.isBlack == current.isBlack)
                        {
                            if (current.isBlack)
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = black_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            else
                            {
                                foreach (var item in next_edge_list)
                                {
                                    item.chess.GetComponent<MeshRenderer>().material = white_mat;
                                    item.isBlack = current.isBlack;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else break;
            i += 1;
        }
    }
}
