//
// Copyright (c) 2017 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

namespace Windows.Devices.Pwm
{
    internal interface IPwmPin
    {
        double GetActiveDutyCyclePercentage();
        void SetActiveDutyCyclePercentage(double dutyCyclePercentage);
        void Start();
        void Stop();

        PwmController Controller { get; }
        bool IsStarted { get; }
        PwmPulsePolarity Polarity { get; set; }
    }
}
