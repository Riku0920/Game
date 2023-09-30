using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPointPosScript : MonoBehaviour
{
    [SerializeField] GameObject[] CoinGamePos;
    // Start is called before the first frame update

    int start = 0;
    int end = 100;
    public int count = 8;

    List<int> numbers = new List<int>();

    void Start()
    {
        end = CoinGamePos.Length - 1;
        for (int i = start; i <= end; i++)
        {
            numbers.Add(i);
        }

        while (count-- > 0)
        {
            //�����łƂ����ꏊ�ɃR�C���o���A���̏ꏊ�̗������폜
            int index = Random.Range(0, numbers.Count);
            int ransu = numbers[index];
            numbers.RemoveAt(index);
            CoinGamePos[ransu].SetActive(true);
        }
    }
}
