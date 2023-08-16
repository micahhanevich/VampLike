public class WideShafts : PerkCard
{
    public override void OnSelect()
    {
        ResourceControls.Player.ProjectileSizeMulitplier *= 1.2f;
    }

    public override void OnUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
