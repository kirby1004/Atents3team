using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class NpcMovement : MonoBehaviour
{
    #region NPCMovement 멤버 변수
    public enum NpcState //Npc 상태 
    {
        Create,Standing, Walk,Run, Disappear
    }
    protected Transform Tartget;
    public UnityAction RespawnSetting;
    public NpcState myState = NpcState.Create;

    float RotSpeed = 360.0f;
    float MoveSpeed = 3.0f;

    public static int WalkAbleCount = 0;
    public int Count = WalkAbleCount;
    private void Update()
    {
        Count = WalkAbleCount;
    }
    public Transform[] walkPoints;
    protected Transform Target;
    public Collider myCollider;

    private int destPoint = 0;
    public LayerMask mask;
    
    public Animator myAnimator;
    #endregion

    private void Start()
    {
        ChangeState(NpcState.Walk);
    }

    #region 상태 변환 함수
    virtual protected void ChangeState(NpcState ns)
    {
        if (myState == ns) return;
        myState = ns;
        switch (myState)
        {
            case NpcState.Walk:
                WalkAbleCount++;
                myAnimator.SetBool("Walking", true);
                StopAllCoroutines();
                GotoNextPoint();
                break;
            case NpcState.Run:
                StopAllCoroutines();
                Fear(Target);
                break;
            case NpcState.Disappear:
                StopAllCoroutines();
                Disappear();
                break;
        }
    }
    #endregion

    #region NPC 이동 관련 함수
    public void SetPoints(Transform[] p)
    {
        walkPoints = p;
    }

    void GotoNextPoint()
    {
        if (walkPoints.Length == 0)
            return;
        WalkToPos(walkPoints[destPoint].position, () => { GotoNextPoint(); });
        destPoint = (destPoint + 1) % walkPoints.Length;
    }

    protected void WalkToPos(Vector3 pos, UnityAction done = null)
    {
        StopAllCoroutines();
        StartCoroutine(WalkingToPos(pos, done));
    }

    IEnumerator WalkingToPos(Vector3 pos, UnityAction done)
    {
        yield return new WaitForEndOfFrame();
        Vector3 dir=Vector3.zero;
        float dist = 0.0f;
        switch (myState)
        {
            case NpcState.Walk:
                dir = pos - transform.position;
                dist = dir.magnitude;
                dir.Normalize();
                break;
            case NpcState.Run:
                dir = (pos - transform.position) * -1;
                dist = 20.0f;
                dir.Normalize();
                MoveSpeed = 3.5f;
                myAnimator.SetBool("Running", true);
                break;
        }

        StartCoroutine(Rotating(dir));

        while (dist > 0.0f)
        {


            float delta = MoveSpeed * Time.deltaTime;
            if (dist - delta < 0.0f)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(dir * delta, Space.World);

            yield return null;
        }
        done?.Invoke();
    }

    IEnumerator Rotating(Vector3 dir)
    {
        float angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;
        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir = -1.0f;
        }
        while (angle > 0.0f)
        {
            float delta = RotSpeed * Time.deltaTime;
            if (angle - delta < 0.0f)
            {
                delta = angle;
            }
            angle -= delta;
            transform.Rotate(Vector3.up * rotDir * delta);
            yield return null;
        }
    }
    #endregion

    #region NPC 상태 함수
    protected void Fear(Transform target)
    {
        StartCoroutine(WalkingToPos(target.position, () => { ChangeState(NpcState.Disappear); }));
        myCollider.enabled = false;
    }

    virtual protected void Disappear()
    {
        RespawnSetting?.Invoke();
        ObjectPoolingManager.instance.ReturnObject(gameObject);
        WalkAbleCount--;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & mask) != 0)
        {
            Debug.Log("Ddd");
            Target = other.transform;
            ChangeState(NpcState.Run);
        }
    }
    #endregion
}
