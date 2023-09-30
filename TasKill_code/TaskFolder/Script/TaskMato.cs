using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMato : MonoBehaviour
{
    public float MatoHp;
    // Start is called before the first frame update

    public void DamageMato(bool my)
    {
        if (!my)
        {
            return;
        }
        MatoHp = MatoHp - 1;
        if(MatoHp <= 0)
        {
            GameObject.FindWithTag("Player").GetComponent<Taskmanagement>().Mato();
            Destroy(this.gameObject);
        }
    }
}
