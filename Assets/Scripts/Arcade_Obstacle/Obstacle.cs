using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private ObstacleManager obstacleManager;

    public void SetObstacleManager(ObstacleManager manager)
    {
        obstacleManager = manager;
    }

    void OnDestroy()
    {
        if (obstacleManager != null)
        {
            obstacleManager.RemoveObstacle(gameObject);
        }
    }
}