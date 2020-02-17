public enum Tags
{
	Player,
	Enemy
}

public enum Scenes
{
	Main,
	Stage,
	Stage1,
	GameOver
}


public static class GalagaHelper
{
	public static string GetTag(this Tags tag)
	{ 
		switch(tag)
		{
			case Tags.Player:
				return "Player";
			case Tags.Enemy:
				return "Enemy";
			default: return string.Empty;
		}
	}

	public static string GetScene(this Scenes scene)
	{
		switch (scene)
		{
			case Scenes.Main:
				return "MainScene";
			case Scenes.Stage:
				return "StageScene";
			case Scenes.Stage1:
				return "StageScene1";
			case Scenes.GameOver:
				return "GameOverScene";
			default: return string.Empty;
		}
	}
}
