using UnityEngine;

public class WaypointComponent : MonoBehaviour
{
	public float MoveSpeed = 5f;

	private Waypoint[] _waypoints;
	private int _waypointIndex = 0;

	// Start is called before the first frame update
	void Start()
	{
		_waypoints = Resources.FindObjectsOfTypeAll<Waypoint>();
		_waypointIndex = _waypoints.Length - 1;

		//to move
		var waypoints = FindObjectsOfType<Waypoint>();
		foreach (var waypoint in waypoints)
		{
			waypoint.gameObject.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, _waypoints[_waypointIndex].transform.position, MoveSpeed * Time.deltaTime);

		if (transform.position == _waypoints[_waypointIndex].transform.position)
		{
			_waypointIndex--;
		}

		if (_waypointIndex == -1)
			_waypointIndex = _waypoints.Length - 1;
	}
}
