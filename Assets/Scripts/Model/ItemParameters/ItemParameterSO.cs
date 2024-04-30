using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ItemParameterSO : ScriptableObject
    {
        [field: SerializeField]
        public string ParameterName { get; private set; }
    }
}