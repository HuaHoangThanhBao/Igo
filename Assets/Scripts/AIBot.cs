using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIBot
{
    public List<MapEdge> placed_pos = new List<MapEdge>();

    public void AddNewPos(MapEdge pos)
    {
        placed_pos.Add(pos);
    }

    public Vector3 Auto(Vector3 pos)
    {
        return new Vector3(0.5f, 0.5f, -0.5f);
    }
}
