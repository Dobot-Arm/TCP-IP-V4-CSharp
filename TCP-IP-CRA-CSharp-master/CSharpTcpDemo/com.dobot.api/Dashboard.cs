using System;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace CSharpTcpDemo.com.dobot.api
{
    public class Dashboard : DobotClient
    {
        protected override void OnConnected(Socket sock)
        {
            sock.SendTimeout = 5000;
            sock.ReceiveTimeout = 5000;
        }

        protected override void OnDisconnected()
        {
        }

        private string SendRecvMsg(string str, int timeout = 5000)
        {
            if (!IsConnected())
            {
                return "device does not connected!!!";
            }

            if (!SendData(str))
            {
                return str + ":send error";
            }

            return WaitReply(timeout);
        }

        /// <summary>
        /// Enable the robot
        /// </summary>
        public string EnableRobot(double load = 0.0, double centerX = 0.0, double centerY = 0.0, double centerZ = 0.0, int isCheck = -1)
        {
            string str = "EnableRobot(";
            if (load != 0)
            {
                str += string.Format("{0:F6}", load);
                if (centerX != 0 || centerY != 0 || centerZ != 0)
                {
                    str += string.Format(",{0:F6},{1:F6},{2:F6}", centerX, centerY, centerZ);
                    if (isCheck != -1)
                    {
                        str += "," + isCheck;
                    }
                }
            }
            str += ")";
            return SendRecvMsg(str, 20000);
        }

        /// <summary>
        /// Disable the robot
        /// </summary>
        public string DisableRobot()
        {
            return SendRecvMsg("DisableRobot()", 20000);
        }

        /// <summary>
        /// Clear controller alarm information
        /// </summary>
        public string ClearError()
        {
            return SendRecvMsg("ClearError()");
        }

        /// <summary>
        /// Powering on the robot
        /// </summary>
        public string PowerOn()
        {
            return SendRecvMsg("PowerOn()", 15000);
        }

        /// <summary>
        /// Powering off the robot (Emergency Stop)
        /// </summary>
        public string PowerOff()
        {
            return EmergencyStop();
        }

        /// <summary>
        /// Run the script file
        /// </summary>
        public string RunScript(string projectName)
        {
            return SendRecvMsg(string.Format("RunScript({0})", projectName));
        }

        /// <summary>
        /// Stop the delivered motion command queue or the RunScript command
        /// </summary>
        public string Stop()
        {
            return SendRecvMsg("Stop()", 20000);
        }

        /// <summary>
        /// Pause the delivered motion command queue or the RunScript command
        /// </summary>
        public string Pause()
        {
            return SendRecvMsg("Pause()");
        }

        /// <summary>
        /// Continue the paused motion command queue or the RunScript command
        /// </summary>
        public string Continue()
        {
            return SendRecvMsg("Continue()");
        }

        /// <summary>
        /// Emergency Stop
        /// </summary>
        public string EmergencyStop(int mode = 1)
        {
            return SendRecvMsg(string.Format("EmergencyStop({0})", mode), 15000);
        }

        /// <summary>
        /// Control the brake of specified joint
        /// </summary>
        public string BrakeControl(int axisID, int value)
        {
            return SendRecvMsg(string.Format("BrakeControl({0},{1})", axisID, value));
        }

        /// <summary>
        /// Set the global speed ratio
        /// </summary>
        public string SpeedFactor(int ratio)
        {
            return SendRecvMsg(string.Format("SpeedFactor({0})", ratio));
        }

        /// <summary>
        /// Set the global user coordinate system
        /// </summary>
        public string User(int index)
        {
            return SendRecvMsg(string.Format("User({0})", index));
        }

        /// <summary>
        /// Modify the specified user coordinate system
        /// </summary>
        public string SetUser(int index, string table)
        {
            return SendRecvMsg(string.Format("SetUser({0},{1})", index, table));
        }

        /// <summary>
        /// Calculate the user coordinate system
        /// </summary>
        public string CalcUser(int index, int matrixDirection, string table)
        {
            return SendRecvMsg(string.Format("CalcUser({0},{1},{2})", index, matrixDirection, table));
        }

        /// <summary>
        /// Set the global tool coordinate system
        /// </summary>
        public string Tool(int index)
        {
            return SendRecvMsg(string.Format("Tool({0})", index));
        }

        /// <summary>
        /// Modify the specified tool coordinate system
        /// </summary>
        public string SetTool(int index, string table)
        {
            return SendRecvMsg(string.Format("SetTool({0},{1})", index, table));
        }

        /// <summary>
        /// Calculate the tool coordinate system
        /// </summary>
        public string CalcTool(int index, int matrixDirection, string table)
        {
            return SendRecvMsg(string.Format("CalcTool({0},{1},{2})", index, matrixDirection, table));
        }

        /// <summary>
        /// Set the load of the robot arm
        /// </summary>
        public string SetPayload(double load = 0.0, double x = 0.0, double y = 0.0, double z = 0.0, string name = "F")
        {
            string str = "SetPayload(";
            if (name != "F")
            {
                str += name;
            }
            else
            {
                if (load != 0)
                {
                    str += string.Format("{0:F6}", load);
                    if (x != 0 || y != 0 || z != 0)
                    {
                        str += string.Format(",{0:F6},{1:F6},{2:F6}", x, y, z);
                    }
                }
            }
            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// Set acceleration ratio of joint motion
        /// </summary>
        public string AccJ(int speed)
        {
            return SendRecvMsg(string.Format("AccJ({0})", speed));
        }

        /// <summary>
        /// Set acceleration ratio of linear and arc motion
        /// </summary>
        public string AccL(int speed)
        {
            return SendRecvMsg(string.Format("AccL({0})", speed));
        }

        /// <summary>
        /// Set the speed ratio of joint motion
        /// </summary>
        public string VelJ(int speed)
        {
            return SendRecvMsg(string.Format("VelJ({0})", speed));
        }

        /// <summary>
        /// Set the speed ratio of linear and arc motion
        /// </summary>
        public string VelL(int speed)
        {
            return SendRecvMsg(string.Format("VelL({0})", speed));
        }

        /// <summary>
        /// Set the continuous path (CP) ratio
        /// </summary>
        public string CP(int ratio)
        {
            return SendRecvMsg(string.Format("CP({0})", ratio));
        }

        /// <summary>
        /// Set the collision detection level
        /// </summary>
        public string SetCollisionLevel(int level)
        {
            return SendRecvMsg(string.Format("SetCollisionLevel({0})", level));
        }

        /// <summary>
        /// Set the backoff distance after the robot detects collision
        /// </summary>
        public string SetBackDistance(double distance)
        {
            return SendRecvMsg(string.Format("SetBackDistance({0:F6})", distance));
        }

        /// <summary>
        /// Set the post-collision processing mode
        /// </summary>
        public string SetPostCollisionMode(int mode)
        {
            return SendRecvMsg(string.Format("SetPostCollisionMode({0})", mode));
        }

        /// <summary>
        /// Enter drag mode
        /// </summary>
        public string StartDrag()
        {
            return SendRecvMsg("StartDrag()");
        }

        /// <summary>
        /// Exit drag mode
        /// </summary>
        public string StopDrag()
        {
            return SendRecvMsg("StopDrag()");
        }

        /// <summary>
        /// Set drag sensitivity
        /// </summary>
        public string DragSensivity(int index, int value)
        {
            return SendRecvMsg(string.Format("DragSensivity({0},{1})", index, value));
        }

        /// <summary>
        /// Switch on or off the SafeSkin
        /// </summary>
        public string EnableSafeSkin(int status)
        {
            return SendRecvMsg(string.Format("EnableSafeSkin({0})", status));
        }

        /// <summary>
        /// Set the sensitivity for each part of the SafeSkin
        /// </summary>
        public string SetSafeSkin(int part, int status)
        {
            return SendRecvMsg(string.Format("SetSafeSkin({0},{1})", part, status));
        }

        /// <summary>
        /// Switch on/off the specified safety wall
        /// </summary>
        public string SetSafeWallEnable(int index, int value)
        {
            return SendRecvMsg(string.Format("SetSafeWallEnable({0},{1})", index, value));
        }

        /// <summary>
        /// Switch on/off the specified interference area
        /// </summary>
        public string SetWorkZoneEnable(int index, int value)
        {
            return SendRecvMsg(string.Format("SetWorkZoneEnable({0},{1})", index, value));
        }

        /// <summary>
        /// Get the current status of the robot
        /// </summary>
        public string RobotMode()
        {
            return SendRecvMsg("RobotMode()");
        }

        /// <summary>
        /// Positive solution
        /// </summary>
        public string PositiveKin(double J1, double J2, double J3, double J4, double J5, double J6, int user = -1, int tool = -1)
        {
            string str = string.Format("PositiveKin({0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}", J1, J2, J3, J4, J5, J6);
            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// Inverse solution
        /// </summary>
        public string InverseKin(double X, double Y, double Z, double Rx, double Ry, double Rz, int user = -1, int tool = -1, int useJointNear = -1, string jointNear = "")
        {
            string str = string.Format("InverseKin({0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}", X, Y, Z, Rx, Ry, Rz);
            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (useJointNear != -1) str += string.Format(",useJointNear={0}", useJointNear);
            if (!string.IsNullOrEmpty(jointNear)) str += string.Format(",JointNear={0}", jointNear);
            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// Get the joint coordinates of current posture
        /// </summary>
        public string GetAngle()
        {
            return SendRecvMsg("GetAngle()");
        }

        /// <summary>
        /// Get the Cartesian coordinates of the current posture
        /// </summary>
        public string GetPose(int user = -1, int tool = -1)
        {
            string str = "GetPose(";
            if (user != -1) str += string.Format("user={0}", user);
            if (tool != -1) str += string.Format("{0}tool={1}", (user != -1 ? "," : ""), tool);
            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// Get error ID
        /// </summary>
        public string GetErrorID()
        {
            return SendRecvMsg("GetErrorID()");
        }

        /// <summary>
        /// Set the status of digital output port (queue command)
        /// </summary>
        public string DO(int index, int status, int time = -1)
        {
            string str = string.Format("DO({0},{1}", index, status);
            if (time != -1) str += string.Format(",{0}", time);
            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// Set the status of digital output port (immediate command)
        /// </summary>
        public string DOInstant(int index, int status)
        {
            return SendRecvMsg(string.Format("DOInstant({0},{1})", index, status));
        }

        public string DigitalOutputs(int index, bool status)
        {
             return DOInstant(index, status ? 1 : 0);
        }

        /// <summary>
        /// Get the status of digital output port
        /// </summary>
        public string GetDO(int index)
        {
            return SendRecvMsg(string.Format("GetDO({0})", index));
        }

        /// <summary>
        /// Set the status of multiple digital output ports (queue command)
        /// </summary>
        public string DOGroup(params int[] args)
        {
            string str = "DOGroup(";
            str += string.Join(",", args);
            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// Get the status of multiple digital output ports
        /// </summary>
        public string GetDOGroup(params int[] args)
        {
             string str = "GetDOGroup(";
             str += string.Join(",", args);
             str += ")";
             return SendRecvMsg(str);
        }

        /// <summary>
        /// Set the status of tool digital output port (queue command)
        /// </summary>
        public string ToolDO(int index, int status)
        {
            return SendRecvMsg(string.Format("ToolDO({0},{1})", index, status));
        }

        public string ToolDO(int index, bool status)
        {
             return ToolDOInstant(index, status ? 1 : 0);
        }

        /// <summary>
        /// Set the status of tool digital output port (immediate command)
        /// </summary>
        public string ToolDOInstant(int index, int status)
        {
            return SendRecvMsg(string.Format("ToolDOInstant({0},{1})", index, status));
        }

        /// <summary>
        /// Get the status of tool digital output port
        /// </summary>
        public string GetToolDO(int index)
        {
            return SendRecvMsg(string.Format("GetToolDO({0})", index));
        }

        /// <summary>
        /// Set the value of analog output port (queue command)
        /// </summary>
        public string AO(int index, double value)
        {
            return SendRecvMsg(string.Format("AO({0},{1:F6})", index, value));
        }

        /// <summary>
        /// Set the value of analog output port (immediate command)
        /// </summary>
        public string AOInstant(int index, double value)
        {
             return SendRecvMsg(string.Format("AOInstant({0},{1:F6})", index, value));
        }

        /// <summary>
        /// Get the value of analog output port
        /// </summary>
        public string GetAO(int index)
        {
            return SendRecvMsg(string.Format("GetAO({0})", index));
        }

        /// <summary>
        /// Get status of DI port
        /// </summary>
        public string DI(int index)
        {
            return SendRecvMsg(string.Format("DI({0})", index));
        }

        /// <summary>
        /// Get status of multiple DI ports
        /// </summary>
        public string DIGroup(params int[] args)
        {
             string str = "DIGroup(";
             str += string.Join(",", args);
             str += ")";
             return SendRecvMsg(str);
        }

        /// <summary>
        /// Get the status of tool digital input port
        /// </summary>
        public string ToolDI(int index)
        {
            return SendRecvMsg(string.Format("ToolDI({0})", index));
        }

        /// <summary>
        /// Get the value of AI port
        /// </summary>
        public string AI(int index)
        {
            return SendRecvMsg(string.Format("AI({0})", index));
        }

        /// <summary>
        /// Get the value of tool AI port
        /// </summary>
        public string ToolAI(int index)
        {
            return SendRecvMsg(string.Format("ToolAI({0})", index));
        }

        /// <summary>
        /// Set tool 485 params
        /// </summary>
        public string SetTool485(int baud, string parity = "N", int stopbit = 1, int identify = -1)
        {
            string str = string.Format("SetTool485({0},{1},{2}", baud, parity, stopbit);
            if (identify != -1) str += "," + identify;
            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// Set tool power
        /// </summary>
        public string SetToolPower(int status, int identify = -1)
        {
            string str = string.Format("SetToolPower({0}", status);
             if (identify != -1) str += "," + identify;
            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// Set tool mode
        /// </summary>
        public string SetToolMode(int mode, int type, int identify = -1)
        {
            string str = string.Format("SetToolMode({0},{1}", mode, type);
             if (identify != -1) str += "," + identify;
            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// Create Modbus master
        /// </summary>
        public string ModbusCreate(string ip, int port, int slave_id, int isRTU = -1)
        {
             string str = string.Format("ModbusCreate({0},{1},{2}", ip, port, slave_id);
             if (isRTU != -1) str += "," + isRTU;
             str += ")";
             return SendRecvMsg(str);
        }

        /// <summary>
        /// Create Modbus RTU master
        /// </summary>
        public string ModbusRTUCreate(int slave_id, int baud, string parity = "E", int data_bit = 8, int stop_bit = 1)
        {
            return SendRecvMsg(string.Format("ModbusRTUCreate({0},{1},{2},{3},{4})", slave_id, baud, parity, data_bit, stop_bit));
        }

        /// <summary>
        /// Close Modbus master
        /// </summary>
        public string ModbusClose(int index)
        {
            return SendRecvMsg(string.Format("ModbusClose({0})", index));
        }

        /// <summary>
        /// Get Modbus input bits
        /// </summary>
        public string GetInBits(int index, int addr, int count)
        {
            return SendRecvMsg(string.Format("GetInBits({0},{1},{2})", index, addr, count));
        }

        /// <summary>
        /// Get Modbus input registers
        /// </summary>
        public string GetInRegs(int index, int addr, int count, string valType = "U16")
        {
            return SendRecvMsg(string.Format("GetInRegs({0},{1},{2},{3})", index, addr, count, valType));
        }

        /// <summary>
        /// Get Modbus coils
        /// </summary>
        public string GetCoils(int index, int addr, int count)
        {
             return SendRecvMsg(string.Format("GetCoils({0},{1},{2})", index, addr, count));
        }

        /// <summary>
        /// Set Modbus coils
        /// </summary>
        public string SetCoils(int index, int addr, int count, string valTab)
        {
             return SendRecvMsg(string.Format("SetCoils({0},{1},{2},{3})", index, addr, count, valTab));
        }

        /// <summary>
        /// Get Modbus holding registers
        /// </summary>
        public string GetHoldRegs(int index, int addr, int count, string valType = "U16")
        {
             return SendRecvMsg(string.Format("GetHoldRegs({0},{1},{2},{3})", index, addr, count, valType));
        }

        /// <summary>
        /// Set Modbus holding registers
        /// </summary>
        public string SetHoldRegs(int index, int addr, int count, string valTab, string valType = "U16")
        {
             return SendRecvMsg(string.Format("SetHoldRegs({0},{1},{2},{3},{4})", index, addr, count, valTab, valType));
        }

        public string GetInputBool(int address) { return SendRecvMsg(string.Format("GetInputBool({0})", address)); }
        public string GetInputInt(int address) { return SendRecvMsg(string.Format("GetInputInt({0})", address)); }
        public string GetInputFloat(int address) { return SendRecvMsg(string.Format("GetInputFloat({0})", address)); }
        public string GetOutputBool(int address) { return SendRecvMsg(string.Format("GetOutputBool({0})", address)); }
        public string GetOutputInt(int address) { return SendRecvMsg(string.Format("GetOutputInt({0})", address)); }
        public string GetOutputFloat(int address) { return SendRecvMsg(string.Format("GetOutputFloat({0})", address)); }
        public string SetOutputBool(int address, int value) { return SendRecvMsg(string.Format("SetOutputBool({0},{1})", address, value)); }
        public string SetOutputInt(int address, int value) { return SendRecvMsg(string.Format("SetOutputInt({0},{1})", address, value)); }
        public string SetOutputFloat(int address, int value) { return SendRecvMsg(string.Format("SetOutputFloat({0},{1})", address, value)); }

        /// <summary>
        /// MovJ
        /// </summary>
        public string MovJ(double a1, double b1, double c1, double d1, double e1, double f1, int coordinateMode = 0, int user = -1, int tool = -1, int a = -1, int v = -1, int cp = -1)
        {
            string str = coordinateMode == 0 ? 
                string.Format("MovJ(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}}", a1, b1, c1, d1, e1, f1) : 
                string.Format("MovJ(joint={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}}", a1, b1, c1, d1, e1, f1);

            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1) str += string.Format(",v={0}", v);
            if (cp != -1) str += string.Format(",cp={0}", cp);
            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// Compatibility overload for MovJ with DescartesPoint
        /// </summary>
        public string MovJ(DescartesPoint pt)
        {
            if (null == pt) return "send error:invalid parameter!!!";
            string str = String.Format("MovJ(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}})", pt.x, pt.y, pt.z, pt.rx, pt.ry, pt.rz);
            return SendRecvMsg(str);
        }

        public string JointMovJ(JointPoint pt)
        {
            if (null == pt) return "send error:invalid parameter!!!";
            string str = String.Format("MovJ(joint={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}})", pt.j1, pt.j2, pt.j3, pt.j4, pt.j5, pt.j6);
            return SendRecvMsg(str);
        }

        /// <summary>
        /// MovJ with JointPoint (Alias for compatibility)
        /// </summary>
        public string MovJ_J(JointPoint pt)
        {
             return JointMovJ(pt);
        }

        /// <summary>
        /// MovL
        /// </summary>
        public string MovL(double a1, double b1, double c1, double d1, double e1, double f1, int coordinateMode = 0, int user = -1, int tool = -1, int a = -1, int v = -1, int speed = -1, int cp = -1, int r = -1)
        {
            string str = coordinateMode == 0 ?
                string.Format("MovL(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}}", a1, b1, c1, d1, e1, f1) :
                string.Format("MovL(joint={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}}", a1, b1, c1, d1, e1, f1);

            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1 && speed != -1) str += string.Format(",speed={0}", speed);
            else if (speed != -1) str += string.Format(",speed={0}", speed);
            else if (v != -1) str += string.Format(",v={0}", v);
            
            if (cp != -1 && r != -1) str += string.Format(",r={0}", r);
            else if (r != -1) str += string.Format(",r={0}", r);
            else if (cp != -1) str += string.Format(",cp={0}", cp);

            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// Compatibility overload for MovL with DescartesPoint
        /// </summary>
        public string MovL(DescartesPoint pt)
        {
            if (null == pt) return "send error:invalid parameter!!!";
            string str = String.Format("MovL(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}})", pt.x, pt.y, pt.z, pt.rx, pt.ry, pt.rz);
            return SendRecvMsg(str);
        }

        /// <summary>
        /// MovL with JointPoint (for compatibility)
        /// </summary>
        public string MovL_J(JointPoint pt)
        {
            if (null == pt) return "send error:invalid parameter!!!";
            string str = String.Format("MovL(joint={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}})", pt.j1, pt.j2, pt.j3, pt.j4, pt.j5, pt.j6);
            return SendRecvMsg(str);
        }

        /// <summary>
        /// ServoJ
        /// </summary>
        public string ServoJ(double j1, double j2, double j3, double j4, double j5, double j6, double t = -1, double aheadtime = -1, double gain = -1)
        {
            string str = string.Format("ServoJ({0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}", j1, j2, j3, j4, j5, j6);
            if (t != -1) str += string.Format(",t={0:F6}", t);
            if (aheadtime != -1) str += string.Format(",aheadtime={0:F6}", aheadtime);
            if (gain != -1) str += string.Format(",gain={0:F6}", gain);
            str += ")";
            return SendRecvMsg(str);
        }

        public string ServoJ(JointPoint pt, float t)
        {
            if (null == pt) return "send error:invalid parameter!!!";
            return ServoJ(pt.j1, pt.j2, pt.j3, pt.j4, pt.j5, pt.j6, t);
        }

        /// <summary>
        /// ServoP
        /// </summary>
        public string ServoP(double x, double y, double z, double rx, double ry, double rz, double t = -1, double aheadtime = -1, double gain = -1)
        {
            string str = string.Format("ServoP({0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}", x, y, z, rx, ry, rz);
            if (t != -1) str += string.Format(",t={0:F6}", t);
            if (aheadtime != -1) str += string.Format(",aheadtime={0:F6}", aheadtime);
            if (gain != -1) str += string.Format(",gain={0:F6}", gain);
            str += ")";
            return SendRecvMsg(str);
        }

        public string ServoP(DescartesPoint pt, float t)
        {
            if (null == pt) return "send error:invalid parameter!!!";
            return ServoP(pt.x, pt.y, pt.z, pt.rx, pt.ry, pt.rz, t);
        }

        /// <summary>
        /// MovLIO
        /// </summary>
        public string MovLIO(double a1, double b1, double c1, double d1, double e1, double f1, int coordinateMode, int Mode, int Distance, int Index, int Status, int user = -1, int tool = -1, int a = -1, int v = -1, int speed = -1, int cp = -1, int r = -1)
        {
            string str = coordinateMode == 0 ?
                string.Format("MovLIO(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},{{{{0},{1},{2},{3}}}}", a1, b1, c1, d1, e1, f1, Mode, Distance, Index, Status) :
                string.Format("MovLIO(joint={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},{{{{0},{1},{2},{3}}}}", a1, b1, c1, d1, e1, f1, Mode, Distance, Index, Status);

            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1 && speed != -1) str += string.Format(",speed={0}", speed);
            else if (speed != -1) str += string.Format(",speed={0}", speed);
            else if (v != -1) str += string.Format(",v={0}", v);

            if (cp != -1 && r != -1) str += string.Format(",r={0}", r);
            else if (r != -1) str += string.Format(",r={0}", r);
            else if (cp != -1) str += string.Format(",cp={0}", cp);

            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// MovJIO
        /// </summary>
        public string MovJIO(double a1, double b1, double c1, double d1, double e1, double f1, int coordinateMode, int Mode, int Distance, int Index, int Status, int user = -1, int tool = -1, int a = -1, int v = -1, int cp = -1)
        {
            string str = coordinateMode == 0 ?
                string.Format("MovJIO(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},{{{{0},{1},{2},{3}}}}", a1, b1, c1, d1, e1, f1, Mode, Distance, Index, Status) :
                string.Format("MovJIO(joint={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},{{{{0},{1},{2},{3}}}}", a1, b1, c1, d1, e1, f1, Mode, Distance, Index, Status);

            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1) str += string.Format(",v={0}", v);
            if (cp != -1) str += string.Format(",cp={0}", cp);

            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// Arc
        /// </summary>
        public string Arc(double a1, double b1, double c1, double d1, double e1, double f1, double a2, double b2, double c2, double d2, double e2, double f2, int coordinateMode, int user = -1, int tool = -1, int a = -1, int v = -1, int speed = -1, int cp = -1, int r = -1)
        {
            string str = coordinateMode == 0 ?
                string.Format("Arc(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},pose={{{6:F6},{7:F6},{8:F6},{9:F6},{10:F6},{11:F6}}}", a1, b1, c1, d1, e1, f1, a2, b2, c2, d2, e2, f2) :
                string.Format("Arc(joint={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},joint={{{6:F6},{7:F6},{8:F6},{9:F6},{10:F6},{11:F6}}}", a1, b1, c1, d1, e1, f1, a2, b2, c2, d2, e2, f2);

            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1 && speed != -1) str += string.Format(",speed={0}", speed);
            else if (speed != -1) str += string.Format(",speed={0}", speed);
            else if (v != -1) str += string.Format(",v={0}", v);

            if (cp != -1 && r != -1) str += string.Format(",r={0}", r);
            else if (r != -1) str += string.Format(",r={0}", r);
            else if (cp != -1) str += string.Format(",cp={0}", cp);

            str += ")";
            return SendRecvMsg(str);
        }

        /// <summary>
        /// Circle
        /// </summary>
        public string Circle(double a1, double b1, double c1, double d1, double e1, double f1, double a2, double b2, double c2, double d2, double e2, double f2, int coordinateMode, int count, int user = -1, int tool = -1, int a = -1, int v = -1, int speed = -1, int cp = -1, int r = -1)
        {
             string str = coordinateMode == 0 ?
                string.Format("Circle(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},pose={{{6:F6},{7:F6},{8:F6},{9:F6},{10:F6},{11:F6}}},{12}", a1, b1, c1, d1, e1, f1, a2, b2, c2, d2, e2, f2, count) :
                string.Format("Circle(joint={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},joint={{{6:F6},{7:F6},{8:F6},{9:F6},{10:F6},{11:F6}}},{12}", a1, b1, c1, d1, e1, f1, a2, b2, c2, d2, e2, f2, count);

            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1 && speed != -1) str += string.Format(",speed={0}", speed);
            else if (speed != -1) str += string.Format(",speed={0}", speed);
            else if (v != -1) str += string.Format(",v={0}", v);

            if (cp != -1 && r != -1) str += string.Format(",r={0}", r);
            else if (r != -1) str += string.Format(",r={0}", r);
            else if (cp != -1) str += string.Format(",cp={0}", cp);

            str += ")";
            return SendRecvMsg(str);
        }

        public string MoveJog(string s)
        {
            if (string.IsNullOrEmpty(s)) return SendRecvMsg("MoveJog()");
            if (Regex.IsMatch(s, "^(J[1-6][+-])|([XYZ][+-])|(R[xyz][+-])$"))
            {
                 if (Regex.IsMatch(s, "^(J[1-6][+-])$"))
                     return SendRecvMsg("MoveJog(" + s + ")");
                 else
                     return SendRecvMsg("MoveJog(" + s + ",coordtype=1,user=0,tool=0)");
            }
            return "send error:invalid parameter!!!";
        }

        public string MoveJog(string axisId, int coordType, int user, int tool)
        {
             return SendRecvMsg(string.Format("MoveJog({0},coordtype={1},user={2},tool={3})", axisId, coordType, user, tool));
        }

        public string StopMoveJog()
        {
            return MoveJog(null);
        }

        public string GetStartPose(string traceName)
        {
            return SendRecvMsg(string.Format("GetStartPose({0})", traceName));
        }

        public string StartPath(string traceName, int isConst = -1, double multi = -1, int user = -1, int tool = -1)
        {
             string str = string.Format("StartPath({0}", traceName);
             if (isConst != -1) str += string.Format(",isConst={0}", isConst);
             if (multi != -1) str += string.Format(",multi={0:F6}", multi);
             if (user != -1) str += string.Format(",user={0}", user);
             if (tool != -1) str += string.Format(",tool={0}", tool);
             str += ")";
             return SendRecvMsg(str);
        }

        public string RelMovJTool(double offsetX, double offsetY, double offsetZ, double offsetRx, double offsetRy, double offsetRz, int user = -1, int tool = -1, int a = -1, int v = -1, int cp = -1)
        {
            string str = string.Format("RelMovJTool({0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}", offsetX, offsetY, offsetZ, offsetRx, offsetRy, offsetRz);
            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1) str += string.Format(",v={0}", v);
            if (cp != -1) str += string.Format(",cp={0}", cp);
            str += ")";
            return SendRecvMsg(str);
        }

        public string RelMovLTool(double offsetX, double offsetY, double offsetZ, double offsetRx, double offsetRy, double offsetRz, int user = -1, int tool = -1, int a = -1, int v = -1, int speed = -1, int cp = -1, int r = -1)
        {
            string str = string.Format("RelMovLTool({0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}", offsetX, offsetY, offsetZ, offsetRx, offsetRy, offsetRz);
            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1 && speed != -1) str += string.Format(",speed={0}", speed);
            else if (speed != -1) str += string.Format(",speed={0}", speed);
            else if (v != -1) str += string.Format(",v={0}", v);
            
            if (cp != -1 && r != -1) str += string.Format(",r={0}", r);
            else if (r != -1) str += string.Format(",r={0}", r);
            else if (cp != -1) str += string.Format(",cp={0}", cp);
            str += ")";
            return SendRecvMsg(str);
        }

        public string RelMovJUser(double offsetX, double offsetY, double offsetZ, double offsetRx, double offsetRy, double offsetRz, int user = -1, int tool = -1, int a = -1, int v = -1, int cp = -1)
        {
            string str = string.Format("RelMovJUser({0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}", offsetX, offsetY, offsetZ, offsetRx, offsetRy, offsetRz);
            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1) str += string.Format(",v={0}", v);
            if (cp != -1) str += string.Format(",cp={0}", cp);
            str += ")";
            return SendRecvMsg(str);
        }

        public string RelMovLUser(double offsetX, double offsetY, double offsetZ, double offsetRx, double offsetRy, double offsetRz, int user = -1, int tool = -1, int a = -1, int v = -1, int speed = -1, int cp = -1, int r = -1)
        {
            string str = string.Format("RelMovLUser({0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}", offsetX, offsetY, offsetZ, offsetRx, offsetRy, offsetRz);
            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1 && speed != -1) str += string.Format(",speed={0}", speed);
            else if (speed != -1) str += string.Format(",speed={0}", speed);
            else if (v != -1) str += string.Format(",v={0}", v);
            
            if (cp != -1 && r != -1) str += string.Format(",r={0}", r);
            else if (r != -1) str += string.Format(",r={0}", r);
            else if (cp != -1) str += string.Format(",cp={0}", cp);
            str += ")";
            return SendRecvMsg(str);
        }

        public string RelJointMovJ(double offset1, double offset2, double offset3, double offset4, double offset5, double offset6, int a = -1, int v = -1, int cp = -1)
        {
            string str = string.Format("RelJointMovJ({0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}", offset1, offset2, offset3, offset4, offset5, offset6);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1) str += string.Format(",v={0}", v);
            if (cp != -1) str += string.Format(",cp={0}", cp);
            str += ")";
            return SendRecvMsg(str);
        }

        public string GetCurrentCommandID()
        {
            return SendRecvMsg("GetCurrentCommandID()");
        }

        public string SetResumeOffset(double distance)
        {
            return SendRecvMsg(string.Format("SetResumeOffset({0:F6})", distance));
        }

        public string PathRecovery()
        {
            return SendRecvMsg("PathRecovery()");
        }

        public string PathRecoveryStop()
        {
            return SendRecvMsg("PathRecoveryStop()");
        }

        public string PathRecoveryStatus()
        {
            return SendRecvMsg("PathRecoveryStatus()");
        }

        public string LogExportUSB(int range)
        {
            return SendRecvMsg(string.Format("LogExportUSB({0})", range));
        }

        public string GetExportStatus()
        {
            return SendRecvMsg("GetExportStatus()");
        }

        public string EnableFTSensor(int status)
        {
            return SendRecvMsg(string.Format("EnableFTSensor({0})", status));
        }

        public string SixForceHome()
        {
            return SendRecvMsg("SixForceHome()");
        }

        public string GetForce(int tool = -1)
        {
            return tool == -1 ? SendRecvMsg("GetForce()") : SendRecvMsg(string.Format("GetForce({0})", tool));
        }

        public string ForceDriveMode(int x, int y, int z, int rx, int ry, int rz, int user = -1)
        {
            string str = string.Format("ForceDriveMode({{{0},{1},{2},{3},{4},{5}}}", x, y, z, rx, ry, rz);
            if (user != -1) str += string.Format(",{0}", user);
            str += ")";
            return SendRecvMsg(str);
        }

        public string ForceDriveSpeed(int speed)
        {
            return SendRecvMsg(string.Format("ForceDriveSpeed({0})", speed));
        }

        public string FCForceMode(int x, int y, int z, int rx, int ry, int rz, int fx, int fy, int fz, int frx, int fry, int frz, int reference = -1, int user = -1, int tool = -1)
        {
             string str = string.Format("FCForceMode({{{0},{1},{2},{3},{4},{5}}},{{{6},{7},{8},{9},{10},{11}}}", x, y, z, rx, ry, rz, fx, fy, fz, frx, fry, frz);
             if (reference != -1) str += string.Format(",reference={0}", reference);
             if (user != -1) str += string.Format(",user={0}", user);
             if (tool != -1) str += string.Format(",tool={0}", tool);
             str += ")";
             return SendRecvMsg(str);
        }

        public string FCSetDeviation(int x, int y, int z, int rx, int ry, int rz, int controltype = -1)
        {
             string str = string.Format("FCSetDeviation({{{0},{1},{2},{3},{4},{5}}}", x, y, z, rx, ry, rz);
             if (controltype != -1) str += string.Format(",{0}", controltype);
             str += ")";
             return SendRecvMsg(str);
        }

        public string FCSetForceLimit(int x, int y, int z, int rx, int ry, int rz)
        {
             return SendRecvMsg(string.Format("FCSetForceLimit({0},{1},{2},{3},{4},{5})", x, y, z, rx, ry, rz));
        }

        public string FCSetMass(int x, int y, int z, int rx, int ry, int rz)
        {
             return SendRecvMsg(string.Format("FCSetMass({0},{1},{2},{3},{4},{5})", x, y, z, rx, ry, rz));
        }

        public string FCSetStiffness(int x, int y, int z, int rx, int ry, int rz)
        {
             return SendRecvMsg(string.Format("FCSetStiffness({0},{1},{2},{3},{4},{5})", x, y, z, rx, ry, rz));
        }

        public string FCSetDamping(int x, int y, int z, int rx, int ry, int rz)
        {
             return SendRecvMsg(string.Format("FCSetDamping({0},{1},{2},{3},{4},{5})", x, y, z, rx, ry, rz));
        }

        public string FCOff()
        {
            return SendRecvMsg("FCOff()");
        }

        public string FCSetForceSpeedLimit(int x, int y, int z, int rx, int ry, int rz)
        {
             return SendRecvMsg(string.Format("FCSetForceSpeedLimit({0},{1},{2},{3},{4},{5})", x, y, z, rx, ry, rz));
        }

        public string FCSetForce(int x, int y, int z, int rx, int ry, int rz)
        {
             return SendRecvMsg(string.Format("FCSetForce({0},{1},{2},{3},{4},{5})", x, y, z, rx, ry, rz));
        }

        public string RequestControl()
        {
            return SendRecvMsg("RequestControl()");
        }

        public string RelPointTool(int coordinateMode, double a1, double b1, double c1, double d1, double e1, double f1, double x, double y, double z, double rx, double ry, double rz)
        {
            string str = coordinateMode == 0 ?
                string.Format("RelPointTool(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},", a1, b1, c1, d1, e1, f1) :
                string.Format("RelPointTool(joint={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},", a1, b1, c1, d1, e1, f1);
            
            str += string.Format("{{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}})", x, y, z, rx, ry, rz);
            return SendRecvMsg(str);
        }

        public string RelPointUser(int coordinateMode, double a1, double b1, double c1, double d1, double e1, double f1, double x, double y, double z, double rx, double ry, double rz)
        {
            string str = coordinateMode == 0 ?
                string.Format("RelPointUser(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},", a1, b1, c1, d1, e1, f1) :
                string.Format("RelPointUser(joint={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},", a1, b1, c1, d1, e1, f1);
            
            str += string.Format("{{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}})", x, y, z, rx, ry, rz);
            return SendRecvMsg(str);
        }

        public string RelJoint(double j1, double j2, double j3, double j4, double j5, double j6, double offset1, double offset2, double offset3, double offset4, double offset5, double offset6)
        {
            return SendRecvMsg(string.Format("RelJoint({0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6},{{{6:F6},{7:F6},{8:F6},{9:F6},{10:F6},{11:F6}}})", j1, j2, j3, j4, j5, j6, offset1, offset2, offset3, offset4, offset5, offset6));
        }

        public string ArcTrackStart()
        {
            return SendRecvMsg("ArcTrackStart()");
        }

        public string ArcTrackParams(int sampleTime, int coordinateType, double upDownCompensationMin, double upDownCompensationMax, double upDownCompensationOffset, double leftRightCompensationMin, double leftRightCompensationMax, double leftRightCompensationOffset)
        {
            return SendRecvMsg(string.Format("ArcTrackParams({0},{1},{2:F6},{3:F6},{4:F6},{5:F6},{6:F6},{7:F6})", sampleTime, coordinateType, upDownCompensationMin, upDownCompensationMax, upDownCompensationOffset, leftRightCompensationMin, leftRightCompensationMax, leftRightCompensationOffset));
        }

        public string ArcTrackEnd()
        {
            return SendRecvMsg("ArcTrackEnd()");
        }

        public string CheckMovC(double j1a, double j2a, double j3a, double j4a, double j5a, double j6a, double j1b, double j2b, double j3b, double j4b, double j5b, double j6b, double j1c, double j2c, double j3c, double j4c, double j5c, double j6c, int user = -1, int tool = -1, int a = -1, int v = -1, int cp = -1)
        {
            string str = string.Format("CheckMovC(joint={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},joint={{{6:F6},{7:F6},{8:F6},{9:F6},{10:F6},{11:F6}}},joint={{{12:F6},{13:F6},{14:F6},{15:F6},{16:F6},{17:F6}}}", j1a, j2a, j3a, j4a, j5a, j6a, j1b, j2b, j3b, j4b, j5b, j6b, j1c, j2c, j3c, j4c, j5c, j6c);
            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1) str += string.Format(",v={0}", v);
            if (cp != -1) str += string.Format(",cp={0}", cp);
            str += ")";
            return SendRecvMsg(str);
        }

        public string CheckMovJ(double j1a, double j2a, double j3a, double j4a, double j5a, double j6a, double j1b, double j2b, double j3b, double j4b, double j5b, double j6b, int user = -1, int tool = -1, int a = -1, int v = -1, int cp = -1)
        {
            string str = string.Format("CheckMovJ(joint={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},joint={{{6:F6},{7:F6},{8:F6},{9:F6},{10:F6},{11:F6}}}", j1a, j2a, j3a, j4a, j5a, j6a, j1b, j2b, j3b, j4b, j5b, j6b);
            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1) str += string.Format(",v={0}", v);
            if (cp != -1) str += string.Format(",cp={0}", cp);
            str += ")";
            return SendRecvMsg(str);
        }

        public string CnvInit(int index)
        {
            return SendRecvMsg(string.Format("CnvInit({0})", index));
        }

        public string CnvMovL(double j1, double j2, double j3, double j4, double j5, double j6, int user = -1, int tool = -1, int a = -1, int v = -1, int cp = -1, int r = -1)
        {
            string str = string.Format("CnvMovL(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}}", j1, j2, j3, j4, j5, j6);
            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1) str += string.Format(",v={0}", v);
            if (cp != -1) str += string.Format(",cp={0}", cp);
            if (r != -1) str += string.Format(",r={0}", r);
            str += ")";
            return SendRecvMsg(str);
        }

        public string EndRTOffset()
        {
            return SendRecvMsg("EndRTOffset()");
        }

        public string StartRTOffset()
        {
            return SendRecvMsg("StartRTOffset()");
        }

        public string FCCollisionSwitch(int enable)
        {
            return SendRecvMsg(string.Format("FCCollisionSwitch(enable={0})", enable));
        }

        public string SetFCCollision(double force, double torque)
        {
            return SendRecvMsg(string.Format("SetFCCollision({0:F6},{1:F6})", force, torque));
        }

        public string GetCnvObject(int objId)
        {
            return SendRecvMsg(string.Format("GetCnvObject({0})", objId));
        }

        public string DOGroupDEC(int group, int value)
        {
            return SendRecvMsg(string.Format("DOGroupDEC({0},{1})", group, value));
        }

        public string GetDOGroupDEC(int group, int value)
        {
            return SendRecvMsg(string.Format("GetDOGroupDEC({0},{1})", group, value));
        }

        public string DIGroupDEC(int group, int value)
        {
            return SendRecvMsg(string.Format("DIGroupDEC({0},{1})", group, value));
        }

        public string InverseSolution(double a1, double b1, double c1, double d1, double e1, double f1, int user = -1, int tool = -1, int isJoint = 0)
        {
            string str = string.Format("InverseSolution(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}}", a1, b1, c1, d1, e1, f1);
            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (isJoint != 0) str += string.Format(",isJoint={0}", isJoint);
            str += ")";
            return SendRecvMsg(str);
        }

        public string MoveL(double a1, double b1, double c1, double d1, double e1, double f1, int user = -1, int tool = -1, int a = -1, int v = -1, int speed = -1, int cp = -1, int r = -1)
        {
            string str = string.Format("MoveL(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}}", a1, b1, c1, d1, e1, f1);
            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1 && speed != -1) str += string.Format(",speed={0}", speed);
            else if (speed != -1) str += string.Format(",speed={0}", speed);
            else if (v != -1) str += string.Format(",v={0}", v);
            
            if (cp != -1 && r != -1) str += string.Format(",r={0}", r);
            else if (r != -1) str += string.Format(",r={0}", r);
            else if (cp != -1) str += string.Format(",cp={0}", cp);
            str += ")";
            return SendRecvMsg(str);
        }

        public string OffsetPara(double x, double y, double z, double rx, double ry, double rz)
        {
            return SendRecvMsg(string.Format("OffsetPara({0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6})", x, y, z, rx, ry, rz));
        }

        public string ResetRobot()
        {
            return SendRecvMsg("ResetRobot()");
        }

        public string RunTo(double a1, double b1, double c1, double d1, double e1, double f1, int moveType, int user = -1, int tool = -1, int a = -1, int v = -1)
        {
            string str = moveType == 0 ?
                string.Format("RunTo(pose={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},moveType=0", a1, b1, c1, d1, e1, f1) :
                string.Format("RunTo(joint={{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}},moveType=1", a1, b1, c1, d1, e1, f1);

            if (user != -1) str += string.Format(",user={0}", user);
            if (tool != -1) str += string.Format(",tool={0}", tool);
            if (a != -1) str += string.Format(",a={0}", a);
            if (v != -1) str += string.Format(",v={0}", v);
            str += ")";
            return SendRecvMsg(str);
        }

        public string SetArcTrackOffset(double offsetX, double offsetY, double offsetZ, double offsetRx, double offsetRy, double offsetRz)
        {
            return SendRecvMsg(string.Format("SetArcTrackOffset({{{0:F6},{1:F6},{2:F6},{3:F6},{4:F6},{5:F6}}})", offsetX, offsetY, offsetZ, offsetRx, offsetRy, offsetRz));
        }

        public string SetCnvPointOffset(double xOffset, double yOffset)
        {
            return SendRecvMsg(string.Format("SetCnvPointOffset({0:F6},{1:F6})", xOffset, yOffset));
        }

        public string SetCnvTimeCompensation(int time)
        {
            return SendRecvMsg(string.Format("SetCnvTimeCompensation({0})", time));
        }

        public string StartSyncCnv()
        {
            return SendRecvMsg("StartSyncCnv()");
        }

        public string StopSyncCnv()
        {
            return SendRecvMsg("StopSyncCnv()");
        }

        public string TcpSendAndParse(string cmd)
        {
            return SendRecvMsg(string.Format("TcpSendAndParse(\"{0}\")", cmd));
        }

        public string Sleep(int count)
        {
            return SendRecvMsg(string.Format("Sleep({0})", count));
        }
    }
}
