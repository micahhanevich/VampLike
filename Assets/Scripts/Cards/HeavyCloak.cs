public class HeavyCloak : PerkCard
{
    public override void OnSelect()
    {
        ResourceControls.Player.MaxHealth += 4;
        ResourceControls.Player.Heal(4);
        ResourceControls.Player.MoveCooldownMultiplier *= 1.1f;
    }

    public override void OnUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
