using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //变量
    public float moveSpeen = 3;
    private Vector3 bullectAulerAngles;
    private float v=-1;
    private float h;


    //计时器
    private float timeVal;
    private float timeValChangeDirection=4;
   
    //组件和对象
    private SpriteRenderer sr;
    public Sprite[] TankSprite;
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        if (timeVal >=1)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }

    }
    private void FixedUpdate()
    {
        Move();
    }
    /*坦克的攻击方法*/
    private void Attack()
    {

            Instantiate(bullectPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectAulerAngles));
            timeVal = 0;

    }
    /*坦克移动方法*/
    private void Move()
    {
        if (timeValChangeDirection >= 4)
        {

            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {
                h = -1;
                v = 0;
            }
            else if (num > 2 && num <= 4)
            {
                h = 1;
                v = 0;
            }
            timeValChangeDirection = 0;
        }
        else {
            timeValChangeDirection += Time.fixedDeltaTime;

        }
        transform.Translate(Vector3.up * v * moveSpeen * Time.deltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = TankSprite[2];
            bullectAulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = TankSprite[0];
            bullectAulerAngles = new Vector3(0, 0, 0);
        }
        if (v != 0)
        {
            return;
        }
        transform.Translate(Vector3.right * h * moveSpeen * Time.deltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = TankSprite[3];
            bullectAulerAngles = new Vector3(0, 0, 90);

        }
        else if (h > 0)
        {
            sr.sprite = TankSprite[1];
            bullectAulerAngles = new Vector3(0, 0, -90);
        }
    }

    /*死亡方法*/
    public void Die()
    {
        PlayerMananger.Instance.playerScore++;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Enemy") {
            timeValChangeDirection = 4;
        }
    }
}
