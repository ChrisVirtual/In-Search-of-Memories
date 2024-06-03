using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/FloatData")]
public class FloatValueSO : ScriptableObject
{
    [SerializeField]
    private float _value;

    public float value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChange?.Invoke(_value);
        }

    }
    public event Action<float> OnValueChange;

}
