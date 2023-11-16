using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExpl : MonoBehaviour
{

    public Animator explAnim;

    // Start is called before the first frame update
    void Start()
    {
        explAnim = GetComponent<Animator>();
        explAnim.Play("FireExplosionAnim");
        StartCoroutine(DestroyAfterAnimation());
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
}
