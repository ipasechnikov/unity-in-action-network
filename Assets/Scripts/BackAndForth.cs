using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;
    [SerializeField] float minZ = -16.0f;
    [SerializeField] float maxZ = 16.0f;

    // Which direction is the object currently moving
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var deltaZ = direction * speed * Time.deltaTime;

        transform.Translate(0, 0, deltaZ);

        if (transform.position.z > maxZ || transform.position.z < minZ)
            direction = -direction;
    }
}
