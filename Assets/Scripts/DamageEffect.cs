using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material material;
    private int damagePropID = Shader.PropertyToID("_DamageAmount");

    [Range(0f, 1f)]
    public float currentDamage = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    public void UpdateDamage(float damagePercent)
    { 
        currentDamage = Mathf.Clamp01(damagePercent);
        material.SetFloat(damagePropID, currentDamage);
    }
}