using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class WaypointEditor : EditorWindow
{
	private GameObject _waypointPrefab;
	private GameObject _waypointsParent;
	private TextMesh _textPrefab;
	private bool _canInstantiateWaypoints = true;
	private List<Waypoint> _waypointObjects = new List<Waypoint>();

	[MenuItem("Window/Waypoint Editor")]
	public static void ShowWindow()
	{
		GetWindow<WaypointEditor>("Waypoint Editor");
	}

	void OnGUI()
	{		
		_canInstantiateWaypoints = EditorGUILayout.Toggle("Instantiate Waypoints", _canInstantiateWaypoints);
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Waypoint:", EditorStyles.boldLabel);
		_waypointPrefab =  (GameObject) EditorGUILayout.ObjectField(_waypointPrefab, typeof(GameObject), true);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Parent object:", EditorStyles.boldLabel);
		_waypointsParent = (GameObject)EditorGUILayout.ObjectField(_waypointsParent, typeof(GameObject), true);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("TextMesh style:", EditorStyles.boldLabel);
		_textPrefab = (TextMesh)EditorGUILayout.ObjectField(_textPrefab, typeof(TextMesh), true);
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Fill automatically"))
		{
			_waypointPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/WaypointSphere.prefab") as GameObject;
			//_waypointsParent = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Waypoints.prefab") as GameObject;
			_textPrefab = AssetDatabase.LoadAssetAtPath<TextMesh>("Assets/Prefabs/WaypointText.prefab") as TextMesh;
		}

		if (GUILayout.Button("Save prefab"))
		{
			string prefabPath = $"Assets/Prefabs/{_waypointsParent.name}.prefab";
			prefabPath = AssetDatabase.GenerateUniqueAssetPath(prefabPath);
			PrefabUtility.SaveAsPrefabAssetAndConnect(_waypointsParent, prefabPath, InteractionMode.UserAction);
		}
		EditorGUILayout.EndHorizontal();

	}

	private void OnEnable()
	{
		SceneView.duringSceneGui += SceneGUI;
	}

	private void OnDisable()
	{
		SceneView.duringSceneGui -= SceneGUI;
	}

	private void SceneGUI(SceneView sceneView)
	{
		if (!_canInstantiateWaypoints)
			return;

		if (Event.current.type == EventType.MouseDown)
		{
			Vector3 mousePosition = Event.current.mousePosition;
			mousePosition.y = sceneView.camera.pixelHeight - mousePosition.y;
			mousePosition = sceneView.camera.ScreenToWorldPoint(mousePosition);
			mousePosition.z = 0;

			GameObject waypointObject = Instantiate(_waypointPrefab, mousePosition, Quaternion.identity);
			waypointObject.transform.SetParent(_waypointsParent.transform);

			TextMesh textObject = Instantiate(_textPrefab, mousePosition, Quaternion.identity);
			textObject.transform.SetParent(waypointObject.transform);
			textObject.text = $"{_textPrefab.text} {_waypointObjects.Count}";
		}
	}

	private void OnHierarchyChange()
	{
		_waypointObjects = Resources.FindObjectsOfTypeAll<Waypoint>().ToList();
	}
}
