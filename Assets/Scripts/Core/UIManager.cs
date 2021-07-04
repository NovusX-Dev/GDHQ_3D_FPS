using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] TextMeshProUGUI _ammoAmount;
    [SerializeField] TextMeshProUGUI _healthAmount;
    [SerializeField] GameObject[] _bloodSplatters;

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateAmmo(int amount, int maxAmount)
    {
        _ammoAmount.text = $"{amount} / {maxAmount}";


    }public void UpdateHealth(int amount, int maxAmount)
    {
        _healthAmount.text = $"{amount} / {maxAmount}";
    }

    public void ActivateSplatter()
    {
        _bloodSplatters[Random.Range(0, _bloodSplatters.Length)].SetActive(true);
    }

}
