using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject panel;

    public void OpenToPanel()
    {
        panel.SetActive(true);
    }

    public void CloseToPanel()
    {
        panel.SetActive(false);
    }
}
