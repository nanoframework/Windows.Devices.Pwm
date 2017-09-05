//
// Copyright (c) 2017 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using System;

namespace Windows.Devices.Pwm
{
    public sealed class PwmPin : IPwmPin, IDisposable
    {
        /// <summary>
        /// Gets the PWM controller in use by this pin.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        public PwmController Controller { get; }

        /// <summary>
        /// Gets the started state of the pin.
        /// </summary>
        /// <value>
        /// True if the PWM has started on this pin, otherwise false.
        /// </value>
        public bool IsStarted { get; }

        /// <summary>
        /// Gets or sets the polarity of the pin.
        /// </summary>
        /// <value>
        /// The pin polarity.
        /// </value>
        public PwmPulsePolarity Polarity { get; set; }

        /// <summary>
        /// Closes current connection to the pin, and makes pin available to be opened by others.
        /// </summary>
        public void Close()
        {
            // This member is not implemented in C#
            throw new NotImplementedException();
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves the duty cycle percentage for this pin.
        /// </summary>
        /// <returns>
        /// The duty cycle percentage, between 0.0 and 1.0.
        /// </returns>
        public double GetActiveDutyCyclePercentage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the duty cycle percentage for this pin.
        /// </summary>
        /// <param name="dutyCyclePercentage">
        /// The desired duty cycle percentage, represented as a value between 0.0 and 1.0.
        /// </param>
        public void SetActiveDutyCyclePercentage(Double dutyCyclePercentage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Starts the PWM on this pin.
        /// </summary>
        public void Start()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stops the PWM on this pin.
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
