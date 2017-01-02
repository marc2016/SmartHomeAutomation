using System;
using System.Linq;

using Windows.Devices.Gpio;

namespace Server.RemoteControl
{
    public class RemoteControl
    {
        #region Constants

        private const int RASPBERRY_DATA_PIN = 2;
        private const int REPEAT_TRANSMIT = 3;

        #endregion

        #region Fields

        private GpioPin mGpioPin;

        private static volatile RemoteControl _instance;
        private static readonly object _syncRoot = new object();

        #endregion

        #region Constructor

        private RemoteControl()
        {
            this.InitGpio();
        }

        #endregion

        #region Properties

        public static RemoteControl Instance
        {
            get
            {
                if (_instance == null)
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                            _instance = new RemoteControl();
                    }

                return _instance;
            }
        }

        #endregion

        #region Method

        public void SwitchOff(string group, int channel)
        {
            this.SendTriState(CodeWordGenerator.GetCodeWordA(group, channel, false));
        }

        public void SwitchOn(string group, int channel)
        {
            this.SendTriState(CodeWordGenerator.GetCodeWordA(group, channel, true));
        }

        #endregion

        #region Subroutines

        /// <summary>
        ///     Initialize the GPIO-Pin for the chosen port raspberryDataPin in drive mode GpioPinDriveMode.Output.
        ///     After this call the port is ready for the transmission.
        /// </summary>
        private void InitGpio()
        {
            var mGpioController = GpioController.GetDefault();
            if (mGpioController != null)
            {
                this.mGpioPin = mGpioController.OpenPin(RASPBERRY_DATA_PIN);
                this.mGpioPin.SetDriveMode(GpioPinDriveMode.Output);
            }
        }

        /// <summary>
        ///     Sends a "Sync" Bit
        /// </summary>
        private void SendSync()
        {
            this.Transmit(1, 31);
        }

        /// <summary>
        ///     Sends the whole code word.
        /// </summary>
        /// <param name="codeWord">The code word.</param>
        private void SendTriState(string codeWord)
        {
            for (var mRepeat = 0; mRepeat < REPEAT_TRANSMIT; mRepeat++)
            {
                for (var i = 0; i < codeWord.Length; i++)
                    switch (codeWord[i])
                    {
                        case '0':
                            this.SendTriState0();
                            break;
                        case 'F':
                            this.SendTriStateF();
                            break;
                        case '1':
                            this.SendTriState1();
                            break;
                    }
                this.SendSync();
            }
        }

        /// <summary>
        ///     Sends a Tri-State "0" Bit
        /// </summary>
        private void SendTriState0()
        {
            this.Transmit(1, 3);
            this.Transmit(1, 3);
        }

        /// <summary>
        ///     Sends a Tri-State "1" Bit
        /// </summary>
        private void SendTriState1()
        {
            this.Transmit(3, 1);
            this.Transmit(3, 1);
        }

        /// <summary>
        ///     Sends a Tri-State "F" Bit
        /// </summary>
        private void SendTriStateF()
        {
            this.Transmit(1, 3);
            this.Transmit(3, 1);
        }

        /// <summary>
        ///     Transmit a number of high and low pulses.
        /// </summary>
        /// <param name="highPulses">High pulses.</param>
        /// <param name="lowPulses">Low pulses.</param>
        private void Transmit(int highPulses, int lowPulses)
        {
            for (var i = 0; i < highPulses; i++)
            {
                this.mGpioPin.Write(GpioPinValue.High);
                DelayHelper.Delay(350);
            }
            for (var i = 0; i < lowPulses; i++)
            {
                this.mGpioPin.Write(GpioPinValue.Low);
                DelayHelper.Delay(350);
            }
        }

        #endregion
    }
}