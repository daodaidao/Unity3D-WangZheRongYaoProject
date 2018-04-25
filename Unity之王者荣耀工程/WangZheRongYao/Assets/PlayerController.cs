using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //角色
    public GameObject player;
    //摇杆
    public Joystick joystick;

    void Update()
    {
        //摇杆没有被触发
        if (joystick.tapCount <= 0)
        {
            return;
        }
        //获取摇杆偏移
        var joyPositionX = joystick.position.x;
        var joyPositionY = joystick.position.y;

        if (joyPositionY != 0 || joyPositionX != 0)
        {
            //设置角色的朝向（朝向当前坐标+摇杆偏移量）
            player.transform.LookAt(new Vector3(player.transform.position.x + joyPositionX, player.transform.position.y, player.transform.position.z + joyPositionY));
            //移动玩家的位置（按朝向位置移动）
            player.transform.Translate(Vector3.forward * Time.deltaTime * 5);
            //播放奔跑动画
            player.GetComponent<Animation>().Play("run");
        }
        else
        {
            //播放待机动画
            player.GetComponent<Animation>().Play("idle");
        }
    }
}
