using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CounterView))]
public class CounterView : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Counter _counter;
    private Vector3 _defaultLocalScale;
    private Vector3 _eventLocalScale;
    private Color _defaultColor;
    private Color _eventColor;
    private Coroutine _coroutine;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _counter = GetComponent<Counter>();
        _defaultLocalScale = _text.transform.localScale;
        _eventLocalScale = _defaultLocalScale * 1.2f;
        _defaultColor = _text.color;
        _eventColor = Color.red;
        _text.text = "0";
    }

    private void OnEnable()
    {
        _counter.NumberUpdate += NumberUpdate;
        _counter.RunStatus += ParametresUpdate;
    }

    private void OnDisable()
    {
        _counter.NumberUpdate -= NumberUpdate;
        _counter.RunStatus += ParametresUpdate;
    }

    private void ParametresUpdate(bool isRunned)
    {
        SetTextColor(isRunned);
        SetTextLocalScale(isRunned);
    }

    private void NumberUpdate(int text) =>
        _text.text = text.ToString();

    private void SetTextLocalScale(bool isRunned)
    {
        if (isRunned)
            _text.transform.localScale = _eventLocalScale;
        else
            _text.transform.localScale = _defaultLocalScale;
    }

    private void SetTextColor(bool isRunned)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        StartCoroutineColor(isRunned);
    }

    private void StartCoroutineColor(bool isRunned)
    {
        if (isRunned)
            _coroutine = StartCoroutine(ColorTransition(_defaultColor, _eventColor));
        else
            _coroutine = StartCoroutine(ColorTransition(_eventColor, _defaultColor));
    }

    private IEnumerator ColorTransition(Color colorFrom, Color colorTo)
    {
        float startTime = 0;
        float endTime = 1;

        while (startTime < endTime)
        {
            startTime += Time.deltaTime;
            _text.color = Color.Lerp(colorFrom, colorTo, startTime);
            yield return null;
        }
    }
}