//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Windows.Devices.Pwm
{
    /// <summary>
    /// Represents a single PWM pin on the system.
    /// </summary>
    public sealed class PwmPin : IPwmPin, IDisposable
    {
        // this is used as the lock object 
        // a lock is required because multiple threads can access the device
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly object _syncLock;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly PwmController _pwmController;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private PwmPulsePolarity _polarity;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private double _dutyCyclePercentage;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private uint _dutyCycle;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int _pinNumber;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private int _pwmTimer;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool _isStarted;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool _disposed;

        internal PwmPin (PwmController controller, int pwmTimer, int pinNumber)
        {
            _pwmController = controller;
            _pwmTimer = pwmTimer;
            _pinNumber = pinNumber;
            _polarity = PwmPulsePolarity.ActiveHigh;
            _syncLock = new object();

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

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }

                DisposeNative();

                _disposed = true;
            }
        }

        #pragma warning disable 1591
        ~PwmPin()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            lock (_syncLock)
            {
                if (!_disposed)
                {
                    Dispose(true);

                    GC.SuppressFinalize(this);
                }
            }
        }
        #pragma warning restore 1591

        #endregion

        #region Native Calls

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

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void DisposeNative();

        #endregion
    }
}
