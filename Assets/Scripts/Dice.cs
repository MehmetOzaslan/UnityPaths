using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dice : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Sprite[] sprites;

    public UnityEvent Rolled; 

    public int Roll()
    {
        int roll = Random.Range(1, 7);
        spriteRenderer.sprite = sprites[roll-1];
        Rolled.Invoke();
        return roll;
    }


    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("DiceSimple");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Roll();
        }   
    }
}
