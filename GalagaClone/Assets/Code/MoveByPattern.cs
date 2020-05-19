using System;
using UnityEngine;

public class MoveByPattern : MonoBehaviour
{
	public GameObject Pattern;
	public float MoveSpeed = 5f;

	public event Action PatternFinished;

	private GameObject[] _waypoints;
	private int _waypointIndex = 0;
	private bool _isPatternFinished = false;
	private bool _isPatternStarted = false;

	// Start is called before the first frame update
	void Start()
	{
		_waypoints = new GameObject[Pattern.transform.childCount];//Resources.FindObjectsOfTypeAll<Waypoint>();
		int i = 0;
		foreach(Transform child in Pattern.transform)
		{
			_waypoints[i] = child.gameObject;
			i++;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(_isPatternFinished || !_isPatternStarted)
		{
			return;
		}

		transform.position = Vector3.MoveTowards(transform.position, _waypoints[_waypointIndex].transform.position, MoveSpeed * Time.deltaTime);

		if (transform.position == _waypoints[_waypointIndex].transform.position)
		{
			_waypointIndex++;
		}

		if (_waypointIndex == _waypoints.Length)
		{
			_isPatternFinished = true;
			_isPatternStarted = false;
			PatternFinished?.Invoke();
		}
	}

	public void StartPattern()
	{
		_isPatternStarted = true;
	}

	public void StopPattern()
	{
		_isPatternStarted = false;
	}
}
