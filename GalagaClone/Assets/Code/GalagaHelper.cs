public enum Tags
{
	Player,
	Enemy
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
}
