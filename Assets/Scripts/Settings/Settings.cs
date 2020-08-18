using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private GameObject main_menu = default;
    [SerializeField]
    private GameObject level_selection_ui = default;
    [SerializeField]
    private GameObject settings_selection_ui = default;

    private GameObject level_sel_clone;
    private GameObject settings_sel_clone;

    public void PlayGameEvent()
    {
        SceneManager.LoadScene(1);
    }

    public void LevelSelectionEvent()
    {
        main_menu.SetActive(false);
        level_sel_clone = Instantiate(level_selection_ui, transform);
    }

    public void SettingSelectionEvent()
    {
        main_menu.SetActive(false);
        settings_sel_clone = Instantiate(settings_selection_ui, transform);
    }

    public void OnChooseSettingEvent()
    {
        main_menu.SetActive(true);
        Destroy(settings_sel_clone);
    }

    public void OnChooseLevelEvent()
    {
        main_menu.SetActive(true);
        Destroy(level_sel_clone);
    }
}
