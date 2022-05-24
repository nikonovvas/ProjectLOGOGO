 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;
    public float jumpH;
    public Transform groundChek;
    bool isGraunded;
    Animator anim;
    int curHp;
    int maxHp = 3;
    bool isHit = false;
    public Main main;
    public bool key = false;
    bool CanTP = true;
    public bool inWoter = false;
    int Rec_book = 0;


    // Start is called before the first frame update
     void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(inWoter){
            anim.SetInteger("State", 4);
            isGraunded = false;
            if (Input.GetAxis("Horizontal") != 0 )
                Flip();
        }
        else
        {
            CheckGround();

            if (Input.GetAxis("Horizontal") == 0 && (isGraunded))
            {
                anim.SetInteger("State", 1);
            }
            else
            {
                Flip();
                if (isGraunded)
                    anim.SetInteger("State", 2);
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && isGraunded == true) { 
            rb.AddForce(transform.up * jumpH, ForceMode2D.Impulse);
        }
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, rb.velocity.y);

    }
    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundChek.position, 0.2f);
        isGraunded = colliders.Length > 1;

        if (!isGraunded)
            anim.SetInteger("State", 3);
    }

    public void RecountHP(int deltaHp)
    {

        curHp = curHp + deltaHp;
        if (curHp >= maxHp)
        {
            curHp = maxHp;
        }
        if (deltaHp < 0)
        {
            StopCoroutine(OnHit());
            isHit = true;
            StartCoroutine(OnHit()); 
        }
        if (curHp <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Invoke("Lose",2f);
        }
        print("HP:" + curHp);
    }
    IEnumerator OnHit()
    {
        if(isHit)
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g-0.1f, GetComponent<SpriteRenderer>().color.b - 0.1f);
        else
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.1f, GetComponent<SpriteRenderer>().color.b + 0.1f);
        StopCoroutine(OnHit());

        if (GetComponent<SpriteRenderer>().color.g == 1f)
            StopCoroutine(OnHit());

        if (GetComponent<SpriteRenderer>().color.g <= 0)
            isHit = false;
        yield return new WaitForSeconds(0.02f);
            StartCoroutine(OnHit());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject);
            key = true;
        }
        if (collision.gameObject.tag == "Book")
        {
            Destroy(collision.gameObject);
            Rec_book++;
            print("Зачетных книжек:"+Rec_book);
        }
        if (collision.gameObject.tag == "heart")
        {
            Destroy(collision.gameObject);
            RecountHP(1);
            print("HP:" + curHp);
        }



        /*if (collision.gameObject.tag == "Door")
        {
            if (collision.gameObject.GetComponent<Door>().isOpen && CanTP)
            {
                CanTP = false;
                collision.gameObject.GetComponent<Door>().Teleport(gameObject);
                StartCoroutine(TPwait());
            }
            else if (key)
            {
                collision.gameObject.GetComponent<Door>().Unlock();
            }
        }*/
    }
    IEnumerator TPwait()
    {
        yield return new WaitForSeconds(1);
        CanTP = true;
    }

    void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
