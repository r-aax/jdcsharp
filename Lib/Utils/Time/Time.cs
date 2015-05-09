// Author: Alexey Rybakov

using System;

using Lib.Physics.MeasUnits;
using Lib.Maths.Numbers;

namespace Lib.Utils.Time
{
    /// <summary>
    /// Time.
    /// </summary>
    public class UTC
    {
        /// <summary>
        /// Hours.
        /// </summary>
        public int Hours = 0;

        /// <summary>
        /// Minutes.
        /// </summary>
        public int Minutes = 0;

        /// <summary>
        /// Seconds.
        /// </summary>
        public int Seconds = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="hours">hours</param>
        /// <param name="minutes">minutes</param>
        /// <param name="seconds">seconds</param>
        public UTC(int hours, int minutes, int seconds)
        {
            if ((hours < 0)
                || (hours >= (int)Constants.DayHours)
                || (minutes < 0)
                || (minutes >= (int)Constants.HourMinutes)
                || (seconds < 0)
                || (seconds >= (int)Constants.MinuteSeconds))
            {
                throw new Exception();
            }

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0}:{1}:{2}", Hours, Minutes, Seconds);
        }

        /// <summary>
        /// Random time.
        /// </summary>
        /// <returns>random time</returns>
        public static UTC Random()
        {
            return new UTC(Randoms.RandomInInterval(0, (int)Constants.DayHours - 1),
                           Randoms.RandomInInterval(0, (int)Constants.HourMinutes - 1),
                           Randoms.RandomInInterval(0, (int)Constants.MinuteSeconds - 1));
        }
    }
}
