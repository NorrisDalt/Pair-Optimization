using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool isDead = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        // hide player
        gameObject.SetActive(false);

        // trigger game over
        GameManager.Instance.GameOver();
    }
}
