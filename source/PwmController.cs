//
// Copyright (c) 2017 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;
using System.Collections.Generic;
using Windows.Devices.Pwm.Provider;

namespace Windows.Devices.Pwm
{
    /// <summary>
    /// Represents a PWM controller connected to the system.
    /// </summary>
    public sealed class PwmController : IPwmController
    {
        /// <summary>
        /// Gets the actual frequency of the PWM.
        /// </summary>
        /// <value>
        /// The frequency in Hz.
        /// </value>
        public double ActualFrequency { get; }

        /// <summary>
        /// Gets the maximum frequency offered by the controller.
        /// </summary>
        /// <value>
        /// The maximum frequency in Hz.
        /// </value>
        public double MaxFrequency { get; }

        /// <summary>
        /// Gets the minimum frequency offered by the controller.
        /// </summary>
        /// <value>
        /// The minimum frequency in Hz.
        /// </value>
        public double MinFrequency { get; }

        /// <summary>
        /// Gets the number of pins available on the system.
        /// </summary>
        /// <value>
        /// The number of pins.
        /// </value>
        public int PinCount { get; }

        /// <summary>
        /// Initializes a PWM controller instance based on the given DeviceInformation ID.
        /// </summary>
        /// <param name="deviceId">
        /// The acquired DeviceInformation ID.
        /// </param>
        /// <returns>
        /// IAsyncOperation<PwmController> 
        /// </returns>
        public static PwmController FromId(String deviceId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all the controllers on the system asynchronously.
        /// </summary>
        /// <param name="provider">
        /// The PWM provider that is on the system.
        /// </param>
        /// <returns>
        /// When the method completes successfully, it returns a list of values that represent the controllers available on the system.
        /// </returns>
        public static IReadOnlyList<PwmController> GetControllers(IPwmProvider provider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the default PWM controller on the system.
        /// </summary>
        /// <returns>
        /// The default PWM controller on the system, or null if the system has no PWM controller.
        /// </returns>
        public static PwmController GetDefault()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves an Advanced Query Syntax (AQS) string for all the PWM controllers on the system. You can use this string with the DeviceInformation.FindAllAsync method to get DeviceInformation objects for those controllers.
        /// </summary>
        /// <returns></returns>
        public static string GetDeviceSelector()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves an Advanced Query Syntax (AQS) string for the PWM controller that has the specified friendly name. You can use this string with the DeviceInformation.FindAllAsync method to get DeviceInformation objects for those controllers.
        /// </summary>
        /// <param name="friendlyName">
        /// A friendly name for the particular PWM controller for which you want to get the corresponding AQS string.
        /// </param>
        /// <returns></returns>
        public static string GetDeviceSelector(String friendlyName)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

    }
}
