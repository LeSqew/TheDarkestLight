using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public void Start()
    {
        target = FindObjectOfType<Player>();
    }
    public void Update()
    {
        AI();
        CheckDead();
    }
    public void FixedUpdate()
    {
        Move(targetVector);
    }
    public sbyte HP;
    public byte Damage;
    public const float coolDown = 0.5f;
    public float currentCoolDown = 0;
    public float MoveSpeed;
    public float MaxSpeed;
    public float Deceleration;
    public Rigidbody2D rb;
    public Player target;
    public Vector2 targetVector = Vector2.zero;
    public float jumpForce;
    public bool isGround;
    public float rayDistance = 0.6f;
    public void AI()
    {
        targetVector = target.rb.position;
        if (currentCoolDown >= coolDown)
        {
            Atack(rb.position, 1.1f, 9, 1);
            currentCoolDown = 0;
        }
        currentCoolDown += Time.deltaTime;
    }
    public void Move(Vector2 target)
    {
        Vector2 targetVelocity = -(rb.position-target) * MoveSpeed;
        // Apply inertia to the character movement
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * Deceleration);
        // Clamp the velocity to the maximum speed
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
        //
        rb.velocity = new Vector2(targetVelocity.x, targetVelocity.y).normalized * MoveSpeed;
    }
    public void CheckDead()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Atack(Vector2 point, float radius, int layerMask, sbyte damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 1 << layerMask);
        //получаем все коллайдеры в радиусе атаки

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<Player>())
            {
                hit.GetComponent<Player>().HP -= damage;
            }
        }

    }
}
