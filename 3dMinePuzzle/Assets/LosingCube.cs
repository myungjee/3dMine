using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosingCube : MonoBehaviour
{
    private Text text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text = GameObject.Find("TempUIText").GetComponent<Text>();

            text.text = "You Lost!";
        }
    }
}
