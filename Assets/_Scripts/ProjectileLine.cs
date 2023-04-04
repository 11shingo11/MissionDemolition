using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S;
    [Header("Set in Ispector")]
    public float minDist = 0.1f;
    private LineRenderer _line;
    private GameObject _poi;
    private List<Vector3> _points;
    private bool _isLaunched = false;

    void Awake()
    {
        S = this;
        _line = GetComponent<LineRenderer>();
        _line.enabled = false;
        _points = new List<Vector3>();
    }

    public GameObject poi 
    {
        get 
        {
            return (_poi);
        }
        set
        {
            _poi = value;
            if ( _poi != null )
            {
                _line.enabled = false;
                _points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    public void Clear()
    {
        _poi = null;
        _line.enabled = false;
        _points = new List<Vector3>();

    }

    public void AddPoint()
    {
        Vector3 pt = _poi.transform.position;
        if (_points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        {
            return;
        }

        _points.Add(pt);
        _line.positionCount = _points.Count;

        for (int i = 0; i < _points.Count; i++)
        {
            _line.SetPosition(i, _points[i]);
        }

        _line.enabled = true;
    }


    public Vector3 lastPoint
    {
        get{
            if(_points == null)
            {
                return (Vector3.zero);
            }
            return (_points[_points.Count-1]);
        }
    }


    public void ClearPreviousTrajectory()
    {
        if (_points.Count > 2)
        {
            _points.RemoveRange(0, _points.Count - 2);
            _line.positionCount = _points.Count;

            for (int i = 0; i < _points.Count; i++)
            {
                _line.SetPosition(i, _points[i]);
            }
        }
    }


    public void AddEmptyPoint()
    {
        if (_points.Count > 0)
        {
            _points.Add(_points[_points.Count - 1]);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_poi == null)
        {
            if (FollowCam.POI != null)
            {
                _poi = FollowCam.POI;
                _isLaunched = true;
                ClearPreviousTrajectory();
                AddEmptyPoint();
            }
            else
            {
                return;
            }
        }

        if (_isLaunched)
        {
            AddPoint();
        }

        if (FollowCam.POI == null)
        {
            _poi = null;
            _isLaunched = false;
        }
    }

}
