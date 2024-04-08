using Patholab_DAL_V1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using forms = System.Windows.Forms;

namespace HostUserControl
{
    /// <summary>
    /// Interaction logic for PortalAssuta.xaml
    /// </summary>
    public partial class PortalAssuta : UserControl
    {
        private DataLayer dal = null;

        public PortalAssuta(DataLayer i_Dal)
        {
            InitializeComponent();
            dal = i_Dal;
        }

        // Opens portal page for user with id {userID}
        public void openPortalForUser(string userID)
        {
            openPortal(dal, userID);
        }

        public static void openPortal(DataLayer i_Dal, string userID)
        {
            try
            {
                if (i_Dal != null)
                {
                    PHRASE_HEADER phrase = i_Dal.FindBy<PHRASE_HEADER>(header => header.NAME == "portal assuta").FirstOrDefault();

                    if (phrase != null)
                    {
                        string username = string.Empty;
                        string password = string.Empty;
                        string domain = string.Empty;
                        Dictionary<string, string> parameters = new Dictionary<string, string>() 
                    {
                        {"username" , string.Empty},
                        {"password" , string.Empty},
                        {"domain" , string.Empty},
                        {"url" , string.Empty},
                        {"tosearch" , string.Empty},
                        {"parametername" , string.Empty},
                    };

                        foreach (PHRASE_ENTRY entry in phrase.PHRASE_ENTRY)
                        {
                            switch (entry.PHRASE_NAME.ToLower())
                            {
                                case "username":
                                    parameters["username"] = entry.PHRASE_DESCRIPTION;
                                    break;
                                case "password":
                                    parameters["password"] = entry.PHRASE_DESCRIPTION;
                                    break;
                                case "domain":
                                    parameters["domain"] = entry.PHRASE_DESCRIPTION;
                                    break;
                                case "url":
                                    parameters["url"] = entry.PHRASE_DESCRIPTION;
                                    break;
                                case "tosearch":
                                    parameters["tosearch"] = entry.PHRASE_DESCRIPTION;
                                    break;
                                case "parametername":
                                    parameters["parametername"] = entry.PHRASE_DESCRIPTION;
                                    break;
                                default:
                                    MessageBox.Show("unknown entry in phrase");
                                    break;
                            }
                        }

                        if (!string.IsNullOrEmpty(parameters["username"]) && !string.IsNullOrEmpty(parameters["password"]))
                        {
                            string prefix = string.Empty;
                            string suffix = string.Empty;

                            if (parameters["url"].Contains("http://"))
                            {
                                prefix = "http://";
                                suffix = parameters["url"].Substring(parameters["url"].IndexOf("http://") + 7);
                            }
                            else if (parameters["url"].Contains("https://"))
                            {
                                prefix = "https://";
                                suffix = parameters["url"].Substring(parameters["url"].IndexOf("https://") + 8);
                            }
                            else
                            {
                                suffix = parameters["url"];
                            }

                            string url = prefix + parameters["username"] + ":" + parameters["password"] + "@" + suffix + parameters["tosearch"] + parameters["parametername"] + userID.ToString();

                            Process.Start("chrome.exe", url);
                        }
                        else
                        {
                            MessageBox.Show("Password or Username are empty!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can't find phrase.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    // Handles windows security window - automating credentials
    //public class NetworkConnection : IDisposable
    //    {
    //        string _networkName;

    //        public NetworkConnection(string networkName,
    //            NetworkCredential credentials)
    //        {
    //            _networkName = networkName;

    //            var netResource = new NetResource()
    //            {
    //                Scope = ResourceScope.GlobalNetwork,
    //                ResourceType = ResourceType.Disk,
    //                DisplayType = ResourceDisplaytype.Share,
    //                RemoteName = networkName
    //            };

    //            var userName = string.IsNullOrEmpty(credentials.Domain)
    //                ? credentials.UserName
    //                : string.Format(@"{0}\{1}", credentials.Domain, credentials.UserName);

    //            var result = WNetAddConnection2(
    //                netResource,
    //                credentials.Password,
    //                userName,
    //                0);

    //            if (result != 0)
    //            {
    //                try
    //                {
    //                    throw new Win32Exception(result);
    //                }
    //                catch (Exception ex)
    //                {
    //                    MessageBox.Show(ex.Message , "Credential error!", MessageBoxButton.OK, MessageBoxImage.Error);                     
    //                }
    //            }
    //            else
    //            {
    //                MessageBox.Show("Successful login!");
    //            }
    //        }

    //        ~NetworkConnection()
    //        {
    //            Dispose(false);
    //        }

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }

    //        protected virtual void Dispose(bool disposing)
    //        {
    //            WNetCancelConnection2(_networkName, 0, true);
    //        }

    //        [DllImport("mpr.dll")]
    //        private static extern int WNetAddConnection2(NetResource netResource,
    //            string password, string username, int flags);

    //        [DllImport("mpr.dll")]
    //        private static extern int WNetCancelConnection2(string name, int flags,
    //            bool force);
    //    }

    //    [StructLayout(LayoutKind.Sequential)]
    //    public class NetResource
    //    {
    //        public ResourceScope Scope;
    //        public ResourceType ResourceType;
    //        public ResourceDisplaytype DisplayType;
    //        public int Usage;
    //        public string LocalName;
    //        public string RemoteName;
    //        public string Comment;
    //        public string Provider;
    //    }

    //    public enum ResourceScope : int
    //    {
    //        Connected = 1,
    //        GlobalNetwork,
    //        Remembered,
    //        Recent,
    //        Context
    //    };

    //    public enum ResourceType : int
    //    {
    //        Any = 0,
    //        Disk = 1,
    //        Print = 2,
    //        Reserved = 8,
    //    }

    //    public enum ResourceDisplaytype : int
    //    {
    //        Generic = 0x0,
    //        Domain = 0x01,
    //        Server = 0x02,
    //        Share = 0x03,
    //        File = 0x04,
    //        Group = 0x05,
    //        Network = 0x06,
    //        Root = 0x07,
    //        Shareadmin = 0x08,
    //        Directory = 0x09,
    //        Tree = 0x0a,
    //        Ndscontainer = 0x0b
    //    }
}
