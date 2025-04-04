public interface ICollectible
{
    void Accept(ICollectibleVisitor visitor);
}
