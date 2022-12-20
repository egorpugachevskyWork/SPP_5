using System.Runtime.InteropServices.ComTypes;

namespace MainPart;

public class ImplementationInfo
{
    public ImplementationInfo(LivingTime timeToLive, Type implementationType, Enum? index)
    {
        TimeToLive = timeToLive;
        ImplementationType = implementationType;
        Index = index;
    }
    public Enum? Index { get; set; }
    public LivingTime TimeToLive { get; set; }
    public Type ImplementationType{ get; set; }
}