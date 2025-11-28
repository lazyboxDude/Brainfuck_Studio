using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SimpleOutline : MonoBehaviour
{
    public Color outlineColor = Color.yellow;
    public float outlineWidth = 0.05f;

    private Material originalMaterial;
    private Material outlineMaterial;
    private bool outlined = false;

    void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        originalMaterial = renderer.material;
        outlineMaterial = new Material(originalMaterial);
        outlineMaterial.color = outlineColor;
    }

    public void EnableOutline()
    {
        if (outlined) return;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = outlineMaterial;
        transform.localScale *= (1 + outlineWidth);
        outlined = true;
    }

    public void DisableOutline()
    {
        if (!outlined) return;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = originalMaterial;
        transform.localScale /= (1 + outlineWidth);
        outlined = false;
    }
}
