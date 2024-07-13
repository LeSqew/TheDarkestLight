using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool GameIsPause;
    public GameObject UI_Game;
    public GameObject UI_Pause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameIsPause)
            {
                PauseGame();
            }
            else { ContinueGame(); }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        UI_Game.SetActive(false);
        UI_Pause.SetActive(true);
        GameIsPause = true;
        Debug.Log("1");
    }
    public void ContinueGame()
    {
        Debug.Log("2");
        Time.timeScale = 1f;
        UI_Game.SetActive(true);
        UI_Pause.SetActive(false);
        GameIsPause = false;
    }
    public void BackMenu()
    {
        Debug.Log("3");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
