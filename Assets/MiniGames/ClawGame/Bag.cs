using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    //[SerializeField] private ClawGame crane;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Toy"))
        {
            //win
            collision.gameObject.SetActive(false);
        }
    }
}
