using System.Collections;

namespace Kefir.Model.Graph
{
    internal interface INodeModelInternal : INodeModel
    {
        void SetOpened(bool state);
        void SetCost(int cost);
    }
}