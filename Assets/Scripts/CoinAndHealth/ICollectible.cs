using System;

public interface ICollectible
{
    void Collect(ICollectibleVisitor visitor);

    event Action<ICollectible> Collected;
}
