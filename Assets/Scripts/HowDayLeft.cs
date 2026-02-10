using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HowDayLeft : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI middleText;
    [SerializeField] private TextMeshProUGUI rightText;
    [SerializeField] private TextMeshProUGUI howDayText;
    [SerializeField] private TextMeshProUGUI howHPText;
    [SerializeField] private Image healthImg;
    [SerializeField] private PlayerInput moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction.enabled = false;
        StartCoroutine(waitInit());
    }

    private IEnumerator waitInit(){
        while(EntireManager.instance == null){
            yield return null;
        }
        if(7 - EntireManager.instance.leftDay != 0)leftText.text = (7 - EntireManager.instance.leftDay).ToString();
        else leftText.text = "";
        EntireManager.instance.leftDay--;
        middleText.text = (7 - EntireManager.instance.leftDay).ToString();
        if(8 - EntireManager.instance.leftDay != 8) rightText.text = (8 - EntireManager.instance.leftDay).ToString();
        else rightText.text = "";
        howDayText.text = EntireManager.instance.leftDay.ToString() + " days left";
        howHPText.text = (EntireManager.instance.maxHP - EntireManager.instance.healthPower).ToString() + " left";

        if(EntireManager.instance.leftDay == 0 || EntireManager.instance.maxHP - EntireManager.instance.healthPower < 0) LoadEnding();
        else {
            float alphaTimer = 1.5f;
            float alpha = 1f;
            while(alphaTimer > 0f){
                alphaTimer -= Time.deltaTime;
                alpha = Mathf.Lerp(0f, 1f, alphaTimer);
                dayText.color = new Color(1f, 1f, 1f, alpha);
                leftText.color = new Color(1f, 1f, 1f, alpha);
                middleText.color = new Color(1f, 1f, 1f, alpha);
                rightText.color = new Color(1f, 1f, 1f, alpha);
                howDayText.color = new Color(1f, 1f, 1f, alpha);
                howHPText.color = new Color(1f, 1f, 1f, alpha);
                healthImg.color = new Color(1f, 1f, 1f, alpha);
                yield return null;
            }
            gameObject.SetActive(false);
        }
        moveAction.enabled = true;
    }


    private void LoadEnding(){
        SceneManager.LoadScene("EndingScene");
    }
}
