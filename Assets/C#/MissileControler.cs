using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float missileSpeed = 25f;
    public int damage = 1; // Mỗi viên đạn gây 1 sát thương

    void Update()
    {
        transform.Translate(Vector3.up * missileSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Gây sát thương cho kẻ địch
            }

            GameObject explosion = Instantiate(GameManager.instance.explosion, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            Destroy(gameObject); // Hủy đạn sau khi va chạm
        }
    }
}
