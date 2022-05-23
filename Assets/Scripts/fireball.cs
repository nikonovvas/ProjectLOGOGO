using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public float speed = 2f;
    public float timelivebomb = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Livebomb());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        
    }
    IEnumerator Livebomb()
    {
        yield return new WaitForSeconds(timelivebomb);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopCoroutine(Livebomb());
        gameObject.SetActive(false);
    }
}
