using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Status
{
    public bool Positive { get; protected set; }
    public string Description { get; protected set; }
    public StatusCollection.STATUSES StatusType { get; protected set; }

    public GameObject Icon;

    public abstract void OnGet(Entity target);

    public abstract void OnTick(Entity target);

    public abstract void OnTimer(Entity target, float delayBetweenProcs);

    public abstract void OnEnd(Entity target);

    public abstract void OnRemove(Entity target);
}
