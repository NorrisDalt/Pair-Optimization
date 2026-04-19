using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;

    public void ResetHealth()
    {
        health = 3; // reset when reused from pool
    }

    void Update()
    {
        if (EnemyPool.Instance.player == null) return;

        Vector3 dir = (EnemyPool.Instance.player.position - transform.position).normalized;
        transform.position += dir * EnemyPool.Instance.enemyMoveSpeed * Time.deltaTime;

        float x = Mathf.Clamp(transform.position.x, -5f, 5f);
        float y = Mathf.Clamp(transform.position.y, -5f, 5f);
        transform.position = new Vector3(x, y, 0);
    }

    public void TakeHit()
    {
        health--;

        if (health <= 0)
        {
            gameObject.SetActive(false);
            WaveSpawner.Instance.OnEnemyKilled();
        }
    }
}
