using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    public float fallSpeed = 2f; // Tốc độ rơi xuống
    private bool canBeCollected = false; // Chỉ cho phép nhặt sau khi đã rơi xong

    private void Start()
    {
        // Đợi 0.5 giây rồi mới có thể nhặt
        Invoke("EnableCollection", 0.5f);
    }

    private void Update()
    {
        // Nếu chưa chạm đất, cho vật phẩm rơi xuống
        if (!canBeCollected)
        {
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    private void EnableCollection()
    {
        canBeCollected = true; // Cho phép nhặt vật phẩm
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeCollected && collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().IncreaseBulletCount();
            Destroy(gameObject);
        }
    }
}
