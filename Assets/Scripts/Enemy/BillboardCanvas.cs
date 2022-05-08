using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillboardCanvas : MonoBehaviour
{
    public Transform cam;
    public Slider healthSlider;
    public Slider orangeHealthSlider;
    public float fillSpeed;
    public EnemyStats enemyStats;
    private bool startLerping;
    private HealthbarBehaviour healthbarBehaviour;

    void Awake()
    {
        healthbarBehaviour = GetComponent<HealthbarBehaviour>();
        SetMaxHealth();
    }

    void Start() 
    {
        StartCoroutine(LateStartToSetSecondBarHP());
        //SetMaxHealth();
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
        
    }

    void Update() 
    {
        healthSlider.value = enemyStats.enemyHealth;
        
        if(orangeHealthSlider.value != healthSlider.value)
        {
            startLerping = true; 
        }
        else if(orangeHealthSlider.value == healthSlider.value)
        {
            startLerping = false;
        }
        
        if(orangeHealthSlider.value == healthSlider.value && !startLerping)
        {
            healthbarBehaviour.secondColor = new Color(healthbarBehaviour.r2, healthbarBehaviour.g2, healthbarBehaviour.b2, 0f);
            healthbarBehaviour.secondImage.color = healthbarBehaviour.secondColor;
        }

        if(startLerping)
        {
            healthbarBehaviour.secondColor = new Color(healthbarBehaviour.r2, healthbarBehaviour.g2, healthbarBehaviour.b2, 1f);
            healthbarBehaviour.secondImage.color = healthbarBehaviour.secondColor;
            orangeHealthSlider.value = Mathf.MoveTowards(orangeHealthSlider.value, healthSlider.value, Time.deltaTime * fillSpeed);
        }
    }

    public void SetMaxHealth()
    {
        healthSlider.maxValue = enemyStats.enemyMaxHealth;
        healthSlider.value = enemyStats.enemyHealth;
        orangeHealthSlider.maxValue = enemyStats.enemyMaxHealth;
        orangeHealthSlider.value = enemyStats.enemyHealth;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
    
    public IEnumerator LateStartToSetSecondBarHP()
    {
        yield return new WaitForSeconds(0.2f);
        orangeHealthSlider.value = enemyStats.enemyHealth;
        startLerping = true;
    }
}
