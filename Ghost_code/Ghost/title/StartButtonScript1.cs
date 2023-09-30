using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript1 : MonoBehaviour
{
    AudioClip clip;
    AudioSource audio;
    bool maveFlag;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        clip = gameObject.GetComponent<AudioSource>().clip;

        maveFlag = false;   //�ړ��{�^���͉�����Ă��Ȃ�
    }
    void Update()
    {
        //�ړ��{�^����������Ă���@and
        //���̍Đ����I����Ă�����ړ�
        if (maveFlag == true && audio.isPlaying == false)
        {
            SceneManager.LoadScene("setting");
        }
    }

    public void OnClickStartButton()
    {
        audio.PlayOneShot(clip);
        maveFlag = true;    //�ړ��{�^���������ꂽ
    }
}