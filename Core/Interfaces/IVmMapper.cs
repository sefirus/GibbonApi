namespace Core.Interfaces;

public interface IVmMapper <in TSource, out TDestination>
{
    TDestination Map(TSource source);
}