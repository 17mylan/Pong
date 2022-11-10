using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomize : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public Sprite RandomGadgetIce;
    //public Sprite RandomGadgetSpeed;
    public float randomValue;
    public void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        GameObject.Find("RandomGadget").GetComponent<SpriteRenderer>().sprite = RandomGadgetIce;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        RandomizePosition();
    }
    public void Start()
    {
        RandomizePosition();
    }
}
