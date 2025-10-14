using UnityEngine;
using TMPro;
public class AmmoManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static AmmoManager instance { get; set; }
      //UI
    public TextMeshProUGUI ammoDisplay;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
