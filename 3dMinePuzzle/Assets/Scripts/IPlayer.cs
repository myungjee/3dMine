using UniRx;

namespace cubepuzzle
{
    internal interface IPlayer
    {
        ReactiveProperty<int> CurrentPosition { get; set; }
    }
}