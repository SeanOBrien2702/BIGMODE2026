using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material material;

    // Make sure this matches the Reference name in Shader Graph
    // (Check the Blackboard "Node Settings" tab for the exact string)
    private int damagePropID = Shader.PropertyToID("_DamageAmount");

    [Range(0f, 1f)]
    public float currentDamage = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // We use material (not sharedMaterial) to create a unique instance 
        // so enemies don't all get damaged at the same time.
        material = spriteRenderer.material;
    }

    void Update()
    {
        // For testing: You can change the slider in the Inspector
        // In a real game, you would call UpdateDamage() when hit.
        UpdateDamage(currentDamage);
    }

    public void UpdateDamage(float damagePercent)
    {
        // Clamp ensures we stay between 0 and 1
        currentDamage = Mathf.Clamp01(damagePercent);
        material.SetFloat(damagePropID, currentDamage);
    }
}