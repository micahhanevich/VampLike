public class PaddedBoots : PerkCard
{
    public override void OnSelect()
    {
        ResourceControls.Player.MoveCooldownMultiplier *= 0.88f;
    }

    public override void OnUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
