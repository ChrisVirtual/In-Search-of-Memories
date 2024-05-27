using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offSet;

    private void Start()
    {
        // If necessary, initialize the health bar here
        Health health = GetComponentInParent<Health>();
        if (health != null)
        {
            UpdateHealthBar(health.getCurrentHealth(), health.maxHealth);
        }
    }
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;
    }
    public void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = target.position + offSet;
    }


}
