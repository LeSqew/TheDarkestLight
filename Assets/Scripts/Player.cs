using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public sbyte HP = 7;
    public byte heals = 3;
    public sbyte MaxHp = 7;
    // передвижение
    public Rigidbody2D rb;
    public float MoveSpeed = 5f;
    private float Vertical;
    private float Horizontal;
    public float MaxSpeed = 20f;
    public float Deceleration = 1f;
    public float RunMultiplier = 1.1f;
    bool PlayerIsRunning
    {
        get
        {
            if (rb.velocity != Vector2.zero)
            { return true; }
            else { return false; }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            
            Atack(Direction, AtackRadius, 8, 1);
        }
        CheckHP();
        
        renderer.flipX = !(Direction.x - rb.position.x > 0);
        if (Input.GetKeyUp(KeyCode.Q) && HP != MaxHp && heals != 0)
        {
            Heal();
        }
        
    }
    private void FixedUpdate()
    {
        Move();
    }

    //Анимация
    public Animator animator;
    public SpriteRenderer renderer;

    void Move() 
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        Vector2 targetVelocity = new Vector2 (Horizontal * MoveSpeed, Vertical * MoveSpeed);
        if (PlayerIsRunning)
        {
            if (Vertical !=0 || Horizontal!=0) 
            {    
                if(Mathf.Abs(Vertical) > Mathf.Abs(Horizontal))
                {
                    if (Vertical > 0)
                    {
                        Direction = rb.position + new Vector2(0, 2);
                    }
                    else { Direction = rb.position + new Vector2(0, -2); }
                }
                else
                {
                    if (Horizontal > 0)
                    {
                        Direction = rb.position + new Vector2(2, 0);
                    }
                    else { Direction = rb.position + new Vector2(-2,0); }
                }
            }
            targetVelocity *= RunMultiplier;
        }
        // Apply inertia to the character movement
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * Deceleration);
        // Clamp the velocity to the maximum speed
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
        //
        rb.velocity = new Vector2(Horizontal, Vertical).normalized * MoveSpeed;

        //animation
        if (Horizontal != 0)
        {
            animator.SetBool("RunningSide", true);
            animator.SetBool("RunningDown", false);
            animator.SetBool("RunningTop", false);
        }
        else if (Vertical > 0)
        {
            animator.SetBool("RunningSide", false);
            animator.SetBool("RunningDown", false);
            animator.SetBool("RunningTop", true);
        }
        else if (Vertical < 0)
        {
            animator.SetBool("RunningSide", false);
            animator.SetBool("RunningDown", true);
            animator.SetBool("RunningTop", false);
        }
        else
        {
            animator.SetBool("RunningSide", false);
            animator.SetBool("RunningDown", false);
            animator.SetBool("RunningTop", false);
        }
    }
    public void CheckHP() {
        if(HP > MaxHp)
        {
            HP = MaxHp;
        }

        if (HP == 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void Heal()
    {
        HP = MaxHp;
        heals--;
    }

    //атака
    private Vector2 Direction = Vector2.zero; // точка атаки
    public float AtackRadius = 1f;

    // point - точка контакта
    // radius - радиус поражения
    // layerMask - номер слоя, с которым будет взаимодействие
    // damage - наносимый урон
    // allTargets - должны-ли получить урон все цели, попавшие в зону поражения
    public void Atack(Vector2 point, float radius, int layerMask, sbyte damage)
    {
        if (Mathf.Abs(Direction.x-rb.position.x) != 0)
        {
            animator.SetTrigger("AtackSide");
        }
        else if (Direction.y - rb.position.y > 0)
        {
            animator.SetTrigger("AtackTop");

        }
        else if (Direction.y - rb.position.y < 0)
        {
            animator.SetTrigger("AtackDown");
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 1 << layerMask);
        //получаем все коллайдеры в радиусе атаки


        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<Enemy>())
            {
                hit.GetComponent<Enemy>().HP -= damage;
            }
            if (hit.GetComponent<Enemy2>())
            {
                hit.GetComponent<Enemy2>().HP -= damage;
            }
            
        }

    }
    public GameObject panel;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Dialog")
        {
            panel.SetActive(true);
        }
    }
}
