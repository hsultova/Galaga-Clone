using UnityEngine;

[CreateAssetMenu(fileName ="EnemyGroup", menuName = "Data/EnemyGroup")]
public class EnemyGroup : ScriptableObject
{
	public int Enemies;
	public float Offset;
	public GameObject Enemy;
}
