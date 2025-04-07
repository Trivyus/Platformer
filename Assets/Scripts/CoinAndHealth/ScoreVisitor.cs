public class ScoreVisitor : ICollectibleVisitor
{
    private int _score;

    public void Visit(Coin coin)
    {
        _score += 10;
    }

    public void Visit(HealthPack healthPack)
    {
    }
}
