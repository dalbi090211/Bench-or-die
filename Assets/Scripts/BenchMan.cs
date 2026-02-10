using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class BenchMan : MonoBehaviour
{
    private Coroutine timerRoutine;
    private bool isBench;
    private Animator anim;
    public int BenchLevel;
    public int DayLevel;
    [SerializeField] ParticleSystem ps;
    [SerializeField] Image[] DayLevelImg;
    [SerializeField] private Image DiedBg;
    [SerializeField] private TextMeshProUGUI DiedText;

    private void Awake()
    {
        if(BenchCanva.instance == null) Debug.Log("benchCanva not assigned");
        anim = this.GetComponent<Animator>();
        DayLevel = 0;
        ps.Stop();
        var emission = ps.emission;
        emission = ps.emission;
        emission.rateOverTime = 5f;
    }

    private void Update()
    {
        if(isBench){
            
        }
    }

    public void OnBenchPress(InputAction.CallbackContext context){
        if(context.performed){
            if(timerRoutine == null){
                isBench = true;
                upLevel(0);
                timerRoutine = StartCoroutine(Scoring());
                SoundManager.instance.PlayOneShot(FmodEvents.instance.bench_start, transform.position);
            }
        }
        else if(context.canceled){
            if(timerRoutine != null && BenchLevel != 6){
                // SoundManager.instance.PlayOneShot(FmodEvents.instance.bench_end, transform.position);
                isBench = false;
                StopCoroutine(timerRoutine);
                timerRoutine = null;
                if(DayLevel != 2) endBench();
                else cvtDayScene();
            }
        }
    }

    private IEnumerator Scoring(){
        float timer = 0f;
        float lastTimeMax = -0.05f;

        while(true){
            //time manage
            if(BenchLevel == 6){
                StartCoroutine(death()); 
            }
            else if(timer > EntireManager.instance.levelConvertTimer){
                timer -= EntireManager.instance.levelConvertTimer;
                lastTimeMax = -0.05f;
                upLevel(++BenchLevel);
            }

            if(lastTimeMax + 0.2f < timer && BenchLevel > 0){ //0.5초마다 체력획득 시도
                lastTimeMax += 0.2f;
                if(EntireManager.instance.gainHealthPower(BenchLevel)){
                    BenchLevel -= 1;
                }
                BenchCanva.instance.updateHp();
            }

            BenchCanva.instance.setStressBar(BenchLevel, timer);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void upLevel(int level){
        BenchLevel = level;
        if(BenchLevel == 3){
            ps.Play();
        }
        if(BenchLevel > 3){
            var emission = ps.emission;
            emission.rateOverTime = 5f + 0.5f*BenchLevel;
        }
        Debug.Log("current Level : " + level);
        anim.SetInteger("CurrentLevel", level);
    }

    private void endBench(){
        DayLevel++;
        upLevel(-1);
        ChgDayLevelImg();
        BenchCanva.instance.chgOutdoor(DayLevel);
        BenchCanva.instance.setStressBar(0, 0f);
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        var emission = ps.emission;
        emission.rateOverTime = 5f;
    }

    private void ChgDayLevelImg(){
        Debug.Log("day level : " + DayLevel);
        Color cvtColor;
        if(DayLevel != 0){
            cvtColor = DayLevelImg[DayLevel-1].color;
            cvtColor.a = 70/255f;
            DayLevelImg[DayLevel-1].color = cvtColor;
        }
        cvtColor = DayLevelImg[DayLevel].color;
        cvtColor.a = 1f;
        DayLevelImg[DayLevel].color = cvtColor;
    }

    private IEnumerator death(){
        EntireManager.instance.restartGame();

        while(DiedBg.color.a < 1f){
            Color cvtColor = DiedBg.color;
            cvtColor.a += Time.deltaTime/2f;
            DiedBg.color = cvtColor;
            cvtColor = DiedText.color;
            cvtColor.a += Time.deltaTime/2f;
            DiedText.color = cvtColor;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        DayLevel = 0;
        BenchLevel = 0;
        SceneManager.LoadScene("BenchScene");
    }

    private void cvtDayScene(){
        Debug.Log("change scene");
        SceneManager.LoadScene("StoreScene");
    }
}
