using UnityEngine;

public class Stun : Status
{
    public Stun()
    {
        Positive = false;
        Description = "Cannot Attack";
        StatusType = StatusCollection.STATUSES.STUN;
        Icon = ResourceControls.StunIcon;
    }

    public override void OnEnd(Entity target)
    {
        target.AttackLocked = false;
    }

    public override void OnGet(Entity target)
    {
        target.AttackLocked = true;
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
