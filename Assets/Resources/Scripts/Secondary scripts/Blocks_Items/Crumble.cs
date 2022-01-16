using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : MonoBehaviour
{
    public ParticleSystem crumbly;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            print("rrg");

        }
    }
}