using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace cubepuzzle
{
    public class GameStatusController : MonoBehaviour, IGameStatusController
    {
        public bool Won { get; set; }
        public bool Lost { get; set; }
        public Text UIText { get; }
       
        private void Start()
        {
//            CurrentStatus.Where(x => x.Value == Status.Lost).Subscribe(_ => Lost());
//            CurrentStatus.ObserveEveryValueChanged(_ => _).Where(x => x.Value == Status.Won).Subscribe(_ => Debug.Log("Won!"));
        }

        //private static void Lost()
        //{
        //    Debug.Log("Lost!");
        //}
    }
}