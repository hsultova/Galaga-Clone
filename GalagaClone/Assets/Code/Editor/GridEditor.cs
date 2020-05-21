using UnityEditor;
using UnityEngine;

public class GridEditor : EditorWindow
{
	private GameObject _gridPrefab;
	private GameObject _gridParent;
	private int _rows;
	private int _columns;
	private float _offset;

	[MenuItem("Window/Grid Editor")]
	public static void ShowWindow()
	{
		GetWindow<GridEditor>("Grid Editor");
	}

	void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Grid prefab:", EditorStyles.boldLabel);
		_gridPrefab = (GameObject)EditorGUILayout.ObjectField(_gridPrefab, typeof(GameObject), true);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Grid Parent object:", EditorStyles.boldLabel);
		_gridParent = (GameObject)EditorGUILayout.ObjectField(_gridParent, typeof(GameObject), true);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Rows:", EditorStyles.boldLabel);
		_rows = EditorGUILayout.IntField(_rows);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Columns:", EditorStyles.boldLabel);
		_columns = EditorGUILayout.IntField(_columns);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Offset:", EditorStyles.boldLabel);
		_offset = EditorGUILayout.FloatField(_offset);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Generate"))
		{
			int i = 1;
			for (int row = 0; row < _rows; row++)
			{
				for (int column = 0; column < _columns; column++)
				{
					var cell = Instantiate(_gridPrefab, new Vector2(_gridParent.transform.position.x + row * _offset, _gridParent.transform.position.y + column * _offset), Quaternion.identity);
					cell.transform.SetParent(_gridParent.transform);
					cell.name = "Cell" + i;
					i++;
				}
			}
		}
		EditorGUILayout.EndHorizontal();
	}
}
