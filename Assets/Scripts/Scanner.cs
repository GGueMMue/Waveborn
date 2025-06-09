using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float range;
    public LayerMask layerMask;
    public RaycastHit2D[] hits;
    public Transform nearEnemy;

    private void FixedUpdate()
    {
        hits = Physics2D.CircleCastAll(this.transform.position, range, Vector2.zero, 0, layerMask);
        nearEnemy = GetNear();
    }

    Transform GetNear()
    {
        Transform result = null;
        float diff = 100f;

        foreach (var hit in hits) 
        {
            Vector3 myPos = this.transform.position;
            Vector3 near = hit.transform.position;

            float dis = Vector3.Distance(myPos, near);

            if (dis < diff) 
            {
                diff = dis;
                result = hit.transform;
            }
        }


        return result;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
