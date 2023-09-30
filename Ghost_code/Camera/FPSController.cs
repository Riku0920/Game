using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class FPSController : MonoBehaviour
{
    float x, z;
    [SerializeField]
    float speed = 0.2f;


    private ThirdPersonCharacter tpc;
    public GameObject cam;
    Quaternion cameraRot, characterRot;
    float Xsensityvity = 3f, Ysensityvity = 3f;

    bool cursorLock = true;

    //�ϐ��̐錾(�p�x�̐����p)
    float minX = -90f, maxX = 90f;

    // Start is called before the first frame update
    void Start()
    {
        tpc = GetComponent<ThirdPersonCharacter>();
        this.speed = 0.4f;
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * Ysensityvity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensityvity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        //Update�̒��ō쐬�����֐����Ă�
        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

    }

    private void FixedUpdate()
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;
        z = Input.GetAxisRaw("Vertical") * speed;

        //transform.position += new Vector3(x,0,z);

        transform.position += cam.transform.forward * z + cam.transform.right * x;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.isTrigger == false)//Collider��isTrigger�Ƀ`�F�b�N�������Ă��Ȃ��ꍇ
        {
                Debug.Log(speed);
                if (speed != 0)//�ǂ����蔲���Ȃ�����
            {
                    speed = 0f;
                    z = Input.GetAxisRaw("Vertical") * 0.40f;
                }

        }
       
    }
    void OnTriggerExit(Collider col)
    {
        speed = 0.4f;
    }

    //�p�x�����֐��̍쐬
    public Quaternion ClampRotation(Quaternion q)
    {
        //q = x,y,z,w (x,y,z�̓x�N�g���i�ʂƌ����j�Fw�̓X�J���[�i���W�Ƃ͖��֌W�̗ʁj)

        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }


}
