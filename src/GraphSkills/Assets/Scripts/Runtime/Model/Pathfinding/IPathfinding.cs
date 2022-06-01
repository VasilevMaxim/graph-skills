namespace Kefir.Model.Graph
{
    public interface IPathfinding
    {
        bool IsCanFindWayToRoot(ISkillModel current);
    }
}