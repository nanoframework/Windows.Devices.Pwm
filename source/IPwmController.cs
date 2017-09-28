//
// Copyright (c) 2017 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

namespace Windows.Devices.Pwm
{
    internal interface IPwmController
    {
        double SetDesiredFrequency(double desiredFrequency);
        PwmPin OpenPin(int pinNumber);

        double ActualFrequency { get; }
        double MaxFrequency { get; }
        double MinFrequency { get; }
        int PinCount { get; }
    }
}
