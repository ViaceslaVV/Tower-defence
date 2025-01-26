using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;
public class Turret : MonoBehaviour
{
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [SerializeField] private float turretRange = 5f;
    [SerializeField] private float rotattionSpeed = 10f;
    [SerializeField] private float bps = 1f;

    private Transform target;
    private float timeUntilFire;

    public AudioSource audioSource;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }


        RotateTowardsTarget();

        if (!ChecktargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if(timeUntilFire >= 1f/bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
       
    }
    private void Shoot()
    {
        GameObject bulletobj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletobj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);

    }
    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, turretRange,
        (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }
    private bool ChecktargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= turretRange;
    }
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y,
        target.position.x - transform.position.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation,
        targetRotation, rotattionSpeed * Time.deltaTime);
    }


    


    
   

        
}
