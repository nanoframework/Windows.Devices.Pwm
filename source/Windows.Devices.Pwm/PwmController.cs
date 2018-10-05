﻿//
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
        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern uint  NativeSetDesiredFrequency (uint desiredFrequency);

        private readonly int _deviceId;
        private double _actualFrequency;
        private string _pwmTimer;

        internal PwmController(string pwmController)
        {
            var deviceId = (Convert.ToInt32(pwmController.Substring(3)));    // Remove the "TIM" part of the string "TIMxx" to get the "xx" value
            _deviceId = deviceId;
            _actualFrequency = 0.0;
            _pwmTimer = pwmController;
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
            return null;
        }

        /// <summary>
        /// Retrieves an Advanced Query Syntax (AQS) string for all the PWM controllers on the system. You can use this string with the DeviceInformation.FindAllAsync method to get DeviceInformation objects for those controllers.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string GetDeviceSelector();

        /// <summary>
        /// Retrieves an Advanced Query Syntax (AQS) string for the PWM controller that has the specified friendly name. You can use this string with the DeviceInformation.FindAllAsync method to get DeviceInformation objects for those controllers.
        /// </summary>
        /// <param name="friendlyName">
        /// A friendly name for the particular PWM controller for which you want to get the corresponding AQS string.
        /// </param>
        /// <returns></returns>
        public static string GetDeviceSelector(String friendlyName)
        {
            // At the moment, ignore the friendly name.
            return GetDeviceSelector();
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
            return new PwmPin(this, _deviceId, pinNumber);
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
    }
}
