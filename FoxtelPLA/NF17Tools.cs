using SimpleBrowser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FoxtelPLA
{
    class NF17Tools
    {   public string SSID; 
        string configureModem;
        public string wPassword;

        // this string gives indication of nf17 state
        static string getSessionID(string html)
        {      // static since we dont need to instance another program class to run it

            string _pattern = @"var sessionKey='([^']+)';";
            Match match = Regex.Match(html, _pattern);
            return match.Groups[1].Value;

        }

            string getModemModel(string html)
        {
            string modemtype = "default";
            string _pattern = @"NF4V";
            Match match = Regex.Match(html, _pattern);
            if (match.Success)
            {
                modemtype = "NF4V";

            }

            return (modemtype);
        }

        bool checkDSLError(string html)
        {      // static since we dont need to instance another program class to run it

            string _pattern = "DSL Interface Configuration Error";
            Match match = Regex.Match(html, _pattern);

            if (match.Success)
                configureModem = "Configuration Failed , please check modem has been reset";

            return match.Success;

        }

        static string ACSConfigString(string sessionKey)
        /* string Tr69cAcsPwd,
         string tr69cConnReqUser,
         string tr69cConnReqPwd,
         string tr69cConnReqPort,
         string tr69cAcsUrl,
         string informInterval) */
        {
            string _acspost;
            _acspost = "tr69cfg.cgi?tr69cEnable=1&tr69cInformEnable=1&tr69cInformInterval=86423&tr69cAcsURL=http://acs.netcommwireless.com/cpe.php&tr69cAcsUser=cpe&tr69cAcsPwd=cpe&tr69cConnReqUser=cpe&tr69cConnReqPwd=cpe&tr69cConnReqPort=30005&tr69cNoneConnReqAuth=0&tr69cDebugEnable=0&tr69cBoundIfName=Any_WAN&sessionKey=";
            _acspost = _acspost + sessionKey;
            // Console.Write(_acspost);
            return (_acspost);
        }



        public void nf17ACVsetup()
        {
           
            var NF17modem = new Browser();
            Browser.RefererModes ModemBrowserMode;
            // add referrer for NF4V"
            ModemBrowserMode = Browser.RefererModes.OriginWhenCrossOrigin;
            // add referrer for NF4V"


            NF17modem.RefererMode = ModemBrowserMode;
            var modemURL = "http://192.168.20.1/";

            int timeOut = 3000;
            NF17modem.Navigate(modemURL, timeOut);

            if (NF17modem.RequestData().ResponseCode != 401)
            {
                
                configureModem = "modem is not online, please connect,wait and click again";
                return;
            }

            //    Console.Write("get url");
            //    modem.BasicAuthenticationLogin("Broadband Router","admin","admin");
            NF17modem.BasicAuthenticationLogin("192.168.20.1", "admin", "admin");

            modemURL = "http://192.168.20.1";
          
            NF17modem.Navigate(modemURL);

            modemURL = "http://192.168.20.1/info.html";
            NF17modem.Navigate(modemURL);

            string modemtype = getModemModel(NF17modem.CurrentHtml);
            if (modemtype == "NF4V")
            { modemURL = "http://192.168.20.1/wlswitchinterface0.wl"; }
            else
            modemURL = "http://192.168.20.1/wlswitchinterface1.wl";

            NF17modem.Navigate(modemURL);

            string _pattern = @"var ssid = '([^']+)';";
            Match match = Regex.Match(NF17modem.CurrentHtml, _pattern);
            SSID = match.Groups[1].Value;


            modemURL = "http://192.168.20.1/wlsecurity.html";

            NF17modem.Navigate(modemURL);

             _pattern = @"var wpaPskKey = '([^']+)';";

            match = Regex.Match(NF17modem.CurrentHtml, _pattern);
            wPassword = match.Groups[1].Value;
        }


        void configureACS(Browser modem)
        {
            configureModem = "Configuring ACS";
            var modemURL = "http://192.168.20.1/tr69cfg.html";
            int timeOut = 3000;
            if (!modem.Navigate(modemURL, timeOut))
            {
                configureModem = "modem is not connected, please connect and click again";
                return;
            }

            var html = modem.CurrentHtml;
            string sessionKey = getSessionID(html);

            var getstringACS = ACSConfigString(sessionKey);
            modemURL = "http://192.168.20.1/";
            modemURL = modemURL + getstringACS;
            modem.Navigate(modemURL);
        }
        }
}
