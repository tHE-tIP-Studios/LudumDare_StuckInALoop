using UnityEngine;

public class ObstacleRemover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("I got triggered");
        if (other.CompareTag("Obstacle"))
        {
            print("It was a obstacle");
            if (other.gameObject.transform.parent.TryGetComponent<WallFunction>(
                out WallFunction wall))
            {
                print("It was a wall");
                wall.Poll.ReturnToPool(wall);
            }
            if (other.gameObject.transform.parent.TryGetComponent<RampFunction>(
                out RampFunction ramp))
            {
                print("It was a ramp");
                ramp.Pool.ReturnToPool(ramp);
            }
        }
    }
}
