using UnityEngine;

public class ChangeColorCube : Interactable
{
    MeshRenderer mesh;
    public Color[] colors;
    private int colorIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        colorIndex++;
        if (colorIndex > colors.Length - 1)
        {
            colorIndex = 0;
        }
        mesh.material.color = colors[colorIndex];
    }
}
