using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    NavMeshAgent agent;
    bool reTagWaypoints = true;
    List<Vector3> waypoints = new List<Vector3>();
    List<Vector3> spawnPoints = new List<Vector3>();
    GameObject[] wayPointObjs;
    GameObject[] spawnPointObjs;
    GameObject[] jumpPointObjs;
    public int waypointMaxCount = 10; 
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        wayPointObjs = GameObject.FindGameObjectsWithTag("WayPoints");
        spawnPointObjs = GameObject.FindGameObjectsWithTag("SpawnPoints");
        jumpPointObjs = GameObject.FindGameObjectsWithTag("JumpPoints");
    }
    Vector3 GetRandomWayPoints(GameObject[] gOs)
    {
        int r = gOs.Length;
        Vector3 p = new Vector3();
        GameObject o = gOs[Random.Range(0, r)];
        p.x = o.transform.position.x;
        p.z = o.transform.position.z;
        return p;
    }
    public void Move()
    {
        agent.isStopped = true;
        gameObject.transform.position = GetRandomWayPoints(spawnPointObjs);
        waypoints = new List<Vector3>();
        waypoints.Add(GetRandomWayPoints(wayPointObjs));
        waypoints.Add(GetRandomWayPoints(jumpPointObjs));
        agent.isStopped = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (!reTagWaypoints) reTagWaypoints = true;
                if (waypoints.Count >= waypointMaxCount) return;
                waypoints.Add(hit.point);
            }
        }
        if (waypoints.Count > 0)
        {
            Vector3 newDest = waypoints[0];
            if (agent.transform.position.x != newDest.x || agent.transform.position.z != newDest.z)
            {
                agent.destination = newDest;
            }
            else
            {
                waypoints.RemoveAt(0);
            }
        }
    }
}
