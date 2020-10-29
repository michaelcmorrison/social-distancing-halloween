using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject rulesPanel;

    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void ToggleRulesPanel()
    {
        if (rulesPanel.activeSelf)
        {
            rulesPanel.SetActive(false);
        }
        else
        {
            rulesPanel.SetActive(true);
        }
    }
}
