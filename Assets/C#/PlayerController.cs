using UnityEngine;

public class PlayerController : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public float speed = 10f;

    [Header("Health")]
    public int health = 3; // Số máu của phi thuyền

    [Header("Missile")]
    public GameObject missile;
    public Transform missileSpawnPosition;
    public float destroyTime = 5f;

    public Transform muzzleSpawnPosition;

    private void Update()
    {
        PlayerMovement();
        PlayerShoot();
    }

    void PlayerMovement()
    {
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xPos, yPos, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMissile();
            SpawnMuzzleFlash();
        }
    }

    void SpawnMissile()
    {
        audioManager.PlaySFX(audioManager.fireballFire);

        GameObject gm = Instantiate(missile, missileSpawnPosition);
        gm.transform.SetParent(null);
        Destroy(gm, destroyTime);
    }

    void SpawnMuzzleFlash()
    {
        GameObject muzzle = Instantiate(GameManager.instance.muzzleFlash, muzzleSpawnPosition);
        muzzle.transform.SetParent(null);
        Destroy(muzzle, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            audioManager.PlaySFX(audioManager.shipwhenHit);

            TakeDamage(1); // Mất 1 máu khi va chạm
            GameObject explosion = Instantiate(GameManager.instance.explosion, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            Destroy(collision.gameObject); // Hủy vật thể ngay lập tức
        }
    }


    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player HP: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        audioManager.PlaySFX(audioManager.deadth);

        Debug.Log("Game Over");

        // Hiệu ứng nổ khi chết
        GameObject explosion = Instantiate(GameManager.instance.explosion, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);

        // Hiện Game Over menu
        GameManager.instance.gameOverMenu.SetActive(true);

        // Dừng thời gian để game dừng lại
        Time.timeScale = 0f;

        // Hủy nhân vật
        Destroy(gameObject);
    }

}
