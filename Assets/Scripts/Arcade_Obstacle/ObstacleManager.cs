using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public List<GameObject> obstacles = new List<GameObject>();

    public void AddObstacle(GameObject obstacle)
    {
        obstacles.Add(obstacle);
        Debug.Log("Obstacle added. Total obstacles: " + obstacles.Count);
    }

    public void RemoveObstacle(GameObject obstacle)
    {
        obstacles.Remove(obstacle);
        Debug.Log("Obstacle removed. Total obstacles: " + obstacles.Count);
    }
}
