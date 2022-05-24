 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{

    bool isHit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"&& !isHit)
        {
            collision.gameObject.GetComponent<Player>().RecountHP(-1);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 14f, ForceMode2D.Impulse);
        }
    }

    public IEnumerator Death()
    {
        isHit = true;
        GetComponent<Animator>().SetBool("Death", true);
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<Collider2D>().enabled = false;
        transform.GetChild(0).GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    public void StartDeath()
    {
        StartCoroutine(Death());
    }
}
