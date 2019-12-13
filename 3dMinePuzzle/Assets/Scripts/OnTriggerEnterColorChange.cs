using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterColorChange : MonoBehaviour
{
    public bool ColorChanged = false;

    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();

        rend.material.color = new Color(0.055f, 0.043f, 0.043f);
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
