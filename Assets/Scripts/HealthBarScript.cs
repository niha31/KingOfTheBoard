using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public float health = 100;
    float maxHealth = 100;
    public GameObject bar;
    public  GameObject barSprite;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    public void Update()
    {
        if(health >= 50)
        {
            SetColour(Color.green);
        }
        else
        {
            SetColour(Color.yellow);
        }

        if (health <= 0)
        {
            health = 0;
        }
        
        float normalizedSize = health / maxHealth;
        SetSize(normalizedSize);
    }

    

    public void SetSize(float sizeNormalised)
    {
        bar.GetComponent<Transform>().localScale = new Vector3(sizeNormalised, 1.0f, 1.0f);
    }

    public void SetColour(Color colour)
    {
        barSprite.GetComponent<SpriteRenderer>().color = colour;
    }

}
