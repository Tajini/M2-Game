using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCountOpenBossMap : MonoBehaviour
{
    public Transform Level;
    public GameObject Porte1,Porte2,Porte3,Porte4,Porte5,Porte6,Porte7,Porte8,PorteBoss;
    public int lockspam;
    // Start is called before the first frame update
    void Start()
    {
        lockspam = 0;
        Level = GameObject.Find("Level").transform;
        Porte1 = Level.GetChild(0).gameObject;
        Porte2 = Level.GetChild(1).gameObject;
        Porte3 = Level.GetChild(2).gameObject;
        Porte4 = Level.GetChild(3).gameObject;
        Porte5 = Level.GetChild(4).gameObject;
        Porte6 = Level.GetChild(5).gameObject;
        Porte7 = Level.GetChild(6).gameObject;
        Porte8 = Level.GetChild(7).gameObject;
        PorteBoss = Level.GetChild(8).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockspam < 1 && this.transform.GetChild(0).childCount == 0)
        {
            Destroy(Porte1, 2f);
            lockspam = 1;
        }
        if (lockspam < 2 && this.transform.GetChild(1).childCount == 1)
        {
            Destroy(Porte2, 2f);
            lockspam = 2;
        }
        if (lockspam < 3 && this.transform.GetChild(1).childCount == 0)
        {
            Destroy(Porte3, 2f);
            lockspam = 3;
        }
        if (lockspam < 4 && this.transform.GetChild(2).childCount == 1)
        {
            Destroy(Porte4, 2f);
            lockspam = 4;
        }
        if (lockspam < 5 && this.transform.GetChild(2).childCount == 0)
        {
            Destroy(Porte5, 2f);
            lockspam = 5;
        }
        if ( lockspam < 6 && this.transform.GetChild(3).childCount == 1)
        {
            Destroy(Porte6, 2f);
            lockspam = 6;
        }
        if (lockspam < 7 && this.transform.GetChild(3).childCount == 0 )
        {
            Destroy(Porte7, 2f);
            lockspam = 7;
        }
        if (lockspam < 8 && this.transform.GetChild(4).childCount == 1)
        {
            Destroy(Porte8, 2f);
            lockspam = 8;
        }
        if (lockspam < 9 && this.transform.GetChild(4).childCount == 0)
        {
            Destroy(PorteBoss, 2f);
            lockspam = 9;
        }
    }
}
