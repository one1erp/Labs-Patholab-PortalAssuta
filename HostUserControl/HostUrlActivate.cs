using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LSExtensionWindowLib;
using LSSERVICEPROVIDERLib;
using System.Runtime.InteropServices;
using Patholab_Common;
using Patholab_DAL_V1;



namespace HostUserControl
{
    [ComVisible(true)]
    [ProgId("PortalAssuta.PortalAssuta")]
    public partial class HostUrlActivate : UserControl, IExtensionWindow
    {

        #region Private members

        private INautilusProcessXML xmlProcessor;
        private INautilusUser _ntlsUser;
        private IExtensionWindowSite2 _ntlsSite;
        private INautilusServiceProvider sp;
        private INautilusDBConnection _ntlsCon;
        private DataLayer dal = null;


        #endregion


        public HostUrlActivate()
        {
            try
            {
                InitializeComponent();
                this.Disposed += ActivateUrl_Disposed;
                BackColor = Color.FromName("Control");
                this.Dock = DockStyle.Fill;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void ActivateUrl_Disposed(object sender, EventArgs e)
        {
            GC.Collect();
        }

        public bool CloseQuery()
        {
            DialogResult res = MessageBox.Show(@"?האם אתה בטוח שברצונך לצאת ", "Activate Url", MessageBoxButtons.YesNo);

            if (res == DialogResult.Yes)
            {


                if (dal != null) dal.Close();
                if (_ntlsSite != null) _ntlsSite = null;

                this.Dispose();

                return true;
            }
            else
            {
                return false;
            }
        }

        public WindowRefreshType DataChange()
        {
            return LSExtensionWindowLib.WindowRefreshType.windowRefreshNone;
        }

        public WindowButtonsType GetButtons()
        {
            return LSExtensionWindowLib.WindowButtonsType.windowButtonsNone;
        }

        public void Internationalise()
        {

        }

        public void PreDisplay()
        {
            xmlProcessor = Utils.GetXmlProcessor(sp);

            _ntlsUser = Utils.GetNautilusUser(sp);

            activateUrlWindow();
        }

        public void RestoreSettings(int hKey)
        {

        }

        public bool SaveData()
        {
            return true;
        }

        public void SaveSettings(int hKey)
        {

        }

        public void SetParameters(string parameters)
        {

        }

        public void SetServiceProvider(object serviceProvider)
        {
            sp = serviceProvider as NautilusServiceProvider;
            _ntlsCon = Utils.GetNtlsCon(sp);
        }

        public void SetSite(object site)
        {
            _ntlsSite = (IExtensionWindowSite2)site;
            _ntlsSite.SetWindowInternalName("Activate URL");
            _ntlsSite.SetWindowRegistryName("Activate_URL");
            _ntlsSite.SetWindowTitle("Activate URL");
        }

        public void Setup()
        {

        }

        public WindowRefreshType ViewRefresh()
        {
            return LSExtensionWindowLib.WindowRefreshType.windowRefreshNone;
        }

        public void refresh()
        {

        }

        //private void openConnection()
        //{
        //    try
        //    {
        //        connection = new OracleConnection();
        //        List<string> conStringParts = _ntlsCon.GetADOConnectionString().Split(';').ToList();
        //        string[] conStringValues = new string[] { "data", "user", "password"};
        //        List<string> conString = new List<string>();

        //        foreach (string part in conStringParts)
        //        {
        //            foreach (string value in conStringValues)
        //            {
        //                if (part.ToLower().Contains(value))
        //                {
        //                    conString.Add(part);
        //                }
        //            }
        //        }

        //        connection.ConnectionString = String.Join(";", conString);
        //        connection.Open();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Problem in Openning Connection!" + Environment.NewLine + ex.Message);
        //    }
        //}



        private PortalAssuta urlToOpen;

        private void activateUrlWindow()
        {
            try
            {
                //openConnection();
                dal = new DataLayer();
                dal.Connect(_ntlsCon);
                urlToOpen = new PortalAssuta(dal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Opens portal page for specified sdg
        private void buttonOpenPortal_Click(object sender, EventArgs e)
        {
            string userID = string.Empty;
            if (!string.IsNullOrEmpty(textBoxEnterSDG.Text))
            {
                dal.RefreshAll();
                SDG sdg = dal.FindBy<SDG>(s => s.NAME == textBoxEnterSDG.Text).FirstOrDefault();

                if(sdg != null)
                {
                    userID = sdg.SDG_USER.CLIENT.NAME;
                    userID = sdg.SDG_USER.CLIENT.CLIENT_USER.U_PASSPORT.ToUpper() == "T" ? userID : addLeadingZero(userID);

                    if (!string.IsNullOrEmpty(userID))
                    {
                        urlToOpen.openPortalForUser(userID);
                    }
                    else
                    {
                        MessageBox.Show("Error fetching user ID.");
                    } 
                }
                else 
                {
                    MessageBox.Show("No SDG with the specified name.");
                }
            }
        }

        private static void openPoretal(DataLayer i_Dal, string i_SdgId)
        {
            string userID = string.Empty;

            i_Dal.RefreshAll();
            SDG sdg = i_Dal.FindBy<SDG>(s => s.NAME == i_SdgId).FirstOrDefault();

            if (sdg != null)
            {
                userID = sdg.SDG_USER.CLIENT.NAME;
                userID = sdg.SDG_USER.CLIENT.CLIENT_USER.U_PASSPORT.ToUpper() == "T" ? userID : addLeadingZero(userID);

                if (!string.IsNullOrEmpty(userID))
                {
                    PortalAssuta.openPortal(i_Dal, userID);
                }
                else
                {
                    MessageBox.Show("Error fetching user ID.");
                }
            }
            else
            {
                MessageBox.Show("No SDG with the specified name.");
            }
        }

        // Adds leading zeros to the id if necessary.
        private static string addLeadingZero(string id)
        {
            string zeros = string.Empty;

            for (int i = 0; i < 9 - id.Length; i++)
            {
                zeros += "0";
            }
            return zeros + id;
        }

        private void HostUrlActivate_SizeChanged(object sender, EventArgs e)
        {
            //chromeBrowser.Size = new Size(chromeBrowser.Parent.Width, chromeBrowser.Parent.Height - textBoxEnterSDG.Bottom);
        }

        private void textBoxEnterSDG_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                buttonOpenPortal_Click(null, null);
            }
        }
    }
}
