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
