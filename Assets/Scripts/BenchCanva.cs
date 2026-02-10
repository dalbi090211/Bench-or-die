using System;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BenchCanva : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthPower;
    [SerializeField] private Slider stressBar;
    [SerializeField] private GameObject cat;
    [SerializeField] private GameObject sunpung;
    [SerializeField] private GameObject aircon;
    [SerializeField] private GameObject clock;
    [SerializeField] private GameObject poster;

    [SerializeField] private GameObject outdoorCanva1;
    [SerializeField] private GameObject outdoorCanva2;
    [SerializeField] private GameObject outdoorCanva3;

    [SerializeField] private Light2D spotLight;
    [SerializeField] private Light2D globalLight;
    [SerializeField] private CinemachineCamera cvc1;
    [SerializeField] private CinemachineCamera cvc2;
    [SerializeField] private CinemachineCamera cvc3;
    
    
    public static BenchCanva instance;
    private void Awake()
    {
        if(instance != null) {
            Debug.Log("exception occured");
            Destroy(gameObject);
        }
        else {
            instance = this;
            StartCoroutine(waitInit());
        }
    }

    private IEnumerator waitInit(){
        while(EntireManager.instance == null){
            yield return null;
        }
        updateHp();
        setVisibleObj();
        outdoorCanva1.SetActive(true);
        outdoorCanva2.SetActive(false);
        outdoorCanva3.SetActive(false);

        globalLight.color = new Color(1f, 1f, 1f);
        spotLight.color = new Color(1f, 1f, 1f);
        spotLight.intensity = 0.15f;
        
    }

    private void OnDisable()
    {
        instance = null;
    }

    public void updateHp(){ //시간나면 애니메이션 넣을거라 따로 팜
        healthPower.text = EntireManager.instance.healthPower.ToString();
    }

    public void setStressBar(int level, float timer){
        float setValue = (level + timer/EntireManager.instance.levelConvertTimer)*0.15f;
        stressBar.value = setValue;
        if(setValue > 0.91f){
            Die();
        }
    }

    private void Die(){
        
    }

    public void chgOutdoor(int dayLevel){
        if(dayLevel == 1){
            outdoorCanva1.SetActive(false);
            outdoorCanva2.SetActive(true);
            outdoorCanva3.SetActive(false);
            globalLight.color = new Color(0.8862f, 0.8862f, 0.8862f);
            spotLight.intensity = 0.2f;
            cvc1.Priority = 0;
            cvc2.Priority = 5;
        }
        else if(dayLevel == 2){
            outdoorCanva1.SetActive(false);
            outdoorCanva2.SetActive(false);
            outdoorCanva3.SetActive(true);
            globalLight.color = new Color(0.5647f, 0.5647f, 0.5647f);
            spotLight.color = new Color(0.7764f, 0.7254f, 1f);
            spotLight.intensity = 0.4f;
            cvc2.Priority = 0;
            cvc3.Priority = 5;
        }
        else{
            outdoorCanva1.SetActive(true);
            outdoorCanva2.SetActive(false);
            outdoorCanva3.SetActive(false);
            globalLight.color = new Color(1f, 1f, 1f);
            spotLight.color = new Color(1f, 1f, 1f);
            spotLight.intensity = 0.15f;
            cvc3.Priority = 0;
            cvc1.Priority = 5;
        }
    }

    private void setVisibleObj(){
        if(EntireManager.instance.priceLevel[0] > 0) cat.SetActive(true);
        else cat.SetActive(false);
        if(EntireManager.instance.priceLevel[1] > 0) sunpung.SetActive(true);
        else sunpung.SetActive(false);
        if(EntireManager.instance.priceLevel[2] > 0) aircon.SetActive(true);
        else aircon.SetActive(false);
        if(EntireManager.instance.priceLevel[3] > 0) clock.SetActive(true);
        else clock.SetActive(false);
        if(EntireManager.instance.priceLevel[4] > 0) poster.SetActive(true); 
        else poster.SetActive(false);
    }
}
