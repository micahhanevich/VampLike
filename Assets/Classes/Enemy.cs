using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    private Player player;

    [Header("Stats")]
    [SerializeField]
    protected bool AttackMove = true;

    [Min(0)]
    public int RewardXP = 1;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
        srenderer = GetComponentInChildren<SpriteRenderer>();
        player = FindObjectOfType<Player>();
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0) { GetComponentInChildren<Animator>().SetBool("Death", true); }
    }

    private bool AttemptMove()
    {
        bool success = false;
        if (MoveCooldownTimer == 0)
        {
            try
            {
                Vector3 posdif = player.transform.position - transform.position;
                posdif.x = Mathf.Clamp(posdif.x, -1, 1);
                posdif.y = Mathf.Clamp(posdif.y, -1, 1);
                int r = Random.Range(0, 2);


                if (posdif.x != 0 && posdif.y != 0)
                {
                    switch (r)
                    {
                        case 0:
                            Move(new Vector2 { x = posdif.x, y = 0 }, AttackMove);
                            return true;
                        default:
                            Move(new Vector2 { x = 0, y = posdif.y }, AttackMove);
                            return true;
                    }
                }
                else if (posdif.x != 0 ^ posdif.y != 0)
                {
                    return Move(new Vector2 { x = posdif.x, y = posdif.y }, AttackMove);
                }
            }
            catch (MissingReferenceException)
            {
                Debug.LogWarning("Player cannot be found");
            }
        }
        return success;
    }

    private void Update()
    {
        AttemptMove();
    }

    protected override void OnPreMove()
    {
        
    }

    protected override bool OnMove(Vector2 direction, bool attackOnMove = true)
    {
        if (CanMove(direction))
        {
            rbody.MovePosition(transform.position + new Vector3 { x = direction.x, y = direction.y, z = 0 });
            if (attackOnMove) { AttackOnMove(direction); }
            return true;
        }
        else
        {
            if (attackOnMove) { AttackOnMove(direction); }
        }

        if (!attackOnMove) { AttackOnMove(direction); }
        return false;
    }

    override protected void OnPostMove()
    {

    }

    override protected void OnPreAttack()
    {
        
    }

    override protected void OnAttack(Vector2 direction)
    {
        CurrentDirection = direction;
        MoveCooldownTimer = MoveCooldown;
        if (AttackProjectile) { Attack(direction); }
    }

    protected override void OnPostAttack()
    {
        
    }

    private void OnDestroy()
    {
        try
        {
            player.GetComponent<Player>().GainXP(RewardXP);
        }
        catch (MissingReferenceException)
        {
            Debug.LogWarning("Missing Player- cannot reward XP");
        }
    }

}
