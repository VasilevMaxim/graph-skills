namespace Kefir.Model.Graph
{
    public interface ISkillInternal : ISkill
    {
        void SetOpened(bool state);
        void SetCost(int cost);
    }
}