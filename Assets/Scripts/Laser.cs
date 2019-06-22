using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public MobLaser moblaser;
    // Start is called before the first frame update
    void Start()
    {
        moblaser = this.transform.parent.parent.GetComponent<MobLaser>();   }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Heros")
        {
            moblaser.Lock();
        }
    }
}
