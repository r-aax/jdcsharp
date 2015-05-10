// Author: Alexey Rybakov

using System.Timers;
using System.Diagnostics;

namespace Lib.Utils.Time
{
    /// <summary>
    /// Timer with slow.
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// Inner timer.
        /// </summary>
        private System.Timers.Timer InnerTimer;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="step_seconds">step in seconds</param>
        /// <param name="slow_coefficient">slow factor</param>
        public Timer(double step_seconds, double slow_coefficient)
        {
            Debug.Assert(step_seconds > 0.0);
            Debug.Assert(slow_coefficient > 0.0);

            InnerTimer = new System.Timers.Timer(Lib.Physics.MeasUnits.Convert.Milli(step_seconds / slow_coefficient));
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="step_seconds">step in seconds</param>
        public Timer(double step_seconds)
            : this(step_seconds, 1.0)
        {
        }

        /// <summary>
        /// Start timer.
        /// </summary>
        public void Start()
        {
            InnerTimer.Start();
        }

        /// <summary>
        /// Stop timer.
        /// </summary>
        public void Stop()
        {
            InnerTimer.Stop();
        }

        /// <summary>
        /// Function subscriber.
        /// </summary>
        public delegate void SubscribeFun();

        /// <summary>
        /// Subscribe function.
        /// </summary>
        /// <param name="fun"></param>
        public void Subscribe(SubscribeFun fun)
        {
            InnerTimer.Elapsed += new ElapsedEventHandler((sender, args) => fun());
        }
    }
}
