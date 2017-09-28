//
// Copyright (c) 2017 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

namespace temp.Windows.Devices.Pwm
{
    /// <summary>
    /// Describes which polarity the PWM signal should start in.
    /// </summary>
    public enum PwmPulsePolarity
    {
        /// <summary>
        /// Configures the PWM signal to start in the active high state.
        /// </summary>
        ActiveHigh,
        /// <summary>
        /// Configures the PWM signal to start in the active low state.
        /// </summary>
        ActiveLow
    }
}
