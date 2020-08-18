using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSelection : MonoBehaviour
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
        if (SaveData.GetSound() == i)
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
                button.GetComponent<Button>().onClick.AddListener(delegate () { On(index); });
                break;
            case 1:
                button.GetComponent<Button>().onClick.AddListener(delegate () { Off(index); });
                break;
        }
    }

    private void On(int index)
    {
        SaveData.SaveSound(index);
        settings.OnChooseSettingEvent();
    }

    private void Off(int index)
    {
        SaveData.SaveSound(index);
        settings.OnChooseSettingEvent();
    }
}
