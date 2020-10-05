using UnityEngine;

public class ObstacleRemover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (other.gameObject.transform.parent.TryGetComponent<WallFunction>(
                out WallFunction wall))
            {
                wall.Poll.ReturnToPool(wall);
            }
            if (other.gameObject.transform.parent.TryGetComponent<RampFunction>(
                out RampFunction ramp))
            {
                ramp.Pool.ReturnToPool(ramp);
            }
        }
    }
}
