using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterColorChange : MonoBehaviour
{
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();

        rend.material.color = new Color(0.056f, 0.056f, 0.056f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ColorChange();
        }
    }

    public void ColorChange()
    {
        rend.material.color = Color.white;
    }
}
