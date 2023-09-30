using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class characterchenge : MonoBehaviour
{
    [SerializeField] private GameObject meid;
    [SerializeField] private GameObject sitsuji;
    [SerializeField] private GameObject haha;
    [SerializeField] private GameObject titi;
    [SerializeField] private GameObject kodomo1;
    [SerializeField] private GameObject kodomo2;
    [SerializeField] private GameObject Obake;
    [SerializeField] private GameObject ObakeHontai;
    [SerializeField] private GameObject Efect;


    [SerializeField] private float staminum = 100f;

    public GameObject StaminumSlider;
    public Slider Slider;


    bool set = true;
    public bool talk = false;
    private TalkController tt;
    // Start is called before the first frame updatepon
    void Start()
    {
        tt = Obake.GetComponent<TalkController>();
        reset();
        Slider = StaminumSlider.transform.Find("StaminumSliders").GetComponent<Slider>();
        Slider.value = staminum;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _charactor();
        }
        talk = tt.talk;
        if(set == true)
        {
            if (talk == false)
            {
                Slider.value -= 0.0005f;
                if (Slider.value <= 0.0f)
                {
                    reset();
                }
            }
        }
        
        else
        {
            Slider.value += 0.0005f;
        }
    }
    void reset()
    {
        set = false;
        _Obake_on();
        _meid_off();
        _sitsuji_off();
        _hahaoya_off();
        _titioya_off();
        _kodomo1_off();
        _kodomo2_off();
        Efect.SetActive(false);
        ObakeHontai.GetComponent<MeshRenderer>().enabled = true;

    }
    void _charactor()
    {
        Ray ray = new Ray(Obake.transform.position, Obake.transform.forward);
        RaycastHit hit;
        bool cast = Physics.Raycast(ray, out hit, 15.0f);
        if (set == true)
        {
            reset();
        }
        else if(hit.collider != null)
        {
            if(hit.transform.gameObject.tag == "NPC" && Slider.value >= 0.9)
            {
                Efect.SetActive(true);

                if (hit.transform.gameObject.name == meid.gameObject.name)
                {
                    set = true;
                    _Obake_off();
                    _meid_on();
                    meid.transform.position = Obake.transform.position;
                    Obake.transform.rotation = meid.transform.rotation;
                    meid.transform.SetParent(Obake.transform);

                    ObakeHontai.GetComponent<MeshRenderer>().enabled = false;

                    Vector3 pos = meid.transform.position;
                    pos.y -= 3.5f;
                    meid.transform.position = pos;

                }
                
                if (hit.transform.gameObject.name == sitsuji.gameObject.name)
                {
                    
                    set = true;
                    _sitsuji_on();
                    _Obake_off();
                    sitsuji.transform.position = Obake.transform.position;
                    Obake.transform.rotation = sitsuji.transform.rotation;
                    sitsuji.transform.SetParent(Obake.transform);

                    ObakeHontai.GetComponent<MeshRenderer>().enabled = false;

                    Vector3 pos = sitsuji.transform.position;
                    pos.y -= 3.5f;
                    sitsuji.transform.position = pos;
                }

                if (hit.transform.gameObject.name == haha.gameObject.name)
                {
                    set = true;
                    _hahaoya_on();
                    _Obake_off();
                    haha.transform.position = Obake.transform.position;
                    Obake.transform.rotation = haha.transform.rotation;
                    haha.transform.SetParent(Obake.transform);

                    ObakeHontai.GetComponent<MeshRenderer>().enabled = false;

                    Vector3 pos = haha.transform.position;
                    pos.y -= 3.5f;
                    haha.transform.position = pos;
                }
                if (hit.transform.gameObject.name == titi.gameObject.name)
                {
                    set = true;
                    _titioya_on();
                    _Obake_off();
                    titi.transform.position = Obake.transform.position;
                    Obake.transform.rotation = titi.transform.rotation;
                    titi.transform.SetParent(Obake.transform);

                    ObakeHontai.GetComponent<MeshRenderer>().enabled = false;

                    Vector3 pos = titi.transform.position;
                    pos.y -= 3.5f;
                    titi.transform.position = pos;
                    
                }
                else if (hit.transform.gameObject.name == kodomo1.gameObject.name)
                {
                    set = true;
                    _kodomo1_on();
                    _Obake_off();
                    kodomo1.transform.position = Obake.transform.position;
                    Obake.transform.rotation = kodomo1.transform.rotation;
                    kodomo1.transform.SetParent(Obake.transform);

                    ObakeHontai.GetComponent<MeshRenderer>().enabled = false;

                    Vector3 pos = kodomo1.transform.position;
                    pos.y -= 3.5f;
                    kodomo1.transform.position = pos;
                    
                }
                else if (hit.transform.gameObject.name == kodomo2.gameObject.name)
                {
                    set = true;
                    _kodomo2_on();
                    _Obake_off();
                    kodomo2.transform.position = Obake.transform.position;
                    Obake.transform.rotation = kodomo2.transform.rotation;
                    kodomo2.transform.SetParent(Obake.transform);

                    ObakeHontai.GetComponent<MeshRenderer>().enabled = false;

                    Vector3 pos = kodomo2.transform.position;
                    pos.y -= 3.5f;
                    kodomo2.transform.position = pos;
                    
                }
            }
        }
    }
    void _Obake_on()
    {
        //Obake.SetActive(true);
    }
    void _Obake_off()
    {
        //Obake.SetActive(false);
    }
    void _meid_on()
    {
        meid.GetComponent<NPCManager>().enabled = false;
        meid.GetComponent<NavMeshAgent>().enabled = false;
        
    }
    void _sitsuji_on()
    {
        sitsuji.GetComponent<NPCManager>().enabled = false;
        sitsuji.GetComponent<NavMeshAgent>().enabled = false;
    }
    void _hahaoya_on()
    {
        haha.GetComponent<NPCManager>().enabled = false;
        haha.GetComponent<NavMeshAgent>().enabled = false;
    }
    void _titioya_on()
    {
        titi.GetComponent<NPCManager>().enabled = false;
        titi.GetComponent<NavMeshAgent>().enabled = false;
    }
    void _kodomo1_on()
    {
        kodomo1.GetComponent<NPCManager>().enabled = false;
        kodomo1.GetComponent<NavMeshAgent>().enabled = false;
    }
    void _kodomo2_on()
    {
        kodomo2.GetComponent<NPCManager>().enabled = false;
        kodomo2.GetComponent<NavMeshAgent>().enabled = false;
    }
    void _meid_off()
    {
        meid.transform.SetParent(null);
        meid.GetComponent<NPCManager>().enabled = true;
        meid.GetComponent<NavMeshAgent>().enabled = true;
    }
    void _sitsuji_off()
    {
        sitsuji.transform.SetParent(null);
        sitsuji.GetComponent<NPCManager>().enabled = true;
        sitsuji.GetComponent<NavMeshAgent>().enabled = true;
    }
    void _hahaoya_off()
    {
        haha.transform.SetParent(null);
        haha.GetComponent<NPCManager>().enabled = true;
        haha.GetComponent<NavMeshAgent>().enabled = true;
    }
    void _titioya_off()
    {
        titi.transform.SetParent(null);
        titi.GetComponent<NPCManager>().enabled = true;
        titi.GetComponent<NavMeshAgent>().enabled = true;
    }
    void _kodomo1_off()
    {
        kodomo1.transform.SetParent(null);
        kodomo1.GetComponent<NPCManager>().enabled = true;
        kodomo1.GetComponent<NavMeshAgent>().enabled = true;
    }
    void _kodomo2_off()
    {
        kodomo2.transform.SetParent(null);
        kodomo2.GetComponent<NPCManager>().enabled = true;
        kodomo2.GetComponent<NavMeshAgent>().enabled = true;
    }
}
