using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace cubepuzzle
{
    public class WinningCube : MonoBehaviour
    {
        [Inject]
        private GameStatusController status;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().Stat = Status.Won;
            }
        }
    }
}