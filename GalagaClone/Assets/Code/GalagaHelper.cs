using UnityEngine;

public enum Tag
{
	Player,
	Enemy
}

public enum Scene
{
	Main,
	Stage,
	Stage1,
	GameOver
}

public enum EnemyType
{
	Blue,
	Red,
	Green
}

public static class GalagaHelper
{
	public static string GetTag(this Tag tag)
	{ 
		switch(tag)
		{
			case Tag.Player:
				return "Player";
			case Tag.Enemy:
				return "Enemy";
			default: return string.Empty;
		}
	}

	public static string GetScene(this Scene scene)
	{
		switch (scene)
		{
			case Scene.Main:
				return "MainScene";
			case Scene.Stage:
				return "StageScene";
			case Scene.Stage1:
				return "StageScene1";
			case Scene.GameOver:
				return "GameOverScene";
			default: return string.Empty;
		}
	}
}

[System.Serializable]
public class EnenmySpritePair
{
	public EnemyType Type;
	public Sprite Image;
}
