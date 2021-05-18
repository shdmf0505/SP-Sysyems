using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Micube.SmartMES.QualityAnalysis.KeyboardClipboard
{
    /// <summary>
    ///     클립보드 저장하는 기능을 관리하는 함수
    /// </summary>
    public class ClipboardManager
    {
        public delegate void ClipboardOnChangedDelegate(ClipboardFormat format, object data);

        public delegate void OnErrorMessageEventHandler(Exception ex, string message);

        private static readonly string[] formats = Enum.GetNames(typeof(ClipboardFormat));

        private readonly bool isRunning = true;

        public ClipboardManager()
        {
            KeyDownCheckThread = new Thread(CheckKeyboard);
            KeyDownCheckThread.SetApartmentState(ApartmentState.STA);
            KeyDownCheckThread.Start();
        }

        private Thread KeyDownCheckThread { get; }

        public event ClipboardOnChangedDelegate OnChanged;

        public event OnErrorMessageEventHandler OnError;

        /// <summary>
        /// 키입력을 체크하는 함수
        /// </summary>
        private void CheckKeyboard()
        {
            try
            {
                while (isRunning)
                {
                    Thread.Sleep(40); //minimum CPU usage
                    if ((Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) > 0 &&
                        (Keyboard.GetKeyStates(Key.V) & KeyStates.Down) > 0 ||
                        (Keyboard.GetKeyStates(Key.RightCtrl) & KeyStates.Down) > 0 &&
                        (Keyboard.GetKeyStates(Key.V) & KeyStates.Down) > 0)
                    {
                        var iData = Clipboard.GetDataObject();

                        ClipboardFormat? format = null;

                        foreach (var f in formats)
                            if (iData.GetDataPresent(f))
                            {
                                format = (ClipboardFormat)Enum.Parse(typeof(ClipboardFormat), f);
                                break;
                            }

                        var data = iData.GetData(format.ToString());

                        if (data == null || format == null)
                            return;

                        OnChanged?.Invoke((ClipboardFormat)format, data);
                    }
                }
            }
            catch (ThreadAbortException tae)
            {
            }
            catch (Exception ex)
            {
                RaiseOnErrorMessage(ex);
            }
        }

        /// <summary>
        /// 에러 발생 이벤트
        /// </summary>
        /// <param name="error"></param>
        private void RaiseOnErrorMessage(Exception error)
        {
            OnError?.Invoke(error, error.ToString());
        }
    }
}
