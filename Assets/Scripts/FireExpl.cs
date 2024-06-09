using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExpl : MonoBehaviour
{

    public Animator explAnim;

    public float damage = 10.0f;

    private EnemyBase enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        explAnim = GetComponent<Animator>();
        explAnim.Play("FireExplosionAnim");
        StartCoroutine(DestroyAfterAnimation());

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemyScript = enemy.GetComponent<EnemyBase>();
            if (enemyScript != null)
            {
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(explAnim.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger == false)
        {
            GameObject enemyType = collision.gameObject;
            enemyScript.EnemyTakeDamage(enemyType, damage);
        }
    }
}
