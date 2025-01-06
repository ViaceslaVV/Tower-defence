using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;
public class Turret : MonoBehaviour
{
    [SerializeField] private float turretRange = 5f;

    private Transform targetE;

    private void Update()
    {
        if (targetE == null)
        {
            FindTarget();
        }
    }
    
    
    private void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.forward, turretRange);
        
    }

    public void FindTarget()
    {

    }


        
}
