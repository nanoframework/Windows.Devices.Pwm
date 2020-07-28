//
// Copyright (c) 2017 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Runtime.CompilerServices;

namespace Windows.Devices.Pwm
{
    /// <summary>
    /// Represents a PWM controller connected to the system.
    /// </summary>
    public sealed class PwmController : IPwmController
    {
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly int _controllerId;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private double _actualFrequency;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string _pwmTimer;

        internal PwmController(string controller)
        {
            // the Pwm id is an ASCII string with the format 'PWMn'
            // need to grab 'n' from the string and convert that to the integer value from the ASCII code (do this by subtracting 48 from the char value)
            _controllerId = controller[3] - '0';

            // check if this controller is already opened
            var myController = FindController(_controllerId);
            if (myController == null)
            {
                _actualFrequency = 0.0;
                _pwmTimer = controller;

                // add controller to collection, with the ID as key (just the index number)
                PwmControllerManager.ControllersCollection.Add(this);
            }
            else
            {
                // this controller already exists: throw an exception
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Gets the actual frequency of the PWM.
        /// </summary>
        /// <value>
        /// The frequency in Hz.
        /// </value>
        public double ActualFrequency
        {
            get
            {
                return _actualFrequency;
            }
        }

        /// <summary>
        /// Gets the maximum frequency offered by the controller.
        /// </summary>
        /// <value>
        /// The maximum frequency in Hz.
        /// </value>
        public extern double MaxFrequency
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }

        /// <summary>
        /// Gets the minimum frequency offered by the controller.
        /// </summary>
        /// <value>
        /// The minimum frequency in Hz.
        /// </value>
        public extern double MinFrequency
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }

        /// <summary>
        /// Gets the number of pins available on the system.
        /// </summary>
        /// <value>
        /// The number of pins.
        /// </value>
        public extern int PinCount
        {
            [MethodImpl(MethodImplOptions.InternalCall)]
            get;
        }

        /// <summary>
        /// Initializes a PWM controller instance based on the given DeviceInformation ID.
        /// </summary>
        /// <param name="deviceId">
        /// The acquired DeviceInformation ID.
        /// </param>
        /// <returns>
        /// PwmController
        /// </returns>
        public static PwmController FromId(String deviceId)
        {
            return new PwmController(deviceId);
        }

        /// <summary>
        /// Gets the default PWM controller on the system.
        /// </summary>
        /// <returns>
        /// The default PWM controller on the system, or null if the system has no PWM controller.
        /// </returns>
        public static PwmController GetDefault()
        {
            string controllersAqs = GetDeviceSelector();
            string[] controllers = controllersAqs.Split(',');

            if (controllers.Length > 0)
            {
                // the Pwm id is an ASCII string with the format 'PWMn'
                // need to grab 'n' from the string and convert that to the integer value from the ASCII code (do this by subtracting 48 from the char value)
                var controllerId = controllers[0][3] - '0';

                var controller = FindController(controllerId);
                if (controller != null)
                {
                    // controller is already open
                    return controller;
                }
                else
                {
                    // this controller is not in the collection, create it
                    return new PwmController(controllers[0]);
                }
            }

            // the system has no PWM controller 
            return null;
        }

        /// <summary>
        /// Opens the pin for use.
        /// </summary>
        /// <param name="pinNumber">
        /// Which pin to open.
        /// </param>
        /// <returns>
        /// The requested pin now available for use.
        /// </returns>
        public PwmPin OpenPin(Int32 pinNumber)
        {
            return new PwmPin(this, _controllerId, pinNumber);
        }

        /// <summary>
        /// Sets the PWM frequency.
        /// </summary>
        /// <param name="desiredFrequency">
        /// Then value of the desired frequency in Hz.
        /// </param>
        /// <returns>
        /// The actual frequency that was set. This will be the closest supported match as determined by the provider.
        /// </returns>
        public double SetDesiredFrequency(Double desiredFrequency)
        {
            _actualFrequency = NativeSetDesiredFrequency((uint)desiredFrequency);
            
            return _actualFrequency;
        }

        private static PwmController FindController(int index)
        {
            for (int i = 0; i < PwmControllerManager.ControllersCollection.Count; i++)
            {
                if (((PwmController)PwmControllerManager.ControllersCollection[i])._controllerId == index)
                {
                    return (PwmController)PwmControllerManager.ControllersCollection[i];
                }
            }

            return null;
        }

        #region Native Calls

        /// <summary>
        /// Retrieves an Advanced Query Syntax (AQS) string for all the PWM controllers on the system. You can use this string with the DeviceInformation.FindAllAsync method to get DeviceInformation objects for those controllers.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string GetDeviceSelector();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern uint NativeSetDesiredFrequency(uint desiredFrequency);

        #endregion
    }
}

