public class ThickCloak : PerkCard
{
    public override void OnSelect()
    {
        ResourceControls.Player.MaxHealth += 2;
        ResourceControls.Player.Heal(2);
    }

    public override void OnUpgrade()
    {
        throw new System.NotImplementedException();
    }
}
