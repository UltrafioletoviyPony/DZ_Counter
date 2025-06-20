using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Counter))]
public class Counter : MonoBehaviour
{
    private LeftMouseButtonClick _leftMouseButton;
    private int _number;
    private float _delayTime;
    private WaitForSeconds _sleep;
    private Coroutine _coroutine;
    private bool _isRunned;

    public event Action<int> NumberUpdate;
    public event Action<bool> RunStatus;

    private void Awake()
    {
        _leftMouseButton = GetComponent<LeftMouseButtonClick>();
        _number = 0;
        _delayTime = .5f;
        _sleep = new WaitForSeconds(_delayTime);
        _isRunned = false;
    }

    private void OnEnable() =>
        _leftMouseButton.Clicked += Run;

    private void OnDisable() =>
        _leftMouseButton.Clicked -= Run;

    private void Run()
    {
        _isRunned = !_isRunned;
        RunStatus?.Invoke(_isRunned);

        if (_isRunned)
            _coroutine = StartCoroutine(Timer());
        else if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Timer()
    {
        bool isEnabled = true;

        while (isEnabled)
        {
            _number++;
            NumberUpdate?.Invoke(_number);
            yield return _sleep;
        }
    }
}