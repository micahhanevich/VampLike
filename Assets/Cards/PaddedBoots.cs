public class PaddedBoots : PerkCard
{
    public override void OnSelect()
    {
        player.MoveCooldown *= 0.78f;
    }

    public override void OnUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
