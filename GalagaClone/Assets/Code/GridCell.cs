using System;
using UnityEngine;

[Serializable]
public class GridCell : MonoBehaviour
{
	public bool IsFree { get; set; } = true;

	public EnemyType Type;

	public int FillIndex;
}
