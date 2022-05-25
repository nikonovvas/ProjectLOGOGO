using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Сamera : MonoBehaviour
{
    public float speed = 1f;
    public float sm = 0;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(target.position.x , target.position.y + sm, target.position.z);
        position.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position,position, speed * Time.deltaTime);
    }
}
