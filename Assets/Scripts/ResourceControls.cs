using UnityEngine;

public class ResourceControls : MonoBehaviour
{
    public static Player Player;

    [Header("Generic References")]
    public static GameObject HasteIcon;
    public GameObject HasteIconRef;

    public static GameObject ShockIcon;
    public GameObject ShockIconRef;

    public static GameObject FrozenIcon;
    public GameObject FrozenIconRef;

    public static GameObject BurnIcon;
    public GameObject BurnIconRef;

    public static GameObject AcidIcon;
    public GameObject AcidIconRef;

    public static GameObject BleedIcon;
    public GameObject BleedIconRef;

    public static GameObject SlowIcon;
    public GameObject SlowIconRef;

    public static GameObject StunIcon;
    public GameObject StunIconRef;

    public static GameObject RootIcon;
    public GameObject RootIconRef;

    public static GameObject WisdomIcon;
    public GameObject WisdomIconRef;

    public static GameObject TemporalIcon;
    public GameObject TemporalIconRef;

    public static GameObject ChipIcon;
    public GameObject ChipIconRef;

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
        HasteIcon = HasteIconRef;
        ShockIcon = ShockIconRef;
        FrozenIcon = FrozenIconRef;
        BurnIcon = BurnIconRef;
        AcidIcon = AcidIconRef;
        BleedIcon = BleedIconRef;
        SlowIcon = SlowIconRef;
        StunIcon = StunIconRef;
        RootIcon = RootIconRef;
        WisdomIcon = WisdomIconRef;
        TemporalIcon = TemporalIconRef;
        ChipIcon = ChipIconRef;
    }
}
