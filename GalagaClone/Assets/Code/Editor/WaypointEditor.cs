using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class WaypointEditor : EditorWindow
{
	private GameObject _waypointPrefab;
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
		GUILayout.Label("Choose an object to use as waypoint:", EditorStyles.boldLabel);
		_waypointPrefab =  (GameObject) EditorGUILayout.ObjectField(_waypointPrefab, typeof(GameObject), true);

		GUILayout.Label("Choose a TextMesh style:", EditorStyles.boldLabel);
		_textPrefab = (TextMesh)EditorGUILayout.ObjectField(_textPrefab, typeof(TextMesh), true);
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
