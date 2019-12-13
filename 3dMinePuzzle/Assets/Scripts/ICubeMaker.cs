using UnityEngine;

namespace cubepuzzle
{
    internal interface ICubeMaker
    {
        int NumberOfCubesPerRow { get; }
        int NumberOfColumn { get; }
        float Min { get; set; }

        Vector3 StartingPosition();
        void MoveDown();
    }
}