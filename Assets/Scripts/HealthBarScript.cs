using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Image healthBarImage;
    // Start is called before the first frame update
    void Start()
    {
        healthBarImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealthBarValue(float value)
    {
        healthBarImage.fillAmount = value;
        if (healthBarImage.fillAmount < 0.2f)
        {
            SetHealthBarColor(Color.red);
        }
        else if (healthBarImage.fillAmount < 0.4f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }
    }

    public float GetHealthBarValue()
    {
        return healthBarImage.fillAmount;
    }

    public void SetHealthBarColor(Color healthColor)
    {
        healthBarImage.color = healthColor;
    }
}
