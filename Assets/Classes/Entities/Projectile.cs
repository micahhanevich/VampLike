using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Internally Defined")]
    [SerializeField]
    protected Rigidbody rbody;
    [SerializeField]
    protected SpriteRenderer srenderer;
    public Vector2 Direction = new(-1f, 0f);

    [Header("Behavior Properties")]
    public Target TargetType = Target.All;
    public Firing FiringType = Firing.Ranged;

    [Header("Stats")]
    [Min(0)]
    public float Speed = 1f;
    public int Damage = 1;
    [Min(-1)]
    public int Pierce = 0;
    [Min(-1)]
    public int Duration = -1;

    public enum Target
    {
        All = 0,
        Enemy = 1,
        Player = 2,
        None = 3
    }

    public enum Firing
    {
        Ranged = 0,
        Melee = 1
    }

    private void Start()
    {
        if (!rbody) { rbody = GetComponent<Rigidbody>(); }
        SetRotation(Direction);
    }

    // When colliding with something, attempt to attack it
    private void OnTriggerEnter(Collider other)
    {
        Attack(other);
        // Place hooks here later
    }

    public void SetRotation(Vector2 direction)
    {
        switch (FiringType)
        {
            case Firing.Ranged:
                rbody.velocity = direction * (1 + Speed);
                Quaternion rot = Quaternion.Euler(0, 0, Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg);
                rbody.MoveRotation(rot);
                break;
            case Firing.Melee:
                break;
        }
    }

    // Determine if collision should be ignored based on Projectile's TargetType
    protected void Attack(Collider collision)
    {
        switch (TargetType)
        {
            // Ignore collision unless it's an Enemy
            case Target.Enemy:
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                    HitSuccess(enemy);
                }
                break;
            // Ignore collision unless it's the Player
            case Target.Player:
                if (collision.gameObject.CompareTag("Player"))
                {
                    Player player = collision.gameObject.GetComponent<Player>();
                    HitSuccess(player);
                }
                break;
            // Never ignore collision
            case Target.All:
                Entity target = collision.gameObject.GetComponent<Entity>();
                HitSuccess(target);
                break;
            // Always ignore collision
            case Target.None:
            default:
                break;
        }
        
        // Run when a collision is determined to be of the right type
        void HitSuccess(Entity target)
        {
            // Deal Damage to the target
            target.TakeDamage(Damage);

            // Check if projectile has enough pierce left
            Pierce -= 1;
            if (Pierce == -1) { Destroy(gameObject); }
        }
    }

    private void FixedUpdate()
    {
        if (Duration < 0)
        {
            transform.localScale *= 0.9f;
            GetComponent<BoxCollider>().enabled = false;
        }

        if (Duration <= -14)
        {
            Destroy(gameObject);
        }

        Duration -= 1;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        Direction = Direction.normalized;
    }
}
