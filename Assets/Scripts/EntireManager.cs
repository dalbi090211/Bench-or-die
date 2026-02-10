using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class EntireManager : MonoBehaviour
{
    public static EntireManager instance;

    public float levelConvertTimer = 0.9f;
    public int healthPower;
    public int[] prices = new int[5] {40, 100, 40, 60, 80};
    public int[] priceLevel = new int[5] {0, 0, 0, 0, 0};
    public float randomDecreaseLevelRatio = 0.03f;
    public float levelPowerRatio = 0.1f;
    public int leftDay = 7;
    public int maxHP = 10000;
    
    private void Awake(){
        if(instance == null){
            DontDestroyOnLoad(gameObject);
            instance = this;
            healthPower = 0;
        }
        else if(instance != null)
            Destroy(gameObject);
    }

    public void restartGame(){
        healthPower = 0;
        priceLevel = new int[5] {0, 0, 0, 0, 0};
        prices = new int[5] {40, 100, 40, 60, 80};
        levelConvertTimer = 0.9f;
        randomDecreaseLevelRatio = 0.03f;
        levelPowerRatio = 0.1f;
        leftDay = 7;
        maxHP = 10000;
    }

    public Boolean gainHealthPower(int benchLevel){
        healthPower += Mathf.RoundToInt(Mathf.Pow(benchLevel, 1f + levelPowerRatio*(priceLevel[2] + 4*priceLevel[4]))) + 2*priceLevel[0];

        if(priceLevel[1] > 0){
            if(Random.Range(0f, 1f) < randomDecreaseLevelRatio) return true;
            else return false;
        }
        else return false;
    }

    public float sqrt(int dec){
        if (dec > 1) return 1.2f*sqrt(dec-1);
        else return 1f;
    }

}
