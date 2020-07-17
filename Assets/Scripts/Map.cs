using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public MapControl mapControl;
    public static Map instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        mapControl.Setup_Map();
    }
}
