using UnityEngine;

public class WaypointComponent : MonoBehaviour
{
	public GameObject Waypoints;
	public float MoveSpeed = 5f;

	private GameObject[] _waypoints;
	private int _waypointIndex = 0;

	// Start is called before the first frame update
	void Start()
	{
		_waypoints = new GameObject[Waypoints.transform.childCount];//Resources.FindObjectsOfTypeAll<Waypoint>();
		int i = 0;
		foreach(Transform child in Waypoints.transform)
		{
			_waypoints[i] = child.gameObject;
			i++;
		}
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, _waypoints[_waypointIndex].transform.position, MoveSpeed * Time.deltaTime);

		if (transform.position == _waypoints[_waypointIndex].transform.position)
		{
			_waypointIndex++;
		}

		if (_waypointIndex == _waypoints.Length)
			_waypointIndex = 0;
	}
}
