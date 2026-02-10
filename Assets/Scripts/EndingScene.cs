using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingScene : MonoBehaviour
{
    [SerializeField] private GameObject scene1;
    [SerializeField] private GameObject scene2;
    [SerializeField] private GameObject scene3;
    [SerializeField] private TextMeshProUGUI scene3text;


    private Image scene1Image;
    private Image scene2Image;
    private Image scene3Image;

    private void Start()
    {
        scene1Image = scene1.GetComponent<Image>();
        scene2Image = scene2.GetComponent<Image>();
        scene3Image = scene3.GetComponent<Image>();
        Color col1 = scene1Image.color;
        Color col2 = scene2Image.color;
        Color col3 = scene3Image.color;
        col1.a = 0f;
        col2.a = 0f;
        col3.a = 0f;
        scene1Image.color = col1;
        scene2Image.color = col2;
        scene3Image.color = col3;
        var scene3textColor = scene3text.color;
        scene3textColor.a = 0f;
        scene3text.color = scene3textColor;
        StartCoroutine(cartoon());
        
    }

    private IEnumerator cartoon(){
        Color col1 = scene1Image.color;
        Color col2 = scene2Image.color;
        Color col3 = scene3Image.color;

        while(scene1Image.color.a < 1f){
            col1.a += 0.0025f;
            scene1Image.color = col1;
            yield return null;
        }

        while(scene2Image.color.a < 1f){
            col2.a += 0.0025f;
            scene2Image.color = col2;
            yield return null;
        }

        var scene3textColor = scene3text.color;
        while(scene3Image.color.a < 1f){
            col3.a += 0.0025f;
            scene3textColor.a += 0.0025f;
            scene3text.color = scene3textColor;
            scene3Image.color = col3;
            yield return null;
        }
    }
}
