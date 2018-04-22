using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour {

    public int dmg;
    public int movSpeed = 2;
    public float maxRange = 10f;

    private float tmp = 0f;
    private float curr = 0f;

    void Update()
    {
        tmp = movSpeed * Time.deltaTime;

        transform.Translate(Vector3.down * tmp);
        curr += tmp;

        if (curr >= maxRange)
            Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
