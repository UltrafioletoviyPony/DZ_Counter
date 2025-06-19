using System.Collections;
using TMPro;
using UnityEngine;

public class Viewer : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private LeftMouseButton _leftMouseButton;

    private Vector3 _defaultLocalScale;
    private Vector3 _eventLocalScale;
    private Color _defaultColor;
    private Color _eventColor;
    private Coroutine _coroutine;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _leftMouseButton = GetComponent<LeftMouseButton>();
        _defaultLocalScale = _text.transform.localScale;
        _eventLocalScale = _defaultLocalScale * 1.2f;
        _defaultColor = _text.color;
        _eventColor = Color.red;
        _text.text = "0";
    }

    private void OnEnable()
    {
        _leftMouseButton.Clicked += SetTextColor;
        _leftMouseButton.Clicked += SetTextLocalScale;
    }

    private void OnDisable()
    {
        _leftMouseButton.Clicked -= SetTextColor;
        _leftMouseButton.Clicked -= SetTextLocalScale;
    }

    public void SetText(int value) =>
        _text.text = value.ToString();

    private void SetTextLocalScale()
    {
        if (_leftMouseButton.IsRunned)
            _text.transform.localScale = _eventLocalScale;
        else
            _text.transform.localScale = _defaultLocalScale;
    }

    private void SetTextColor()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        StartCoroutineColor();
    }

    private void StartCoroutineColor()
    {
        if (_leftMouseButton.IsRunned)
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