using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flash : MonoBehaviour
{
    public GameObject[] lights;

    bool is_f_down;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            is_f_down = !is_f_down;
        }
        if (is_f_down)
        {
            lights[0].SetActive(true);
            lights[1].SetActive(true);
        }
        else
        {
            lights[0].SetActive(false);
            lights[1].SetActive(false);
        }
    }
}
