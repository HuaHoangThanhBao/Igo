using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public AIBot board;
    public MapControl mapControl;
    public static Map instance;

    private void Awake()
    {
        instance = this;
        board = new AIBot();
    }

    private void Start()
    {
        mapControl.Setup_Map();
    }
}
