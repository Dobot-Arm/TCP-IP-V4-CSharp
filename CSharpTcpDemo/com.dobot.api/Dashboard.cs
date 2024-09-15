using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using CSharthiscpDemo.com.dobot.api;
using System.Text.RegularExpressions;

namespace CSharpTcpDemo.com.dobot.api
{
    class Dashboard : DobotClient
    {
        protected override void OnConnected(Socket sock)
        {
            sock.SendTimeout = 5000;
            sock.ReceiveTimeout = 5000;
        }

        protected override void OnDisconnected()
        {
        }

        /// <summary>
        /// 复位，用于清除错误
        /// </summary>
        /// <returns>返回执行结果的描述信息</returns>
        public string ClearError()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "ClearError()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }

        /// <summary>
        /// 机器人上电
        /// </summary>
        /// <returns>返回执行结果的描述信息</returns>
        public string PowerOn()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "PowerOn()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(15000);
        }

        /// <summary>
        /// 急停
        /// </summary>
        /// <returns>返回执行结果的描述信息</returns>
        public string PowerOff()
        {
            return EmergencyStop();
        }

        /// <summary>
        /// 急停
        /// </summary>
        /// <returns>返回执行结果的描述信息</returns>
        public string EmergencyStop()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "EmergencyStop()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(15000);
        }

        /// <summary>
        /// 使能机器人
        /// </summary>
        /// <returns>返回执行结果的描述信息</returns>
        public string EnableRobot()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "EnableRobot()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(20000);
        }

        /// <summary>
        /// 下使能机器人
        /// </summary>
        /// <returns>返回执行结果的描述信息</returns>
        public string DisableRobot()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "DisableRobot()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(20000);
        }

        /// <summary>
        /// 机器人停止
        /// </summary>
        /// <returns>返回执行结果的描述信息</returns>
        public string Stop()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "Stop()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(20000);
        }

        /// <summary>
        /// 设置全局速度比例。
        /// </summary>
        /// <param name="ratio">运动速度比例，取值范围：1~100</param>
        /// <returns>返回执行结果的描述信息</returns>
        public string SpeedFactor(int ratio)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("SpeedFactor({0})", ratio);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 选择已标定的用户坐标系
        public string User(int index)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("User({0})", index);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 选择已标定的工具坐标系
        public string Tool(int index)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("Tool({0})", index);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 机器人状态
        public string RobotMode()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "RobotMode()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 设置当前的负载
        public string SetPayload(double load)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("SetPayload({0})", load);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 设置控制柜模拟输出端口的模拟值
        public string AO(int index, double value)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("AO({0},{1})", index, value);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// <summary>
        /// 设置控制柜模拟输出端口的模拟值
        /// </summary>
        public string AOInstant(int index, double value)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("AOInstant({0},{1})", index, value);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 设置关节加速度比例。
        public string AccJ(int R)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("AccJ({0})", R);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 设置笛卡尔加速度比例。
        public string AccL(int R)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("AccL({0})", R);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 设置关节速度比例。
        public string VelJ(int R)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("VelJ({0})", R);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 设置笛卡尔速度比例。
        public string VelL(int R)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("VelL({0})", R);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 设置CP比例。CP即平滑过渡，机械臂从起始点经过中间点到达终点时，经过中间点是以直角方式过渡还是以曲线方式过渡。
        public string CP(int R)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("CP({0})", R);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 运行脚本。
        public string RunScript(string projectName)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("RunScript({0})", projectName);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 暂停运动(或脚本)。
        public string Pause()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "Pause()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 暂停运动(或脚本)。
        public string Continue()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "Continue()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 设置碰撞等级。
        public string SetCollisionLevel(int level)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("CP({0})", level);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 获取关节坐标系下机械臂的实时位姿
        public string GetAngle()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "GetAngle()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 获取笛卡尔坐标系下机械臂的实时位姿
        public string GetPose()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "GetPose()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 获取数字量输入端口状态
        public string DI(int index)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("DI({0})", index);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 获取末端数字量输入端口状态
        public string ToolDI(int index)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("ToolDI({0})", index);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 获取模拟量输入端口模拟值（立即指令）
        public string AI(int index)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("AI({0})", index);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 获取末端模拟量输入端口模拟值
        public string ToolAI(int index)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("ToolAI({0})", index);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 开关抱闸
        public string BrakeControl(int axisID, int value)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("BrakeControl({0},{1})", axisID, value);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 进入拖拽(在报错状态下，不可进入拖拽)
        public string StartDrag()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "StartDrag()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 退出拖拽（在报错状态下，可以退出拖拽）
        public string StopDrag()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "StopDrag()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 返回当前算法运行队列ID
        public string GetCurrentCommandID()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "GetCurrentCommandID()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 设置拖拽灵敏度
        /// index 轴号0代表1到6轴均设置为此灵敏度 0-6
        /// value 轴拖拽灵敏度 1-90
        public string DragSensivity(int index, int value)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("DragSensivity({0},{1})", index, value);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 获取数字输出端口状态
        public string GetDO(int index)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("GetDO({0})", index);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 获取模拟量输出端口状态
        public string GetAO(int index)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("GetAO({0})", index);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 获取末端数字输出端口状态
        public string GetToolDO(int index)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("GetToolDO({0})", index);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 设置末端工具供电状态
        /// 0:关闭，1：开启
        public string SetToolPower(int status)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("SetToolPower({0})", status);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 设置用户坐标系
        /// index  坐标系索引 0-50
        /// value 要设置的坐标值{x,y,z,rx,ry,rz}
        public string SetUser(int index, string value)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("SetUser({0},{1})", index, value);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 设置工具坐标系
        /// index  坐标系索引 0-50
        /// value 要设置的坐标值{x,y,z,rx,ry,rz}
        public string SetTool(int index, string value)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("SetTool({0},{1})", index, value);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }

        /// <summary>
        /// 设置数字输出端口状态（队列指令）
        /// </summary>
        /// <param name="index">数字输出索引，取值范围：1~16或100~1000</param>
        /// <param name="status">数字输出端口状态，true：高电平；false：低电平</param>
        /// <returns>返回执行结果的描述信息</returns>
        public string DigitalOutputs(int index, bool status)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("DOInstant({0},{1})", index, status ? 1 : 0);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }

        /// <summary>
        /// 设置末端数字输出端口状态（队列指令）
        /// </summary>
        /// <param name="index">数字输出索引</param>
        /// <param name="status">数字输出端口状态，true：高电平；false：低电平</param>
        /// <returns>返回执行结果的描述信息</returns>
        public string ToolDO(int index, bool status)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = String.Format("ToolDOInstant({0},{1})", index, status ? 1 : 0);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }

        public string GetErrorID()
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str = "GetErrorID()";
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        public string MoveJog(string s)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            string str;
            if (string.IsNullOrEmpty(s))
            {
                str = "MoveJog()";
            }
            else
            {
                string strPattern = "^(J[1-6][+-])|([XYZ][+-])|(R[xyz][+-])$";
                if (Regex.IsMatch(s, strPattern))
                {
                    if( s == "J1+" || s == "J1-" || s == "J2+" || s == "J2-" || s == "J3+" || s == "J3-" || s == "J4+" || s == "J4-" || s == "J5+" || s == "J5-" || s == "J6+" || s == "J6-" )
                    {
                        str = "MoveJog(" + s + ")";
                    }
                    else
                    {
                        str = "MoveJog(" + s + ",coordtype=1,user=0,tool=0)";
                    }
                       
                }
                else
                {
                    return "send error:invalid parameter!!!";
                }
            }
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// <summary>
        /// 停止关节点动运动
        /// </summary>
        /// <returns>返回执行结果的描述信息</returns>
        public string StopMoveJog()
        {
            return MoveJog(null);
        }

        /// <summary>
        /// 点到点运动，目标点位为笛卡尔点位
        /// </summary>
        /// <param name="pt">笛卡尔点位</param>
        /// <returns>返回执行结果的描述信息</returns>
        public string MovJ(DescartesPoint pt)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            if (null == pt)
            {
                return "send error:invalid parameter!!!";
            }
            string str = String.Format("MovJ(pose= {{{0},{1},{2},{3},{4},{5}}})", pt.x, pt.y, pt.z, pt.rx, pt.ry, pt.rz);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }

        /// <summary>
        /// 直线运动，目标点位为笛卡尔点位
        /// </summary>
        /// <param name="pt">笛卡尔点位</param>
        /// <returns>返回执行结果的描述信息</returns>
        public string MovL(DescartesPoint pt)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }
            if (null == pt)
            {
                return "send error:invalid parameter!!!";
            }
            string str = String.Format("MovL(pose = {{{0},{1},{2},{3},{4},{5}}})", pt.x, pt.y, pt.z, pt.rx, pt.ry, pt.rz);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }

        /// <summary>
        /// 点到点运动，目标点位为关节点位
        /// </summary>
        /// <param name="pt">笛卡尔点位</param>
        /// <returns>返回执行结果的描述信息</returns>
        public string MovJ_J(JointPoint pt)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            if (null == pt)
            {
                return "send error:invalid parameter!!!";
            }
            string str = String.Format("MovJ(joint= {{{0},{1},{2},{3},{4},{5}}})", pt.j1, pt.j2, pt.j3, pt.j4, pt.j5, pt.j6);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// <summary>
        /// 直线运动，目标点位为关节点位
        /// </summary>
        /// <param name="pt">笛卡尔点位</param>
        /// <returns>返回执行结果的描述信息</returns>
        public string MovL_J(JointPoint pt)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }
            if (null == pt)
            {
                return "send error:invalid parameter!!!";
            }
            string str = String.Format("MovL(joint = {{{0},{1},{2},{3},{4},{5}}})", pt.j1, pt.j2, pt.j3, pt.j4, pt.j5, pt.j6);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
        /// 基于关节空间的动态跟随命令
        public string ServoP(DescartesPoint pt,float t)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            if (null == pt)
            {
                return "send error:invalid parameter!!!";
            }
            string str = String.Format("ServoP({0},{1},{2},{3},{4},{5},t={6})", pt.x, pt.y, pt.z, pt.rx, pt.ry, pt.rz,t);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }

        /// 基于笛卡尔空间的动态跟随命令
        public string ServoJ(JointPoint pt,float t)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }
            if (null == pt)
            {
                return "send error:invalid parameter!!!";
            }
            string str = String.Format("ServoJ({0},{1},{2},{3},{4},{5},t={6})", pt.j1, pt.j2, pt.j3, pt.j4, pt.j5, pt.j6,t);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }





        /// <summary>
        /// 点到点运动，目标点位为关节点位。
        /// </summary>
        /// <param name="pt"></param>
        /// <returns>返回执行结果的描述信息</returns>
        public string JointMovJ(JointPoint pt)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }
            if (null == pt)
            {
                return "send error:invalid parameter!!!";
            }
            string str = String.Format("MovJ(joint = {{{0},{1},{2},{3},{4},{5}}})", pt.j1, pt.j2, pt.j3, pt.j4, pt.j5, pt.j6);
            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(5000);
        }
    }
}
