using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public GameObject[] button_group;
    private Settings settings;

    private void Awake()
    {
        settings = FindObjectOfType<Settings>();
    }

    private void Start()
    {
        button_group = new GameObject[transform.GetChild(0).childCount];

        for (int i = 0; i < button_group.Length; i++)
        {
            button_group[i] = transform.GetChild(0).GetChild(i).gameObject;
            Select_Active(i);
            Attach_Button(button_group[i], i);
        }
    }

    private void Select_Active(int i)
    {
        if (SaveData.GetLevel() == i)
        {
            button_group[i].transform.GetChild(1).gameObject.SetActive(true);
        }
        else button_group[i].transform.GetChild(1).gameObject.SetActive(false);
    }

    private void Attach_Button(GameObject button, int index)
    {
        switch (index)
        {
            case 0:
                button.GetComponent<Button>().onClick.AddListener(delegate () { Normal(index); });
                break;
            case 1:
                button.GetComponent<Button>().onClick.AddListener(delegate () { Medium(index); });
                break;
            case 2:
                button.GetComponent<Button>().onClick.AddListener(delegate () { Hard(index); });
                break;
        }
    }

    private void Normal(int index)
    {
        SaveData.SaveLevel(index);
        settings.OnChooseLevelEvent();
    }

    private void Medium(int index)
    {
        SaveData.SaveLevel(index);
        settings.OnChooseLevelEvent();
    }

    private void Hard(int index)
    {
        SaveData.SaveLevel(index);
        settings.OnChooseLevelEvent();
    }
}
