public class MultiVisitor : ICollectibleVisitor
{
    private readonly ICollectibleVisitor[] _visitors;

    public MultiVisitor(ICollectibleVisitor[] visitors)
    {
        _visitors = visitors;
    }

    public void Visit(Coin coin)
    {
        foreach (var visitor in _visitors)
            visitor.Visit(coin);
    }

    public void Visit(HealthPack healthPack)
    {
        foreach (var visitor in _visitors)
            visitor.Visit(healthPack);
    }
}
