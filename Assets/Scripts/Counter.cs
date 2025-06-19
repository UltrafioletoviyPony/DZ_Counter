using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private LeftMouseButton _leftMouseButton;
    private Viewer _viewer;
    private int _number;
    private float _delayTime;
    private WaitForSeconds _sleep;
    private Coroutine _coroutine;

    private void Awake()
    {
        _leftMouseButton = GetComponent<LeftMouseButton>();
        _viewer = GetComponent<Viewer>();
        _number = 0;
        _delayTime = .5f;
        _sleep = new WaitForSeconds(_delayTime);
    }

    private void OnEnable() =>
        _leftMouseButton.Clicked += Run;

    private void OnDisable() =>
        _leftMouseButton.Clicked -= Run;

    private void Run()
    {
        if (_leftMouseButton.IsRunned)
            _coroutine = StartCoroutine(Timer());
        else if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            _number++;
            _viewer.SetText(_number);
            yield return _sleep;
        }
    }
}