using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game_manager : MonoBehaviour
{
    //public static game_manager manager;

    public GameObject menu;

    public bool is_menu_show = true;
    private void Update()
    {
        show_menu();
        if (is_menu_show)
        {
            menu.SetActive(true);
        }
        else
        {
            menu.SetActive(false);
        }
    }
    public void show_menu()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            is_menu_show = !is_menu_show;
        }
    }
    public void show_around()
    {
        is_menu_show = !is_menu_show;
    }
    public void exit_game()
    {
        Application.Quit();
    }
}
