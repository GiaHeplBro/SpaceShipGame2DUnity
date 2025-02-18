using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public int scoreToWin;
    public int enemyHealth;
    public int baseEnemyCount; // ✅ Số quái xuất hiện ban đầu
}

