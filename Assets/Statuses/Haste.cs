using UnityEngine;

public class Haste : Status
{
    public Haste()
    {
        Positive = true;
        Description = "-50% Move Cooldown";
        StatusType = StatusCollection.STATUSES.HASTE;
        Icon = ResourceControls.HasteIcon;
    }

    public override void OnEnd(Entity target)
    {
        target.MoveCooldownTotalMultiplier *= 2f;
    }

    public override void OnGet(Entity target)
    {
        target.MoveCooldownTotalMultiplier *= 0.5f;
    }

    public override void OnRemove(Entity target)
    {
        
    }

    public override void OnTick(Entity target)
    {
        
    }

    public override void OnTimer(Entity target, float delayBetweenProcs)
    {
        
    }
}
