using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Entity : MonoBehaviour
{
    [Header("Internally Defined")]
    [SerializeField]
    protected Rigidbody rbody;

    [SerializeField]
    protected SpriteRenderer srenderer;

    public RectTransform HealthBar;
    public RectTransform StaminaBar;
    public RectTransform StatusBar;

    public Projectile AttackProjectile;

    [Header("Background Stats")]
    [Range(-1f, 1f)]
    [SerializeField]
    protected float ForwardProjSpawnScaleMax = 1f;

    [Range(-1f, 1f)]
    [SerializeField]
    protected float ForwardProjSpawnScaleMin = 1f;

    public bool AttackLocked = false;

    [Header("Stats")]
    [Min(1)]
    public int MaxHealth = 10;
    [Min(0.1f)]
    public float HealthMultiplier;

    [Min(0)]
    [SerializeField]
    protected int Health = 10;

    [Min(0f)]
    public float MoveCooldown = 50f;
    [Min(0.1f)]
    public float MoveCooldownMultiplier = 1f;
    [Min(0.1f)]
    public float MoveCooldownTotalMultiplier = 1f;

    [HideInInspector]
    public float MoveCooldownTimer = 0f;

    [Min(-1f)]
    public float ProjectileSpeed = 1f;
    [Min(0.1f)]
    public float ProjectileSpeedMultiplier = 1f;

    [Min(-1)]
    public int ProjectilePierce = 0;
    [Min(0.1f)]
    public float ProjectilePierceMultiplier = 1f;

    [Min(0f)]
    public float ProjectileDuration = 1f;
    [Min(0.1f)]
    public float ProjectileDurationMultiplier = 1f;

    [Range(0.2f, 5f)]
    public float ProjectileSize = 0.8f;
    [Min(0.1f)]
    public float ProjectileSizeMulitplier = 1.25f;

    [Header("Entity Info")]

    public List<StatusApplication> StatusEffects = new();

    protected Vector2 CurrentDirection = new() { x = -1, y = 0 };


    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
        srenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (HealthBar) { UpdateHealth(); }
        if (StaminaBar) { UpdateStamina(); }
        UpdateStatus();
    }

    protected void UpdateHealth()
    {
        if (HealthBar) { HealthBar.localScale = new Vector2 { x = (float)Health / (float)MaxHealth, y = 1 }; }
    }

    protected void UpdateStamina()
    {
        if (MoveCooldownTimer > 0.0f) { MoveCooldownTimer -= 1f; }

        float movecd;
        try
        {
            movecd = MoveCooldownTimer / ((MoveCooldown * MoveCooldownMultiplier) * MoveCooldownTotalMultiplier);
            if (movecd > 1) { movecd = 1f; }
        }
        catch
        {
            movecd = 0f;
        }
        StaminaBar.localScale = new Vector2 { x = movecd, y = 1 };
    }

    protected abstract void RenderStatus(StatusApplication status);

    protected void UpdateStatus()
    {
        foreach (StatusApplication status in StatusEffects)
        {
            RenderStatus(status);
        }
    }

    protected Vector3 GetScaledPosition()
    {
        return GetScaledPosition(transform.position, ForwardProjSpawnScaleMax, ForwardProjSpawnScaleMin);
    }

    protected Vector3 GetScaledPosition(Vector3 position)
    {
        return GetScaledPosition(position, ForwardProjSpawnScaleMax, ForwardProjSpawnScaleMin);
    }

    protected Vector3 GetScaledPosition(float scaleMax, float scaleMin)
    {
        return GetScaledPosition(transform.position, scaleMax, scaleMin);
    }

    protected Vector3 GetScaledPosition(Vector3 position, float scaleMax, float scaleMin)
    {
        Vector2 dirScaled;
        if (CanMove(CurrentDirection)) { dirScaled = CurrentDirection * scaleMax; }
        else { dirScaled = CurrentDirection * scaleMin; }
        return new(position.x + dirScaled.x, position.y + dirScaled.y, 0);
    }

    protected Vector2 GetScaledPosition2D(Vector3 position)
    {
        return (Vector2)GetScaledPosition(position, ForwardProjSpawnScaleMax, ForwardProjSpawnScaleMin);
    }

    protected Vector2 GetScaledPosition2D(float scaleMax, float scaleMin)
    {
        return (Vector2)GetScaledPosition(transform.position, scaleMax, scaleMin);
    }

    protected Vector2 GetScaledPosition2D(Vector3 position, float scaleMax, float scaleMin)
    {
        return (Vector2)GetScaledPosition(position, scaleMax, scaleMin);
    }

    protected Projectile Attack(bool scaleLocation = true)
    {
        return Attack(AttackProjectile, transform.position, CurrentDirection, 0, scaleLocation);
    }

    protected Projectile Attack(Projectile projectileObj, bool scaleLocation = true)
    {
        return Attack(projectileObj, transform.position, CurrentDirection, 0, scaleLocation);
    }

    protected Projectile Attack(Vector3 projSummonLocation, bool scaleLocation = true)
    {
        return Attack(AttackProjectile, projSummonLocation, CurrentDirection, 0, scaleLocation);
    }

    protected Projectile Attack(Vector2 direction, bool scaleLocation = true)
    {
        return Attack(AttackProjectile, transform.position, direction, 0, scaleLocation);
    }

    protected Projectile Attack(Vector2 direction, float angle, bool scaleLocation = true)
    {
        return Attack(AttackProjectile, transform.position, direction, angle, scaleLocation);
    }

    protected Projectile Attack(Projectile projectileObj, Vector3 projSummonLocation, Vector2 direction, bool scaleLocation = true)
    {
        return Attack(projectileObj, projSummonLocation, direction, 0, scaleLocation);
    }

    protected Projectile Attack(Projectile projectileObj, Vector3 projSummonLocation, Vector2 direction, float angle, bool scaleLocation = true)
    {
        if (scaleLocation) { projSummonLocation = GetScaledPosition(projSummonLocation); }
        GameObject summonedobj = Instantiate(projectileObj.gameObject, GetScaledPosition(projSummonLocation), new Quaternion());
        Projectile projectile = summonedobj.GetComponent<Projectile>();
        projectile.Direction = (Vector2)(Quaternion.Euler(0, 0, angle) * (Vector3)direction);
        projectile.Speed *= (ProjectileSpeed * ProjectileSpeedMultiplier);
        projectile.Pierce += (ProjectilePierce * Mathf.RoundToInt(ProjectilePierceMultiplier));
        projectile.Duration = (int)((float)projectile.Duration * (ProjectileDuration * ProjectileDurationMultiplier));
        projectile.transform.localScale = new Vector3() { x = ProjectileSize * ProjectileSizeMulitplier, y = ProjectileSize * ProjectileSizeMulitplier, z = 1f };
        return projectile;
    }

    public abstract void TakeDamage(int damage);

    public abstract void Heal(int amount);

    protected abstract void OnPreMove();

    protected abstract bool OnMove(Vector2 direction, bool attackOnMove = true);

    protected abstract void OnPostMove();

    protected bool Move(Vector2 direction, bool attackOnMove = true)
    {
        OnPreMove();
        bool val = OnMove(direction, attackOnMove);
        if (direction.x > 0) { srenderer.flipX = true; }
        else if (direction.x < 0) { srenderer.flipX = false; }
        OnPostMove();
        return val;
    }

    protected bool CanMove(Vector2 direction)
    {
        Vector3 newpos = (transform.position + (Vector3)direction) - transform.position;
        Ray ray = new(transform.position, newpos);
        bool rayhit = Physics.Raycast(ray, out RaycastHit hit, 1f);

        if (!rayhit) { return true; }
        else if (hit.collider.isTrigger) { return true; }
        else { return false; }
    }

    protected abstract void OnPreAttack();

    protected abstract void OnAttack(Vector2 direction);

    protected abstract void OnPostAttack();

    protected void AttackOnMove(Vector2 direction)
    {
        if (AttackProjectile) {
            OnPreAttack();
            OnAttack(direction);
            OnPostAttack();
        }
    }
}
