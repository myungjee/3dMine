using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterColorChange : MonoBehaviour
{
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();

        rend.material.color = Color.black;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rend.material.color = Color.white;
        }
    }
}
