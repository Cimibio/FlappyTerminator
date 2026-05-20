using UnityEngine;

public class ProjectileRotator : MonoBehaviour
{
    public void SetRotation(Vector2 direction)
    {
        if (direction == Vector2.zero) 
            return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
    }
}