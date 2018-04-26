using UnityEngine;
using System.Collections;


public class MoveController : MonoBehaviour {


	private Animator ani;

	void Start()
	{
		ani = GetComponent<Animator> ();


	}



    void OnEnable()
    {
        EasyJoystick.On_JoystickMove += OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
    }

    void OnDisable()
    {
        EasyJoystick.On_JoystickMove -= OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
    }

    void OnDestroy()
    {
        EasyJoystick.On_JoystickMove -= OnJoystickMove;
        EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
    }


    void OnJoystickMoveEnd(MovingJoystick move)
    {
		if (move.joystickName == "MoveJoystick")
        {
//            GetComponent<Animation>().CrossFade("idle");
 
			ani.SetInteger ("state", AnimState.IDLE);

        }
    }
    void OnJoystickMove(MovingJoystick move)
    {
        if (move.joystickName != "MoveJoystick")
        {
            return;
        }
        

        float joyPositionX = move.joystickAxis.x;
        float joyPositionY = move.joystickAxis.y;

        if (joyPositionY != 0 || joyPositionX != 0)
        {
            //设置角色的朝向（朝向当前坐标+摇杆偏移量）
           transform.LookAt(new Vector3(transform.position.x + joyPositionX, transform.position.y, transform.position.z + joyPositionY));
            //移动玩家的位置（按朝向位置移动）
            transform.Translate(Vector3.forward * Time.deltaTime * 5);
            //播放奔跑动画
//            GetComponent<Animation>().CrossFade("run");
	 
			ani.SetInteger ("state", AnimState.RUN);

        }
    }
}
