using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

    public float movSpeed = 5f;
    public float rotSpeed = 20f;
    public float proximityMin = 0.5f;

    public int hp = 20;//To be used
    public int bulletDmg = 10;
    public int bulletSpeed = 2;
    public float fireRate = 1f;
    public float maxVisibilityRange = 10;
    public float maxAttackRange = 10;

    public Color selectedColor = new Color(0, 1, 0, 1);
    public Color defaultColor = new Color(1, 1, 1, 1);

    public LayerMask whatToHit;
    public Transform bulletPrefab;
    private Transform GunFlash;
    private Transform firePoint;
    private Transform bullet;

    private GameObject Target;
    private Vector2 rotTarget;
    private bool isAttacking = false;
    public int rotationOffset = 0;
    private float timeToFire = 0;

    private Vector2 movVec;
    private Vector2 movTarg;

    private SpriteRenderer mySprite;

	void Start ()
    {

        mySprite = this.GetComponent<SpriteRenderer>();

        movTarg = new Vector2(transform.position.x, transform.position.y);

        firePoint = transform.Find("Fire Point");
        if (firePoint == null)
        {
            Debug.Log("Weapon not found as child for this script!");
        }

        GunFlash = transform.Find("GunFlash");
        if (GunFlash == null)
        {
            Debug.Log("GunFlash not found as child for this script!");
        }


    }

    public GameObject Select()
    {
        mySprite.color = selectedColor;

        return this.gameObject;
    }

    public void DeSelect()
    {
        mySprite.color = defaultColor;
    }

    public void MoveTo(Vector2 target)
    {
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        movTarg = target;
        rotTarget = movTarg;
        Target = null;
        isAttacking = false;
        movVec = (target - myPos).normalized;

    }

    public void Attack(GameObject target)
    {
        Target = target;
        rotTarget = new Vector2(Target.transform.position.x, Target.transform.position.y);
        isAttacking = true;
    }

    public void GetAttacked(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag == "Enemy")
        {
            if(collision.transform.tag == "Player" || collision.transform.tag == "Barracks")
            {
                Attack(collision.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            GetAttacked(collision.transform.GetComponent<BulletLogic>().dmg);
        }
    }

    void Update ()
    {



        Vector2 position = new Vector2(transform.position.x, transform.position.y);
       
        Vector2 difference = rotTarget - position;
        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + rotationOffset);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, rotationZ + rotationOffset), rotSpeed * Time.deltaTime);


        if ( isAttacking && transform.rotation == Quaternion.Euler(0f, 0f, rotationZ + rotationOffset) && Vector2.Distance(position, rotTarget) <= maxVisibilityRange && Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, 90));
            bullet.GetComponent<BulletLogic>().movSpeed = (int)(bulletSpeed + movSpeed);
            bullet.GetComponent<BulletLogic>().dmg = bulletDmg;
            bullet.GetComponent<BulletLogic>().maxRange = maxAttackRange;
            GunFlash.GetComponent<ParticleSystem>().Play();
        }


        if (Mathf.Abs(movTarg.x - transform.position.x) > proximityMin || Mathf.Abs(movTarg.y - transform.position.y) > proximityMin)
        {

            Vector2 currPosTmp = new Vector2(transform.position.x, transform.position.y);
            currPosTmp += movVec * movSpeed * Time.deltaTime;

            Vector3 tmp = new Vector3(currPosTmp.x, currPosTmp.y, transform.position.z);
            transform.position = tmp;
        }
        else
        {
            movTarg = new Vector2(transform.position.x, transform.position.y);
        }

        if(Target != null)
        {
            Attack(Target);
        }

    }
}
