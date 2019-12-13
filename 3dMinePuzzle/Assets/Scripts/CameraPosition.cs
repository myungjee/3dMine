using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace cubepuzzle
{
    public class CameraPosition : MonoBehaviour
    {
        [Inject] private ICubeMaker cubeMaker;
        [SerializeField] private float someValue;

        // Start is called before the first frame update
        void Start()
        {
            this.transform.position -= this.transform.forward * (cubeMaker.NumberOfCubesPerRow - 3) * someValue;
        }
    }
}