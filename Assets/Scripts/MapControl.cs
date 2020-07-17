using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapControl
{
    public GameObject point_prefab;
    public Transform parent;
    public ChangeType changeType;

    public void Setup_Map()
    {
        Map.instance.board.Init(point_prefab, parent);
    }
}
