using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("references")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int PathIndex = 0;

    private void Start()
    {
        target = LevelManager.main.path[PathIndex];
    }

    private void Update()
    {
        if(Vector2.Distance(target.position, rb.position) <= 0.1f)
        {
            PathIndex++;
            
            if (PathIndex >= LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[PathIndex];
            }
        }
    }
    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += 180f;
        rb.rotation = angle;
    }
}
