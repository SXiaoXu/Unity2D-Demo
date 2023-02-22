using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 声明一个刚体变量
    public Rigidbody2D rb;
    // 跑动起来的动画
    public Animator anim;

    // 声明一个速度变量，
    public float speed;
    // 声明一个跳跃强度
    public float jumpforce;

    
    // 游戏开始的时候调用
    void Start()
    {
        
    }

    // Update is called once per frame，
    // 每一帧都会调用 Update()，因为有的电脑达不到每一帧更新一次的运行效果，可用 FixedUpdate() 代替，使运行更平滑，使不同帧数电脑在操作手感上保持一致。
    void FixedUpdate()
    {
        movement();
    }

    // 设置一个移动函数，在每一帧产生画面效果
    void movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
                
        //返回三个值，0,1和-1。 -1左边移动，1右边移动，0不动
        float  facedirection = Input .GetAxisRaw("Horizontal");
       //角色移动，让狐狸跑起来    
       if(horizontalmove != 0)
       {
        // rd.velocity = new Vector2(horizontalmove * speed, rd.velocity.y);
        // Time.fixedDeltaTime 这样相比上面比保证更平滑不跳帧
        rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, rb.velocity.y);
        anim.SetFloat("running", Mathf.Abs(facedirection));
       }
        //左右方向跑的时候，狐狸切换朝向
        if(facedirection != 0)
        {
            // Vector3：三个维度
            transform.localScale = new Vector3(facedirection,1,1);
        } 
        // 角色跳跃
        if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
        }
    }
   
}


