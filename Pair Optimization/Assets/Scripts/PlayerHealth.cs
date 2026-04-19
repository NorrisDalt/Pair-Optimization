using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool isDead = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
        
        // Compare with custom enum tags vs string compare
        if (other.TryGetComponent<EnumTagComponent>(out var enumTag) && enumTag.tagValue == EnumTag.Enemy)
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
