using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogChangeText : MonoBehaviour
{
    public List<string> texts;
    public int index = 0;
    public TMP_Text text;
    public GameObject panel;
    public void NextDialogue()
    {
        if (index >= texts.Count)
        {
            index = 0;
            panel.SetActive(false);
            text.text = texts[0];
            SceneManager.LoadScene("2 world");
        }
        else { text.text = texts[index]; }
        index++;
    }
    public void Start()
    {
        NextDialogue();
    }
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && panel.active)
        {
            NextDialogue();
        }
    }
}
