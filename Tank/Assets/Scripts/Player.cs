using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeen = 3;
    private Vector3 bullectAulerAngles;
    private float timeVal;
    private float defendTimeVal=3;
    private bool isDefended=true;

    private SpriteRenderer sr;
    public Sprite[] TankSprite;
    public GameObject bullectPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;

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
        if (isDefended) {

            defendEffectPrefab.SetActive(true);

            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0) {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }
       
    }
    private void FixedUpdate()
    {
        if (PlayerMananger.Instance.isDefeat) {
            return;
        }
        Move();
        if (timeVal >= 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
    }
    /*坦克的攻击方法*/
    private void Attack() {
        if (Input.GetKeyDown(KeyCode.Space)) {

            Instantiate(bullectPrefab, transform.position,Quaternion.Euler(transform.eulerAngles+bullectAulerAngles));
            timeVal = 0;
        }

    }
     /*坦克移动方法*/
    private void Move() {
        float v = Input.GetAxisRaw("Vertical");
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
        if (Mathf.Abs(v) > 0.05f) {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        if (v != 0)
        {
            return;
        }
        float h = Input.GetAxisRaw("Horizontal");
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
        if (Mathf.Abs(v) > 0.05f)
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else {
            moveAudio.clip = tankAudio[0];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
    }

    /*死亡方法*/
    public void Die() {

        if (isDefended) {
            return;
        }
        PlayerMananger.Instance.isDead = true;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
