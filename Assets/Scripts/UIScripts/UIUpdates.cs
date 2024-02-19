using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdates : MonoBehaviour
{
    private GameObject player;

    WarriorStatManager warriorStatScript;

    public GameObject healthBar;

    public Slider healthBarSlider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        warriorStatScript = player.GetComponent<WarriorStatManager>();
        healthBarSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarSlider.value = warriorStatScript.health;
    }
}