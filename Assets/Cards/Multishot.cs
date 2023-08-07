public class Multishot : PerkCard
{
    public override void OnSelect()
    {
        player.ProjectileCount += 1;
        player.ProjectileSize *= 0.9f;
    }

    public override void OnUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
