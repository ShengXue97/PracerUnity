using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSafe : MonoBehaviour {

    private ArrayList blocka = new ArrayList { "b1", "b2", "b3", "b4", "brick", "finish", "ice", "itemonce", "leftarrow", "rightarrow", "uparrow", "downarrow", "bomb", "crumble", "vanish", "move", "rotateleft", "rotateright", "push", "happy", "sad", "net", "heart", "time" };
    public GameObject safespot;
    public Transform playerTransform;
    public int depth = -20;
    void Start()
    {
    }

    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + new Vector3(0, 0, depth);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        GameObject other = col.gameObject;
        if (blocka.Contains(other.tag))
        {
        }
        else
        {

        }
    }

}
