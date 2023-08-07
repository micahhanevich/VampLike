using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private Canvas PlayerEffectCanvas;

    public RectTransform XPBar;

    public Canvas HUD;
    public Canvas LevelupMenu;

    [Header("Player Stats")]
    [Min(1)]
    protected int MaxXP = 3;

    [Min(0)]
    protected int XP = 1;

    [Min(0)]
    public int Level = 0;

    [Min(1)]
    public int ProjectileCount = 1;

    void Start()
    {
        PlayerEffectCanvas = GetComponent<Canvas>();
        rbody = GetComponent<Rigidbody>();
        srenderer = GetComponentInChildren<SpriteRenderer>();
        LevelupMenu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && MoveCooldownTimer == 0)
        {
            Move(new Vector2 { x = -1, y = 0 });
            srenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.D) && MoveCooldownTimer == 0)
        {
            Move(new Vector2 { x = 1, y = 0 });
            srenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.W) && MoveCooldownTimer == 0)
        {
            Move(new Vector2 { x = 0, y = 1 });
        }
        else if (Input.GetKey(KeyCode.S) && MoveCooldownTimer == 0)
        {
            Move(new Vector2 { x = 0, y = -1 });
        }

        if (Input.GetKey(KeyCode.Space))
        {
            
        }
    }
    
    private void FixedUpdate()
    {
        if (HealthBar) { UpdateHealth(); }
        if (StaminaBar) { UpdateStamina(); }
        if (XPBar) { UpdateXP(); }
    }

    protected Projectile[] MultiAttack(Vector2 direction)
    {
        Projectile[] projectiles = new Projectile[5];
        switch (ProjectileCount)
        {
            case 1:
                return new Projectile[] { Attack() };

            case 2:
                projectiles[0] = Attack(CurrentDirection, 15f);
                projectiles[1] = Attack(CurrentDirection, -15f);
                return projectiles;
            
            case 3:
                projectiles[0] = Attack(CurrentDirection, 30f);
                projectiles[1] = Attack(CurrentDirection, 0f);
                projectiles[2] = Attack(CurrentDirection, -30f);
                return projectiles;

            case 4:
                projectiles[0] = Attack(CurrentDirection, 45f);
                projectiles[1] = Attack(CurrentDirection, 15f);
                projectiles[2] = Attack(CurrentDirection, -15f);
                projectiles[3] = Attack(CurrentDirection, -45f);
                return projectiles;

            case 5:
                projectiles[0] = Attack(CurrentDirection, 60f);
                projectiles[1] = Attack(CurrentDirection, 30f);
                projectiles[2] = Attack(CurrentDirection, 0f);
                projectiles[3] = Attack(CurrentDirection, -30f);
                projectiles[4] = Attack(CurrentDirection, -60f);
                return projectiles;
        }
        return new Projectile[] { Attack() };
    }

    protected override void OnPreAttack()
    {
        
    }

    override protected void OnAttack(Vector2 direction)
    {
        CurrentDirection = direction;
        MoveCooldownTimer = MoveCooldown;
        if (AttackProjectile) { MultiAttack(direction); }
    }

    protected override void OnPostAttack()
    {
        
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

    protected override void OnPostMove()
    {
        
    }

    protected void UpdateXP()
    {
        XPBar.localScale = new Vector2 { x = (float)XP / (float)MaxXP, y = 1 };
    }

    public void GainXP(int amount)
    {
        XP += amount;
        if (XP >= MaxXP)
        {
            XP -= MaxXP;
            LevelUp();
        }
    }

    void LevelUp()
    {
        Level += 1;
        // MaxXP += Mathf.Abs(MaxXP * Mathf.RoundToInt((float)(Level + 1) / 2));
        MaxXP *= 2;
        ButtonControls.OpenLevelUpMenu();
    }
}
