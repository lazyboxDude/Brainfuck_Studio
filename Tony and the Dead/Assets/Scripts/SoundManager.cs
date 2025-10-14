using UnityEngine;

public class SoundManager : MonoBehaviour
{
       // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static SoundManager instance { get; set; }
    public AudioSource gunShotSound;

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
