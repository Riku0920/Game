using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.ThirdPerson;

public class TalkController : MonoBehaviour
{
    private Animator anim; //�A�j���[�V������~�p
    private ThirdPersonUserControl tpuc; //�A�j���[�V������~�p
    private FPSController fps;
    private bool s_fps = false;

    //�@�L�����N�^�[�̑��x
    private Vector3 velocity;

    public bool talk;
    public enum State
    {
        Normal,
        Talk
    }

    //�@���j�e�B�����̏��
    private State state;
    //�@���j�e�B������b�����X�N���v�g
    private TalkManager talkManager;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        tpuc = GetComponent<ThirdPersonUserControl>();
        fps = GetComponent<FPSController>();

        state = State.Normal;

        talkManager = GetComponent<TalkManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Normal)
        {
            if (talkManager.GetConversationPartner() != null
                && Input.GetKeyDown(KeyCode.Space)
                )
            {
                SetState(State.Talk);
            }
        }
        else if (state == State.Talk)
        {

        }

        velocity.y += Physics.gravity.y * Time.deltaTime;

    }
    public void SetState(State state)
    {
        this.state = state;

        if (state == State.Talk)
        {
            anim.SetFloat("Forward", 0.0f);
            anim.SetFloat("Turn", 0.0f);

            if(fps.enabled == true)
            {
                fps.enabled = false;
                s_fps = true;
            }
            talk = true;
            tpuc.GetComponent<ThirdPersonUserControl>().enabled = false;
            
            talkManager.StartTalking();
        }
        else
        {
            talk = false;
            tpuc.enabled = true;
            if(s_fps)
            {
                fps.enabled = true;
                s_fps = false;
            }
        }
    }
    public State GetState()
    {
        return state;
    }

}