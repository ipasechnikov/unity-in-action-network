using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            var hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                var hitPosition = hitCollider.transform.position;

                // Vertical correction so the direction won't point up or down
                hitPosition.y = transform.position.y;

                // Send message only when facing the right direction
                var direction = hitPosition - transform.position;
                if (Vector3.Dot(transform.forward, direction.normalized) > .5f)
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
