using System;
using UnityEngine;
using UnityEngine.UI;

public class LeftMouseButton : MonoBehaviour
{
    private Button _button;
    private bool _isRunned = false;

    public bool IsRunned => _isRunned;

    public event Action Clicked;

    private void Awake() =>
        _button = GetComponent<Button>();

    private void OnEnable() =>
        _button.onClick.AddListener(Click);

    private void OnDisable() =>
        _button.onClick.RemoveListener(Click);

    private void Click()
    {
        _isRunned = !_isRunned;
        Clicked?.Invoke();
    }
}