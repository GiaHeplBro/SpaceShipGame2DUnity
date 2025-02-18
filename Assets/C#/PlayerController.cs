using UnityEngine;
using UnityEngine.UI; // Để sử dụng UI elements

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
    public Slider healthSlider; // Tham chiếu đến UI Slider thanh máu

    [Header("Missile")]
    public GameObject missile;
    public Transform missileSpawnPosition; // Vị trí bắn viên đạn chính giữa
    public Transform[] multiShotPositions; // Các vị trí bắn khi có nhiều viên đạn
    public float destroyTime = 5f;

    [Header("Weapon")]
    public WeaponType currentWeapon = WeaponType.Single; // Mặc định là bắn đơn

    public enum WeaponType
    {
        Single,  // Bắn 1 viên
        Double,  // Bắn 2 viên
        Triple   // Bắn 3 viên
    }

    private void Start()
    {
        // Cập nhật giá trị thanh máu ban đầu
        healthSlider.value = health;
    }

    private void Update()
    {
        PlayerMovement();
        PlayerShoot();

        // Đổi súng bằng phím số (Chỉ để test, sau này có thể làm hệ thống nhặt vũ khí)
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentWeapon = WeaponType.Single;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentWeapon = WeaponType.Double;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentWeapon = WeaponType.Triple;
    }

    void PlayerMovement()
    {
        float xPos = Input.GetAxis("Horizontal");
        float yPos = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xPos, yPos, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    private int bulletCount = 1; // Số viên đạn bắn ra

    public void IncreaseBulletCount()
    {
        bulletCount = Mathf.Clamp(bulletCount + 1, 1, 5); // Tối đa 5 viên đạn
        Debug.Log("Bullet Count Increased: " + bulletCount);
    }

    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < bulletCount; i++)
            {
                SpawnMissile(i);
            }
            SpawnMuzzleFlash();
        }
    }

    void SpawnMissile(int index)
    {
        audioManager.PlaySFX(audioManager.fireballFire);

        // Điều chỉnh vị trí cho nhiều viên đạn
        float offset = (index - (bulletCount - 1) / 2f) * 0.3f;
        Vector3 spawnPos = missileSpawnPosition.position + new Vector3(offset, 0, 0);

        GameObject gm = Instantiate(missile, spawnPos, Quaternion.identity);
        gm.transform.SetParent(null);
        Destroy(gm, destroyTime);
    }


    void InstantiateMissile(Transform spawnPos)
    {
        GameObject gm = Instantiate(missile, spawnPos.position, spawnPos.rotation);
        gm.transform.SetParent(null);
        Destroy(gm, destroyTime);
    }

    void SpawnMuzzleFlash()
    {
        GameObject muzzle = Instantiate(GameManager.instance.muzzleFlash, missileSpawnPosition);
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

        // Cập nhật thanh máu UI
        healthSlider.value = health;

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
    public void UpgradeWeapon()
    {
        if (currentWeapon == WeaponType.Single)
            currentWeapon = WeaponType.Double;
        else if (currentWeapon == WeaponType.Double)
            currentWeapon = WeaponType.Triple;


        Debug.Log("Weapon Upgraded to: " + currentWeapon);
    }
}
