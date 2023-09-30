using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGeneration : MonoBehaviour
{
    public GameObject coin;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject CoinIns = Instantiate(coin,new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.identity);
        CoinIns.transform.parent = this.gameObject.transform;
    }
}
