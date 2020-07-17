using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdge: MonoBehaviour
{
    public Vector3 pos;
    public bool placed;
    public bool isBlack;
    public GameObject chess;

    private void Start()
    {
        pos = transform.position;
    }
}
