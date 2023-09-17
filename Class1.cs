using NetFwTypeLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 마크서버_만들기
{
    internal class Class1
    {
        public static class FirewallHelper
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Method
            ////////////////////////////////////////////////////////////////////////////////////////// Static
            //////////////////////////////////////////////////////////////////////////////// Public

            #region 애플리케이션 방화벽 열기 - OpenApplicationFirewall(applicationName, applicationFilePath)

            /// <summary>
            /// 애플리케이션 방화벽 열기
            /// </summary>
            public static void OpenApplicationFirewall(string applicationName, string applicationFilePath)
            {
                INetFwAuthorizedApplications netFwAuthorizedApplications = null;
                INetFwAuthorizedApplication netFwAuthorizedApplication = null;

                try
                {
                    if (!FindApplication(applicationName))
                    {
                        INetFwProfile netFwProfile = GetProfile();

                        netFwAuthorizedApplications = netFwProfile.AuthorizedApplications;

                        netFwAuthorizedApplication = GetInstance("INetAuthApp") as INetFwAuthorizedApplication;

                        netFwAuthorizedApplication.Name = applicationName;
                        netFwAuthorizedApplication.ProcessImageFileName = applicationFilePath;

                        netFwAuthorizedApplications.Add(netFwAuthorizedApplication);
                    }
                }
                finally
                {
                    if (netFwAuthorizedApplications != null)
                    {
                        netFwAuthorizedApplications = null;
                    }

                    if (netFwAuthorizedApplication != null)
                    {
                        netFwAuthorizedApplication = null;
                    }
                }
            }

            #endregion
            #region 애플리케이션 방화벽 닫기 - CloseApplicationFirewall()

            /// <summary>
            /// 애플리케이션 방화벽 닫기
            /// </summary>
            public static void CloseApplicationFirewall(string applicationName, string applicationFilePath)
            {
                INetFwAuthorizedApplications netFwAuthorizedApplications = null;

                try
                {
                    if (FindApplication(applicationName) == true)
                    {
                        INetFwProfile netFwProfile = GetProfile();

                        netFwAuthorizedApplications = netFwProfile.AuthorizedApplications;

                        netFwAuthorizedApplications.Remove(applicationFilePath);
                    }
                }
                finally
                {
                    if (netFwAuthorizedApplications != null)
                    {
                        netFwAuthorizedApplications = null;
                    }
                }
            }

            #endregion

            #region 포트 방화벽 열기 - OpenPortFirewall(portName, port, protocol)

            /// <summary>
            /// 포트 방화벽 열기
            /// </summary>
            /// <param name="portName">포트명</param>
            /// <param name="port">포트</param>
            /// <param name="protocol">프로토콜</param>
            public static void OpenPortFirewall(string portName, int port, NET_FW_IP_PROTOCOL_ protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP)
            {
                INetFwOpenPorts netFwOpenPorts = null;
                INetFwOpenPort netFwOpenPort = null;

                try
                {
                    if (!IsPortFound(port))
                    {
                        INetFwProfile netFwProfile = GetProfile();

                        netFwOpenPorts = netFwProfile.GloballyOpenPorts;

                        netFwOpenPort = GetInstance("INetOpenPort") as INetFwOpenPort;

                        netFwOpenPort.Name = portName;
                        netFwOpenPort.Port = port;
                        netFwOpenPort.Protocol = protocol;

                        netFwOpenPorts.Add(netFwOpenPort);
                    }
                }
                finally
                {
                    if (netFwOpenPorts != null)
                    {
                        netFwOpenPorts = null;
                    }

                    if (netFwOpenPort != null)
                    {
                        netFwOpenPort = null;
                    }
                }
            }

            #endregion
            #region 포트 방화벽 닫기 - ClosePortFirewall(port, protocol)

            /// <summary>
            /// 포트 방화벽 닫기
            /// </summary>
            /// <param name="port">포트</param>
            /// <param name="protocol">프로토콜</param>
            public static void ClosePortFirewall(int port, NET_FW_IP_PROTOCOL_ protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP)
            {
                INetFwOpenPorts netFwOpenPorts = null;

                try
                {
                    if (IsPortFound(port))
                    {
                        INetFwProfile netFwProfile = GetProfile();

                        netFwOpenPorts = netFwProfile.GloballyOpenPorts;

                        netFwOpenPorts.Remove(port, protocol);
                    }
                }
                finally
                {
                    if (netFwOpenPorts != null)
                    {
                        netFwOpenPorts = null;
                    }
                }
            }

            #endregion

            //////////////////////////////////////////////////////////////////////////////// Private

            #region 애플리케이션 찾기 - FindApplication(applicationName)

            /// <summary>
            /// 애플리케이션 찾기
            /// </summary>
            /// <param name="applicationName">애플리케이션명</param>
            /// <returns>처리 결과</returns>
            private static bool FindApplication(string applicationName)
            {
                bool result = false;

                Type programType = null;
                INetFwMgr netFwMgr = null;
                INetFwAuthorizedApplications netFwAuthorizedApplications = null;
                INetFwAuthorizedApplication netFwAuthorizedApplication = null;

                try
                {
                    programType = Type.GetTypeFromProgID("HNetCfg.FwMgr");

                    netFwMgr = Activator.CreateInstance(programType) as INetFwMgr;

                    if (netFwMgr.LocalPolicy.CurrentProfile.FirewallEnabled)
                    {
                        netFwAuthorizedApplications = netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications;

                        IEnumerator enumerator = netFwAuthorizedApplications.GetEnumerator();

                        while (enumerator.MoveNext())
                        {
                            netFwAuthorizedApplication = enumerator.Current as INetFwAuthorizedApplication;

                            if (netFwAuthorizedApplication.Name == applicationName)
                            {
                                result = true;

                                break;
                            }
                        }
                    }
                }
                finally
                {
                    if (programType != null)
                    {
                        programType = null;
                    }

                    if (netFwMgr != null)
                    {
                        netFwMgr = null;
                    }

                    if (netFwAuthorizedApplications != null)
                    {
                        netFwAuthorizedApplications = null;
                    }

                    if (netFwAuthorizedApplication != null)
                    {
                        netFwAuthorizedApplication = null;
                    }
                }

                return result;
            }

            #endregion
            #region 포트 발견 여부 구하기 - IsPortFound(port)

            /// <summary>
            /// 포트 발견 여부 구하기
            /// </summary>
            /// <param name="port">포트</param>
            /// <returns>포트 발견 여부</returns>
            private static bool IsPortFound(int port)
            {
                bool result = false;

                INetFwOpenPorts netFwOpenPorts = null;
                Type programType = null;
                INetFwMgr netFwMgr = null;
                INetFwOpenPort netFwOpenPort = null;

                try
                {
                    programType = Type.GetTypeFromProgID("HNetCfg.FwMgr");

                    netFwMgr = Activator.CreateInstance(programType) as INetFwMgr;
                    netFwOpenPorts = netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts;

                    IEnumerator portEnumerate = netFwOpenPorts.GetEnumerator();

                    while (portEnumerate.MoveNext())
                    {
                        netFwOpenPort = portEnumerate.Current as INetFwOpenPort;

                        if (netFwOpenPort.Port == port)
                        {
                            result = true;

                            break;
                        }
                    }
                }
                finally
                {
                    if (netFwOpenPorts != null)
                    {
                        netFwOpenPorts = null;
                    }

                    if (programType != null)
                    {
                        programType = null;
                    }

                    if (netFwMgr != null)
                    {
                        netFwMgr = null;
                    }

                    if (netFwOpenPort != null)
                    {
                        netFwOpenPort = null;
                    }
                }

                return result;
            }

            #endregion
            #region 프로필 구하기 - GetProfile()

            /// <summary>
            /// 프로필 구하기
            /// </summary>
            private static INetFwProfile GetProfile()
            {
                INetFwMgr netFwMgr = null;
                INetFwPolicy netFwPolicy = null;

                try
                {
                    netFwMgr = GetInstance("INetFwMgr") as INetFwMgr;
                    netFwPolicy = netFwMgr.LocalPolicy;

                    return netFwPolicy.CurrentProfile;
                }
                finally
                {
                    if (netFwMgr != null)
                    {
                        netFwMgr = null;
                    }

                    if (netFwPolicy != null)
                    {
                        netFwPolicy = null;
                    }
                }
            }

            #endregion
            #region 인스턴스 구하기 - GetInstance(typeName)

            /// <summary>
            /// 인스턴스 구하기
            /// </summary>
            /// <param name="typeName">타입명</param>
            /// <returns>인스턴스</returns>
            private static object GetInstance(string typeName)
            {
                Type type = null;

                switch (typeName)
                {
                    case "INetFwMgr":

                        type = Type.GetTypeFromCLSID(new Guid("{304CE942-6E39-40D8-943A-B913C40C9CD4}"));

                        return Activator.CreateInstance(type);

                    case "INetAuthApp":

                        type = Type.GetTypeFromCLSID(new Guid("{EC9846B3-2762-4A6B-A214-6ACB603462D2}"));

                        return Activator.CreateInstance(type);

                    case "INetOpenPort":

                        type = Type.GetTypeFromCLSID(new Guid("{0CA545C6-37AD-4A6C-BF92-9F7610067EF5}"));

                        return Activator.CreateInstance(type);

                    default:

                        return null;
                }
            }

            #endregion
        }
    }
}
