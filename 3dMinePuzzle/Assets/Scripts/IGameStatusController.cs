using UniRx;
using UnityEngine.UI;

namespace cubepuzzle
{
    public interface IGameStatusController
    {
        bool Won { get; set; }
        bool Lost { get; set; }
        Text UIText { get; }
    }
}