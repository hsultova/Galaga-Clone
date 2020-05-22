using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	public GroupPatternPair[] EnemyGroups;
}

[Serializable]
public class GroupPatternPair
{
	public EnemyGroup EnemyGroup;
	public GameObject Pattern;
}
