using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using UnityEngine.UI;

namespace cubepuzzle
{
    public class Player : MonoBehaviour, IPlayer
    {
        public ReactiveProperty<int> CurrentPosition { get; set; }
        public Status Stat = Status.Normal;

        [SerializeField]
        private Text TextUI;

        [Inject]
        private ICubeMaker cubeMaker;

        // Start is called before the first frame update
        void Start()
        {
            this.transform.position = cubeMaker.StartingPosition();
            CurrentPosition = new ReactiveProperty<int>();

            CurrentPosition.Value = 8;
            Observable.EveryUpdate().Where(_ => Stat == Status.Normal && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))).Subscribe(_ => MoveLeft());
            Observable.EveryUpdate().Where(_ => Stat == Status.Normal && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))).Subscribe(_ => MoveBack());
            Observable.EveryUpdate().Where(_ => Stat == Status.Normal && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))).Subscribe(_ => MoveRight());
            Observable.EveryUpdate().Where(_ => Stat == Status.Normal && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))).Subscribe(_ => MoveFront());
            Observable.EveryUpdate().Where(_ => Stat == Status.Normal && (Input.GetKeyDown(KeyCode.Space))).Subscribe(_ => { MoveDown(); cubeMaker.MoveDown(); });
            Observable.EveryUpdate().Where(_ => Stat == Status.Lost).Subscribe(_ => TextUI.text = "Lost");
            Observable.EveryUpdate().Where(_ => Stat == Status.Won).Subscribe(_ => TextUI.text = "Won");
        }

        private void MoveFront()
        {
            if (IsValidPos(this.transform.position.z + 0.9f))
            {
                CurrentPosition.Value += 1;
                this.transform.position += new Vector3(0, 0, 1);
            }
        }
        
        private void MoveLeft()
        {
            if (IsValidPos(this.transform.position.x - 0.9f))
            {
                CurrentPosition.Value -= cubeMaker.NumberOfCubesPerRow;
                this.transform.position += new Vector3(-1, 0, 0);
            }
        }

        private void MoveBack()
        {
            if (IsValidPos(this.transform.position.z - 0.9f))
            {
                CurrentPosition.Value -= 1;
                this.transform.position += new Vector3(0, 0, -1);
            }
        }

        private void MoveRight()
        {
            if (IsValidPos(this.transform.position.x + 0.9f))
            {
                CurrentPosition.Value += cubeMaker.NumberOfCubesPerRow;
                this.transform.position += new Vector3(1, 0, 0);
            }
        }

        private bool IsValidPos(float currentVal)
        {
            return currentVal > cubeMaker.Min && currentVal < -cubeMaker.Min;
        }

        private void MoveDown()
        {
            CurrentPosition.Value -= cubeMaker.NumberOfColumn;
            this.transform.position += new Vector3(0, 0, 0);
        }
    }
}