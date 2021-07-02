using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] TextMeshProUGUI _ammoAmount;

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateAmmo(int amount, int maxAmount)
    {
        _ammoAmount.text = $"{amount} / {maxAmount}";
    }
    
}
