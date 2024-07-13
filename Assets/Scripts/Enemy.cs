using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
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
        Jump();
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
        if (currentCoolDown >= coolDown )
        {
            Atack(rb.position, 1.1f, 9, 1);
            currentCoolDown = 0;
        }
        currentCoolDown += Time.deltaTime;
    }
    public virtual void Move(Vector2 target)
    {
        rb.AddForce(new Vector2((target.x - rb.position.x) * MoveSpeed, 0));
        // Apply inertia to the character movement
        rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(target.x, 0), Time.fixedDeltaTime * Deceleration);
        // Clamp the velocity to the maximum speed
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed);
    }



    public virtual void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, rayDistance, LayerMask.GetMask("Ground"));

        isGround = hit.collider != null;
        if (targetVector.x > rb.position.x && isGround)
        {

            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
    public void CheckDead()
    {
        if( HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Atack(Vector2 point, float radius, int layerMask, sbyte damage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(point, radius, 1 << layerMask);
        //�������� ��� ���������� � ������� �����

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<Player>())
            {
                hit.GetComponent<Player>().HP -= damage;
            }
        }

    }

}
