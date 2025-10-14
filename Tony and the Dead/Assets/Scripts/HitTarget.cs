using UnityEngine;

public class HitTarget : MonoBehaviour
{
    // This variable stores the health of the target
    public int health = 100;

    // This method can be called to make the target take damage
    public void TakeHit(int damage)
    {
        // Subtract the damage from health
        health -= damage;
        Debug.Log($"HitTarget took {damage} damage! Health is now {health}.");

        // If health is zero or less, the target is 'destroyed'
        if (health <= 0)
        {
            Die();
        }
    }

    // This method handles what happens when the target 'dies'
    void Die()
    {
        Debug.Log("HitTarget has been destroyed!");
        // You can add more logic here, like playing an animation or removing the object
        // For now, we'll just deactivate the GameObject
        gameObject.SetActive(false);
    }
}
