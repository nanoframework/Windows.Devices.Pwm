//
// Copyright (c) 2017 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Windows.Devices.Pwm
{
    public sealed class PwmPin : IPwmPin, IDisposable
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeInit();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeSetActiveDutyCyclePercentage(uint dutyCyclePercentage);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeSetPolarity(byte polarity);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeStart();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeStop();

        private object _syncLock = new object();
        private readonly PwmController _pwmController;
        private PwmPulsePolarity _polarity;
        private double _dutyCyclePercentage;
        private uint _dutyCycle;
        private int _pinNumber;
        private int _pwmTimer;
        private bool _isStarted;

        internal PwmPin (PwmController controller, int pwmTimer, int pinNumber)
        {
            _pwmController = controller;
            _pwmTimer = pwmTimer;
            _pinNumber = pinNumber;
            _polarity = PwmPulsePolarity.ActiveHigh;

            NativeInit();
        }

        /// <summary>
        /// Gets the PWM controller in use by this pin.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        public PwmController Controller
        {
            get
            {
                return _pwmController;
            }
        }

        /// <summary>
        /// Gets the started state of the pin.
        /// </summary>
        /// <value>
        /// True if the PWM has started on this pin, otherwise false.
        /// </value>
        public bool IsStarted
        {
            get
            {
                return _isStarted;
            }
        }

        /// <summary>
        /// Gets or sets the polarity of the pin.
        /// </summary>
        /// <value>
        /// The pin polarity.
        /// </value>
        public PwmPulsePolarity Polarity
        {
            get
            {
                return _polarity;
            }

            set
            {
                _polarity = value;
                NativeSetPolarity((byte)value);
            }
        }

        /// <summary>
        /// Retrieves the duty cycle percentage for this pin.
        /// </summary>
        /// <returns>
        /// The duty cycle percentage, between 0.0 and 1.0.
        /// </returns>
        public double GetActiveDutyCyclePercentage()
        {
            return _dutyCyclePercentage;
        }

        /// <summary>
        /// Sets the duty cycle percentage for this pin.
        /// </summary>
        /// <param name="dutyCyclePercentage">
        /// The desired duty cycle percentage, represented as a value between 0.0 and 1.0.
        /// </param>
        public void SetActiveDutyCyclePercentage(double dutyCyclePercentage)
        {
            _dutyCyclePercentage = dutyCyclePercentage;
            _dutyCycle = (uint)(dutyCyclePercentage * 10000);
            NativeSetActiveDutyCyclePercentage(_dutyCycle);
        }

        /// <summary>
        /// Starts the PWM on this pin.
        /// </summary>
        public void Start()
        {
            NativeStart();
            _isStarted = true;
        }

        /// <summary>
        /// Stops the PWM on this pin.
        /// </summary>
        public void Stop()
        {
            NativeStop();
            _isStarted = false;
        }

        #region IDisposable Support

        private bool _disposedValue;

        private void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }

                DisposeNative();

                _disposedValue = true;
            }
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void DisposeNative();

#pragma warning disable 1591
        ~PwmPin()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            lock (_syncLock)
            {
                if (!_disposedValue)
                {
                    Dispose(true);

                    GC.SuppressFinalize(this);
                }
            }
        }

        #endregion
    }
}
