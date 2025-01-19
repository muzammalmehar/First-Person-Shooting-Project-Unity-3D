// BulletHole.cs
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    public float lifeTime = 5f; // Time until the bullet hole disappears

    void Start()
    {
        // Destroy the bullet hole after a certain amount of time
        Destroy(gameObject, lifeTime);
    }
}
