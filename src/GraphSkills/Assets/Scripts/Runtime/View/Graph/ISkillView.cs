using System;

namespace Kefir.View.Graph
{
    public interface ISkillView
    {
        event Action Clicked;
        
        void Open();
        void Forget();
        void Choose();
        void CancelChoose();
        void SetCost(int value);
    }
}