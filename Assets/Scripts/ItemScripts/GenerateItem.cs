using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateItem : MonoBehaviour
{
    //string itemTypes[];

    public GameObject fireSpellBook;

    public Rigidbody2D rb;

    public Animator anim;

    float startPosY;

    // Start is called before the first frame update
    void Start()
    {
        //itemTypes[] = { "Dragon Egg", "Spell Book", "Magic Artifact", "Potion", "Shield", "Sword" };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        anim.Play("OpenChestAnim");
        Instantiate(fireSpellBook);
    }
}
