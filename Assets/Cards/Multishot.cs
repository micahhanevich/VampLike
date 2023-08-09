public class Multishot : PerkCard
{
    public override void OnSelect()
    {
        player.ProjectileCount += 0.5f;
        player.ProjectileSize *= 0.85f;
    }

    public override void OnUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
