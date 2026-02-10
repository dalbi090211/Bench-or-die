using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreCanva : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthPower;

    [SerializeField] private TextMeshProUGUI catPrice;
    [SerializeField] private TextMeshProUGUI sunpungPrice;
    [SerializeField] private TextMeshProUGUI airconPrice;
    [SerializeField] private TextMeshProUGUI clockPrice;
    [SerializeField] private TextMeshProUGUI posterPrice;

    [SerializeField] private TextMeshProUGUI catLevel;
    [SerializeField] private TextMeshProUGUI sunpungLevel;
    [SerializeField] private TextMeshProUGUI airconLevel;
    [SerializeField] private TextMeshProUGUI clockLevel;
    [SerializeField] private TextMeshProUGUI posterLevel;
    [SerializeField] private TextMeshProUGUI leftDays;

    public static StoreCanva instance;
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
        leftDays.text = EntireManager.instance.leftDay.ToString() + " days left";
        updateHp();
        for(int i = 0; i < 5; i++){
            updatePrice(i);
        }
        for(int i = 0; i < 5; i++){
            updateLevel(i);
        }
    }

    private void OnDisable()
    {
        instance = null;
    }

    private void upcrisingPrice(int index, float latency){
        SoundManager.instance.PlayOneShot(FmodEvents.instance.item_buy, transform.position);
        EntireManager.instance.prices[index] = (int)(EntireManager.instance.prices[index] * latency);
        EntireManager.instance.priceLevel[index] += 1;
        updatePrice(index);
    }

    private void updatePrice(int index){
        switch(index){
            case 0:
                catPrice.text = EntireManager.instance.prices[index].ToString();
                break;
            case 1:
                sunpungPrice.text = EntireManager.instance.prices[index].ToString();
                break;
            case 2:
                airconPrice.text = EntireManager.instance.prices[index].ToString();
                break;
            case 3:
                clockPrice.text = EntireManager.instance.prices[index].ToString();
                break;
            case 4:
                posterPrice.text = EntireManager.instance.prices[index].ToString();
                break;
        }
    }

    private void updateLevel(int index){
        switch(index){
            case 0:
                catLevel.text = "lv " + EntireManager.instance.priceLevel[index].ToString() + ".";
                break;
            case 1:
                sunpungLevel.text = "lv " + EntireManager.instance.priceLevel[index].ToString() + ".";
                break;
            case 2:
                airconLevel.text = "lv " + EntireManager.instance.priceLevel[index].ToString() + ".";
                break;
            case 3:
                clockLevel.text = "lv " + EntireManager.instance.priceLevel[index].ToString() + ".";
                break;
            case 4:
                posterLevel.text = "lv " + EntireManager.instance.priceLevel[index].ToString() + ".";
                break;
        }
    }


    public void updateHp(){ //시간나면 애니메이션 넣을거라 따로 팜
        healthPower.text = EntireManager.instance.healthPower.ToString();
    }

    public void OnclickBuyCat(){
        if(EntireManager.instance.healthPower >= EntireManager.instance.prices[0]){
            EntireManager.instance.healthPower -= EntireManager.instance.prices[0];
            upcrisingPrice(0, 1.5f);
            updateHp();
            updateLevel(0);
        }
        else{
            Debug.Log("not enough money");
        }
    }

    public void OnclickBuySunpung(){
        if(EntireManager.instance.healthPower >= EntireManager.instance.prices[1]){
            EntireManager.instance.healthPower -= EntireManager.instance.prices[1];
            upcrisingPrice(1, 2f);
            updateHp();
            updateLevel(1);
            if(EntireManager.instance.priceLevel[1] > 1) EntireManager.instance.randomDecreaseLevelRatio = EntireManager.instance.randomDecreaseLevelRatio * 1.4f;
        }
        else{
            Debug.Log("not enough money");
        }
    }

    public void OnclickBuyAircon(){
        if(EntireManager.instance.healthPower >= EntireManager.instance.prices[2]){
            EntireManager.instance.healthPower -= EntireManager.instance.prices[2];
            upcrisingPrice(2, 1.4f);
            updateHp();
            updateLevel(2);
        }
        else{
            Debug.Log("not enough money");
        }
    }

    public void OnclickBuyClock(){
        if(EntireManager.instance.healthPower >= EntireManager.instance.prices[3]){
            EntireManager.instance.healthPower -= EntireManager.instance.prices[3];
            upcrisingPrice(3, 1.5f);
            updateHp();
            updateLevel(3);
            EntireManager.instance.levelConvertTimer += 0.2f;
        }
        else{
            Debug.Log("not enough money");
        }
    }

    public void OnclickBuyPoster(){
        if(EntireManager.instance.healthPower >= EntireManager.instance.prices[4]){
            EntireManager.instance.healthPower -= EntireManager.instance.prices[4];
            upcrisingPrice(4, 1.6f);
            updateHp();
            updateLevel(4);
            EntireManager.instance.levelConvertTimer -= 0.1f;
        }
        else{
            Debug.Log("not enough money");
        }
    }

    public void OnclickExit(){
        SceneManager.LoadScene("BenchScene");
    }
}
