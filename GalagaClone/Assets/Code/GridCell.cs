using System;
using UnityEngine;

[Serializable]
public class GridCell : MonoBehaviour
{
	public event Action FillCell;

	private bool _isFree = true;
	public bool IsFree
	{
		get
		{
			return _isFree;
		}
		set
		{
			_isFree = value;
			if(_isFree == false)
			{
				FillCell?.Invoke();
			}
		}
	}

	public EnemyType Type;

	public int FillIndex;
}
