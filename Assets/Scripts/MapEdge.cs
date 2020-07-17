using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdge: MonoBehaviour
{
    public Vector3 pos;
    public bool placed;
    public bool isBlack;
    public GameObject chess;

    public void SetPos(Vector3 dir_pos)
    {
        pos = dir_pos;
        transform.position = dir_pos;
    }
}
