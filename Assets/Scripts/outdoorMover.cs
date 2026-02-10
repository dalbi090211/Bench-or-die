using UnityEngine;

public class outdoorMover : MonoBehaviour
{
    [SerializeField] private GameObject startNode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("outdoor")){
            GameObject obj = collision.gameObject;
            obj.transform.position = startNode.transform.position;
        }
    }


}
