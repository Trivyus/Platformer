public interface ICollectibleVisitor
{
    void Visit(Coin coin);
    void Visit(HealthPack healthPack);
}
