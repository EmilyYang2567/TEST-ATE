using BarCode_FormsApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ClassStruct
{
    public struct Priv_T
    {
        public bool bScanOK;
        public bool bWONoOK;
        public bool bOPIdOk;
        public bool bSNOk;
        public bool bMACOk;
        public bool bCSNOk;

        public int scanMode;
        public string sSnPrefix;
        public string sMAC_Prefix;
        public string currSN;
        public string currCSN;
        public string currMAC;
        public string currWONO;
        public string currOPID;
        private BarcodeScan barCodeForm;

        public Priv_T(BarcodeScan barCodeForm) : this()
        {
            this.barCodeForm = barCodeForm;
        }
    };

    public static Priv_T bc_Priv;
    
}

public class OptStruct
{
    public struct OptAte_T
    {
        public int SnLen;
        public bool ScanSN;
        public int CSnLen;
        public bool ScanCSN;
        public bool ScanMAC;
        public bool ScanWONO;
        public bool ScanOPID;
        public bool ScanSNOk;
        public int ScanMode;
        public string WoNoDef;
        public string OpIdDef;
        public bool TestItem1;
        public bool TestItem2;
        public bool TestItem3;
        public bool bAutoShowScanBox;

    }

    public struct OptShopFlow_T
    {
        public bool ShopFlowEnable;
        public string ConnectSec;
        public string PartNmb;
        public string ProgramName;
        public string ProgramVersion;
        public string[] Errorcode;
        public bool EnableBatchFunction;
        public string ModelNo;
        public string TestStationNo;
    }

    public struct OptPriv_T
    {
        public bool bIsParamsOK;
        public bool bCanClose;
        public string SelModel;
        public string sStartTime;
        public string sTestTime;
       
        public bool bStopFlag;

        public OptAte_T opt_Ate;

        public OptShopFlow_T Shopflow;
        public BarcodeScan barCodeForm;

        public OptPriv_T(BarcodeScan barCodeForm) : this()
        {
            this.barCodeForm = barCodeForm;
        }
    }

    public static OptPriv_T Priv;
}
