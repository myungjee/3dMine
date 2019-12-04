using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace cubepuzzle
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private CubeMaker cubeMaker;

        // Start is called before the first frame update
        void Start()
        {
            this.transform.position = cubeMaker.StartingPosition();

            Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)).Subscribe(_ => MoveLeft());
            Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)).Subscribe(_ => MoveBack());
            Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)).Subscribe(_ => MoveRight());
            Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)).Subscribe(_ => MoveFront());
            Observable.EveryUpdate().Where(_ => Input.GetKeyDown(KeyCode.Space)).Subscribe(_ => { MoveDown(); cubeMaker.MoveDown(); });
        }

        private void MoveFront()
        {
            if (IsValidPos(this.transform.position.z + 0.9f))
            {
                this.transform.position += new Vector3(0, 0, 1);
            }
        }
        
        private void MoveLeft()
        {
            if (IsValidPos(this.transform.position.x - 0.9f))
            {
                this.transform.position += new Vector3(-1, 0, 0);
            }
        }

        private void MoveBack()
        {
            if (IsValidPos(this.transform.position.z - 0.9f))
            {
                this.transform.position += new Vector3(0, 0, -1);
            }
        }

        private void MoveRight()
        {
            if (IsValidPos(this.transform.position.x + 0.9f))
            {
                this.transform.position += new Vector3(1, 0, 0);
            }
        }

        private bool IsValidPos(float currentVal)
        {
            return currentVal > cubeMaker.Min && currentVal < -cubeMaker.Min;
        }

        private void MoveDown()
        {
            this.transform.position += new Vector3(0, 0, 0);
        }
    }
}