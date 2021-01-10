using System;
using System.Collections.Generic;
using System.Linq;
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

using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SampleVendingMachine
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private VendingMachine m_vm;                                                    // 自動販売機実行ファイルの構造体
        public delegate void MyEventHandler(object sender, DataReceivedEventArgs e);    // イベントハンドラ
        public event MyEventHandler MyEvent = null;                                     // イベントハンドラ
        private string logFilePath;                                                     // ログファイルパス
        private string communicationFileDir;                                            // 
        private string stdOutDataFilePath;                                              // 自動販売機データ出力ファイルパス
        private System.IO.FileSystemWatcher m_fw;                                       // ファイル変更監視
        private bool shouldRead;                                                        // ファイル監視でエディタ仕様で複数回更新するので、1回のみ読み込み管理用フラグ

        public MainWindow()
        {
            string modulePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\vending_machine.exe";
            communicationFileDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            logFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\VendingMachineLog.txt";
            stdOutDataFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\System_File.txt";

            m_fw = new System.IO.FileSystemWatcher(communicationFileDir, "System_File.txt");

            InitializeComponent();

            // 初期化
            Init_VendingMachine(modulePath);
            Init_GUI();

            Logging("VendingMachine Wakeup", "Start", false);
        }

        #region "VendingMachine"
        /// <summary>
        /// 自動販売機実行ファイル関連の初期化
        /// 連携パラメータの初期化
        /// 自動販売機実行ファイルの実行
        /// 自動販売機実行ファイルの標準入出力を非同期で監視
        /// </summary>
        /// <param name="modulePath"></param>
        private void Init_VendingMachine(string modulePath)
        {
            MyEvent = new MyEventHandler(event_DataReceived);

            m_vm = new VendingMachine();
            m_vm.modulePath = modulePath;
            m_vm.process = new Process();
            m_vm.payment = 0;
            m_vm.isActiveButtons = new bool[8] { false, false, false, false,
                                                 false, false, false, false };
            m_vm.hasItem = false;
            m_vm.hasChange = false;

            m_vm.process.StartInfo.FileName = m_vm.modulePath;
            m_vm.process.StartInfo.UseShellExecute = false;
            m_vm.process.StartInfo.RedirectStandardOutput = true;
            m_vm.process.StartInfo.RedirectStandardError = true;
            m_vm.process.StartInfo.RedirectStandardInput = true;
            m_vm.process.OutputDataReceived += Process_OutputDataReceived;
            m_vm.process.ErrorDataReceived += Scheme_ErrorDataReceived;

            m_vm.process.StartInfo.CreateNoWindow = false;


            m_vm.process.Start();
            //m_vm.process.BeginOutputReadLine();
            //m_vm.process.BeginErrorReadLine();

            //m_fw.Filter = stdOutDataFilePath;
            m_fw.NotifyFilter = System.IO.NotifyFilters.FileName |
                                System.IO.NotifyFilters.DirectoryName |
                                System.IO.NotifyFilters.LastWrite;

            m_fw.Changed += FileChanged;
            m_fw.EnableRaisingEvents = true;
            
        }


        /// <summary>
        /// 自動販売機実行ファイルから取得するパラメータ用構造体
        /// </summary>
        private struct VendingMachine
        {
            public string modulePath;       // 自動販売機実行ファイルのパス
            public Process process;         // 自動販売機実行ファイルの実行プロセス
            public int payment;             // 自動販売機実行ファイルから取得する合計入力金額
            public bool[] isActiveButtons;  // 自動販売機実行ファイルから取得する販売ボタン郡の使用可否状態
            public bool hasItem;            // 自動販売機実行ファイルから取得する購入済み飲み物の表示状態
            public bool hasChange;          // 自動販売機実行ファイルから取得するおつりの表示状態
            public int change;              // 自動販売機実行ファイルから取得するおつり金額
        }
        #endregion // VendingMachine

        private void UpdateGUI()
        {
            Logging("GUIUpdate", "Update");

            UpdateVendingMachine(m_vm);

            UpdateGUI_TotalPayment(m_vm);
        }

        #region "UpdateGUI"
        List<Image> payImages;

        private void UpdateVendingMachine(VendingMachine vm)
        {

        }

        /// <summary>
        /// GUIパラメータの初期化
        /// 投入金額Imageリストの初期化
        /// </summary>
        private void Init_GUI()
        {
            payImages = new List<Image>();
            payImages.Add(image_coin1);
            payImages.Add(image_coin2);
            payImages.Add(image_coin3);
            payImages.Add(image_coin4);
            payImages.Add(image_coin5);

            foreach (Image item in payImages)
            {
                UpdateGUI_SetNumber(item, 0);
            }
        }

        /// <summary>
        /// 合計金額の表示更新
        /// 5桁目から順に数値を判定し、数値に対応した画像ファイルに変更する
        /// </summary>
        /// <param name="vm"></param>
        private void UpdateGUI_TotalPayment(VendingMachine vm)
        {
            int targetNum = m_vm.payment;
            int setNum, nextTargetNum;
            int targetDigit = 10000;

            // 上の桁からチェック
            for (int i = payImages.Count; i > 0; i--)
            {
                // 目的桁の数値を取得
                setNum = (int)(targetNum / targetDigit);
                nextTargetNum = (int)(targetNum % targetDigit);

                // 画像更新
                UpdateGUI_SetNumber(payImages[i - 1], setNum); 

                // 桁下げて次のループへ
                targetNum = nextTargetNum;
                targetDigit = (int)(targetDigit / 10); 
            }
        }

        /// <summary>
        /// 数値画像の更新
        /// Imageコントール（target）に表示する 数値をnumに変更する
        /// </summary>
        /// <param name="target">変更する桁のImageコントロール</param>
        /// <param name="num">変更後の数値</param>
        private void UpdateGUI_SetNumber(Image target, int num)
        {
            string sourcePath = string.Format("img\\seg{0}_image.png", num);

            target.Source = new BitmapImage(new Uri(sourcePath, UriKind.Relative));
        }
        #endregion // UpdateGUI

        #region "Events"
        private void PaymentCoin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int payment = 0;
            SelectPaymentCoinWindow wnd = new SelectPaymentCoinWindow();
            wnd.Owner = Window.GetWindow(this);
            wnd.ShowDialog(payment);

            SendMessage(string.Format("01,{0}", wnd.Payment));

            //m_vm.payment = wnd.Payment;
            while (shouldRead)
            {
                Task.Delay(200);
            }
            UpdateGUI();
        }

        private void Drink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SendMessage("01,100");
        }

        void event_DataReceived(object sender, DataReceivedEventArgs e)
        {
            // 受信データチェックとm_vmの更新
            if (UpdateRecieveData(e.Data) == false) { return; }

            Logging(e.Data, "RecieveMessage");
            UpdateGUI();
        }
        private void SendMessage(string msg)
        {
            //msg = "01,100";
            shouldRead = true;
            Logging(msg, "Send");

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(communicationFileDir + "\\GUI_File.txt"))
            {
                sw.WriteLine(msg);
            }

            try
            {
                m_vm.process.StandardInput.WriteLine(msg);
                Task.Delay(700);
            }
            catch (Exception e)
            {
                ErrorMessageBox(e.Message);
                //throw e;
            }
        }
        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Dispatcher.BeginInvoke(MyEvent, new object[2] { sender, e });
            Logging(e.Data, "get");
        }

        private void Scheme_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Dispatcher.BeginInvoke(MyEvent, new object[2] { sender, e });
            Logging(e.Data, "get");
        }


        private void FileChanged(object sender, System.IO.FileSystemEventArgs e)
        {
            if (shouldRead == false) { return; }

            string line;

            try
            {
                System.IO.File.Copy(stdOutDataFilePath, communicationFileDir + "\\temp.txt", true);
                using (System.IO.StreamReader sr = new System.IO.StreamReader(communicationFileDir + "\\temp.txt"))
                {
                    line = sr.ReadLine();
                    UpdateRecieveData(line);
                    Logging(line, "Read");
                    shouldRead = false;
                }
                System.IO.File.Delete(communicationFileDir + "\\temp.txt");

            }
            catch (Exception ex)
            {
                ErrorMessageBox(ex.Message);
            }
        }
        #endregion // Events


        private void ErrorMessageBox(string msg, string caption = "自動販売機エラー") 
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(logFilePath, true))
            {
                sw.WriteLine(string.Format("{0} [ERROR]: {1} - {2}", DateTime.Now.ToString(), caption, msg));
            }
            MessageBox.Show(msg, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Logging(string msg, string caption, bool append = true)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(logFilePath, append))
            {
                sw.WriteLine(string.Format("{0} [LOG]  : {1} - {2}", DateTime.Now.ToString(), caption, msg));
            }
        }

        private bool UpdateRecieveData(string srcData)
        {
            if (srcData == null)
            {
                ErrorMessageBox("受信データが空でした。");
                return false;
            }

            string[] receiveData = srcData.Split(",".ToCharArray());

            if (receiveData.Length != 6)
            {
                ErrorMessageBox(string.Format("受信データ列数が異常です。列数:{0}\r\n", receiveData.Length), "自動販売機データ受信エラー");
                return false;
            }

            string errMsg = "受信データが異常です。\r\n";
            int upperDrink, lowerDrink, viewDrink, viewChange;
            if (int.TryParse(receiveData[0], out upperDrink) == false)
            {
                ErrorMessageBox(errMsg + string.Format("　上段金額:{0}\r\n", receiveData[0]));
                return false;
            }

            if (int.TryParse(receiveData[1], out lowerDrink) == false)
            {
                ErrorMessageBox(errMsg + string.Format("　下段金額:{0}\r\n", receiveData[1]));
                return false;
            }

            if (int.TryParse(receiveData[2], out m_vm.payment) == false)
            {
                ErrorMessageBox(errMsg + string.Format("　投入金額:{0}\r\n", receiveData[2]));
                return false;
            }

            if (int.TryParse(receiveData[3], out viewDrink) == false)
            {
                ErrorMessageBox(errMsg + string.Format("　飲み物表示:{0}\r\n", receiveData[3]));
                return false;
            }

            if (int.TryParse(receiveData[4], out viewChange) == false)
            {
                ErrorMessageBox(errMsg + string.Format("　おつり表示:{0}\r\n", receiveData[4]));
                return false;
            }

            if (int.TryParse(receiveData[5], out m_vm.change) == false)
            {
                ErrorMessageBox(errMsg + string.Format("　おつり金額:{0}\r\n", receiveData[5]));
                return false;
            }


            int[] checkValue = { 1000, 100, 10, 1 };
            m_vm.isActiveButtons = new bool[] { false, false, false, false, false, false, false, false };
            for (int i = 0; i < checkValue.Length; i++)
            {
                if (upperDrink >= checkValue[i])
                {
                    upperDrink -= checkValue[i];
                    m_vm.isActiveButtons[i] = true;
                }

                if (lowerDrink >= checkValue[i])
                {
                    lowerDrink -= checkValue[i];
                    m_vm.isActiveButtons[4 + i] = true;
                }
            }

            m_vm.hasItem = (viewDrink == 1);    // viewDrink =1: True, viewDrink =0: False
            m_vm.hasChange = (viewChange == 1); // viewChange=1: True, viewChange=0: False

            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Logging("Exit VendingSystem", "Closing");
                if (m_vm.process != null)
                {
                    m_vm.process.Kill();
                    m_vm.process.Close();
                    m_vm.process.Dispose();
                }
            }
            catch (InvalidOperationException exc)
            {
                ErrorMessageBox(exc.Message, "Window_Closing");
            }
        }
    }
}
