using UnityEngine;
using FMODUnity;

public class FmodEvents : MonoBehaviour {    //객체보단 구조체에 가까움
    [field : Header("Music")]
    [SerializeField] public EventReference TotalBGM;

    [field : Header("SFX")]
    [SerializeField] public EventReference bench_start;
    [SerializeField] public EventReference bench_end;
    [SerializeField] public EventReference item_buy;

    [field : Header("Ambience")]
    

    public static FmodEvents instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Start 메서드 제거
    }
}
