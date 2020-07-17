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
        float x = -0.5f;
        float y = 0.5f;
        float z = -0.5f;

        int i = 0;//tăng số dòng
        int col = 0;//cột
        int row = 0;//dòng

        while (col < 9)
        {
            GameObject point = MonoBehaviour.Instantiate(point_prefab, parent);
            point.AddComponent<MapEdge>();
            point.transform.localPosition = new Vector3(x + row, y, z + i);
            i++;
            if(i == 9)
            {
                col++;
                i = 0;
                row += 1;
            }
        }
    }
}
