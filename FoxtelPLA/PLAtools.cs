using SimpleBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FoxtelPLA
{
    class PLAtools
    {
        private string NP508IP = "http://192.168.1.200";
        private Browser NP508;

        public PLAtools()
        {
            NP508 = new Browser();
        }

        public void loginNP508()
        {
            // string  NP508IP ="http://192.168.1.200";
            //  Browser NP508 = new Browser();
            NP508.Navigate(NP508IP);
            string loginString = NP508IP + "/login?username=admin&password=admin";
            NP508.Navigate(loginString);


        }

        public void logoutNP508()
        {
            string logoutString = NP508IP + "/logout";
            NP508.Navigate(logoutString);
        }
        public void NP508GetWirelessInfo(ref string SSID, ref string password)
        {
            var _SSID = SSID;
            var _password = password;
            string wanurl = NP508IP + "/wlcfg.shtml";
            NP508.Navigate(wanurl);
            //   string _pattern = @"var VapPara = '([^']+)';";
            string _pattern = @"var VapPara = \[\n\t\[(.+)";
            // match eveything .  after the new line tab /n/t   and place stuff within bracket into to group variable 1)

            Match match = Regex.Match(NP508.CurrentHtml, _pattern);
            string stuff = match.Groups[1].Value;
            string[] values = stuff.Split(',');
            _pattern = @"'(.+)'";


            match = Regex.Match(values[8], _pattern);
            SSID = match.Groups[1].Value;
            match = Regex.Match(values[13], _pattern);
            password = match.Groups[1].Value;


        }

        public void NP508SetWirelessInfo(string SSID, string password)
        {
            string setwireless = "/wlcfg.shtml?RadioEnabled=1&RadioIndex=1&Standard=ng20&Channel=0&X_ATH-COM_Powerlevel=1&X_ATH-COM_CWMEnable=1&X_ATH-COM_AMPDUEnabled=1&X_ATH-COM_AMPDUFrames=32&X_ATH-COM_AMPDULimit=50000&X_ATH-COM_Txchainmask=0&X_ATH-COM_Rxchainmask=0&X_ATH-COM_TBRLimit=50000&X_ATH-COM_VoWEnabled=1&Enable=1&VapIndex=1&X_ATH-COM_RadioIndex=1&X_ATH-COM_SSIDHide=0"
                 + "&SSID=" + SSID + "&DeviceOperationMode=RootAP&X_ATH-COM_ShortGI=1&X_ATH-COM_WMM=1&X_ATH-COM_HT40Coexist=0&RTS=off&Fragmentation=off&X_ATH-COM_MEMode=Translate&X_ATH-COM_MELength=32&X_ATH-COM_METimer=30000&X_ATH-COM_METimeout=120000&X_ATH-COM_MEDropMcast=1&X_ATH-COM_HBREnable=1&X_ATH-COM_HBRPERLow=20&X_ATH-COM_HBRPERHigh=35&BeaconType=11i&X_ATH-COM_WPSConfigured=CONFIGURED&X_ATH-COM_WPSPin=56332117&WPAAuthenticationMode=PSKAuthentication&IEEE11iAuthenticationMode=PSKAuthentication&AuthenticationServiceMode=None&WAPIAuthenticationMode=PSKAuthentication&KeyPassphrase="
                 + password + "&PreSharedKey=&WPAEncryptionModes=TKIPEncryption&IEEE11iEncryptionModes=AESEncryption&X_ATH-COM_RSNPreAuth=0&X_ATH-COM_EAPReauthPeriod=3600&BasicAuthenticationMode=Both&WEPKeyIndex=1&WEPKey.1.WEPKey=&WEPKey.2.WEPKey=&WEPKey.3.WEPKey=&WEPKey.4.WEPKey=&X_ATH-COM_WEPRekeyPeriod=300&X_ATH-COM_AuthServerAddr=&X_ATH-COM_AuthServerPort=0&X_ATH-COM_AuthServerSecret=&WAPIUcastRekeyTime=86400&WAPIUcastRekeyPacket=67108864&WAPIMcastRekeyTime=86400&WAPIMcastRekeyPacket=67108864&WAPIPSKType=Ascii&WAPIPreAuth=0&WAPIPSK=&WAPICertIndex=X509&WAPICertStatus=CertValid&WAPIASUAddress=&WAPIASUPort=3810&ParseWAPICert=&UserCert=&CACert=&WAPICertMode=Normal";
            setwireless = NP508IP + setwireless;
            NP508.Navigate(setwireless);

        }



    }
}
