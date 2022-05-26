namespace Kefir.Model.Graph
{
    internal interface ISkillInternal : ISkill
    {
        void SetOpened(bool state);
        void SetCost(int cost);
    }
}