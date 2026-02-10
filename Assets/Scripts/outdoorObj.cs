using UnityEngine;

public class outdoorObj : MonoBehaviour
{

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = pos.x - 0.005f;
        transform.position = pos;
    }
}
