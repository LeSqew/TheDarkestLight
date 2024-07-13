using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HealthSystem : MonoBehaviour
{
    public Player player;
    public Sprite FullHP;
    public Sprite EmptyHP;
    public TMP_Text text;
    public Image[] lives;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHp(player);
        CountHeals();
    }
    public void CheckHp(Player player)
    {
        for (int i = 0; i < lives.Length; i++)
        {
            if (i < player.HP)
            {
                lives[i].sprite = FullHP;
            }
            else
            {
                lives[i].sprite = EmptyHP;
            }
        }
    }
    public void CountHeals()
    {
        text.text = Convert.ToString(player.heals);
    }
}
