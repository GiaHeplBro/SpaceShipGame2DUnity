using UnityEngine;

public class EnemyController : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public float speed = 3f;
    public int health = 3; // Kẻ địch có 3 HP, bắn trúng 3 phát mới chết
    public GameObject powerUpItem; // Prefab vật phẩm

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        audioManager.PlaySFX(audioManager.fireballhitEnemy);

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        audioManager.PlaySFX(audioManager.enemyDestroy);

        // Hiệu ứng nổ
        GameObject explosion = Instantiate(GameManager.instance.explosion, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);

        // Xác suất rơi PowerUp (50% rơi)
        if (Random.value > 0.8f)
        {
            GameManager.instance.SpawnPowerUp(transform.position);
        }

        // **Thêm điểm khi tiêu diệt kẻ địch**
        GameManager.instance.AddScore(1);

        Destroy(gameObject);
    }


}
