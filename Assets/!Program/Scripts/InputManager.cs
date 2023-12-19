using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action ButtonDownClicked;
    public event Action ButtonUpClicked;
    public event Action Destroyed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ButtonDownClicked.Invoke();

        if (Input.GetMouseButtonUp(0))
            ButtonUpClicked.Invoke();
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }
}