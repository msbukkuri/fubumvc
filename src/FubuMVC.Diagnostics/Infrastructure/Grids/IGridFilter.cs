using FubuMVC.Diagnostics.Models.Grids;

namespace FubuMVC.Diagnostics.Infrastructure.Grids
{
    public interface IGridFilter<T>
    {
        bool AppliesTo(T target, JsonGridFilter filter);
        bool Matches(T target, JsonGridFilter filter);
    }
}