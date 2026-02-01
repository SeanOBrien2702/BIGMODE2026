using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public static event Action<int> OnTargetHit = delegate { };
    [SerializeField] int health = 1;
    [SerializeField] bool canMove = true;
    [SerializeField] float stopVelocityThreshold = 0.3f;

    Rigidbody2D rb;

    public int Health { get => health; }
    public bool CanMove { get => canMove; set => canMove = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!canMove)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude < stopVelocityThreshold)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            DealDamage(collision.gameObject.GetComponent<LaunchController>().GetSpeedDamage());
        }
        else if(collision.gameObject.CompareTag("Target"))
        {
            DealDamage(1);
        }
    }

    void DealDamage(int damage)
    {
        health -= damage;
        OnTargetHit?.Invoke(health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}