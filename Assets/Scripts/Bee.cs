using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public GameObject bomb;
    public Transform shoot;
    public float timebomb= 4f;
    // Start is called before the first frame update
    void Start()
    {
        shoot.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        StartCoroutine(Shooting());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(timebomb);
        Instantiate(bomb, shoot.transform.position,transform.rotation);
        StartCoroutine(Shooting());
    }
}
