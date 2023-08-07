public class HuntingBow : PerkCard
{
    public override void OnSelect()
    {
        player.ProjectileSpeed *= 1.2f;
    }

    public override void OnUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
