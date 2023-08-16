using UnityEngine;

public class StatusApplication
{
    public Status Status;
    public string EffectSource;

    public int Duration;
    public float Strength;

    public bool New = true;

    public GameObject Icon;

    public StatusApplication(Status status)
    {
        Status = status;
        EffectSource = "default";
        Duration = 0;
        Strength = 1;
    }

    public StatusApplication(Status status, string effectSource)
    {
        Status = status;
        EffectSource = effectSource;
        Duration = 0;
        Strength = 1;
    }

    public StatusApplication(Status status, string effectSource, int duration)
    {
        Status = status;
        EffectSource = effectSource;
        Duration = duration;
        Strength = 1;
    }

    public StatusApplication(Status status, string effectSource, float strength)
    {
        Status = status;
        EffectSource = effectSource;
        Duration = 0;
        Strength = strength;
    }

    public StatusApplication(Status status, string effectSource, int duration, float strength)
    {
        Status = status;
        EffectSource = effectSource;
        Duration = duration;
        Strength = strength;
    }
}
