using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatterFader : MonoBehaviour
{
    [SerializeField] private float _fadeSpeed = 2f;

    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        _canvasGroup.alpha -= _fadeSpeed * Time.deltaTime;

        if (_canvasGroup.alpha <= 0)
        {
            gameObject.SetActive(false);
            _canvasGroup.alpha = 1;
        }
    }

}
