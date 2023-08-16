public class Multishot : PerkCard
{
    public override void OnSelect()
    {
        ResourceControls.Player.ProjectileCount += 1f;
        ResourceControls.Player.ProjectileSize *= 0.85f;
    }

    public override void OnUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
