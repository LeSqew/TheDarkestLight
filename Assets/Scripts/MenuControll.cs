using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControll : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("1 world");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
