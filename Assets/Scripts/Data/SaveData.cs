using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public static void SaveLevel(int level)
    {
        PlayerPrefs.SetInt("level", level);
    }

    public static int GetLevel()
    {
        return PlayerPrefs.GetInt("level");
    }

    public static void SaveSound(int state)
    {
        PlayerPrefs.SetInt("sound", state);
    }

    public static int GetSound()
    {
        return PlayerPrefs.GetInt("sound");
    }
}
