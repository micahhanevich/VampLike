public class HuntingBow : PerkCard
{
    public override void OnSelect()
    {
        ResourceControls.Player.ProjectileSpeedMultiplier *= 1.15f;
        ResourceControls.Player.ProjectileDurationMultiplier *= 1.15f;
    }

    public override void OnUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
