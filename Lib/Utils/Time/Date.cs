// Author: Alexey Rybakov

using System;

using Lib.Maths.Numbers;

namespace Lib.Utils.Time
{
    /// <summary>
    /// Date.
    /// </summary>
    public class Date
    {
        /// <summary>
        /// Min day.
        /// </summary>
        public static readonly int MinDay = 1;

        /// <summary>
        /// Max day for each month.
        /// </summary>
        public static readonly int[] MaxDays = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        /// <summary>
        /// Min month.
        /// </summary>
        public static readonly Month MinMonth = Month.January;

        /// <summary>
        /// Max month.
        /// </summary>
        public static readonly Month MaxMonth = Month.December;

        /// <summary>
        /// Min year.
        /// </summary>
        public static readonly int MinYear = 0;

        /// <summary>
        /// Day.
        /// </summary>
        public int Day = 1;

        /// <summary>
        /// Month.
        /// </summary>
        public Month Month = Month.January;

        /// <summary>
        /// Year (0 - inf).
        /// </summary>
        public int Year = 0;

        /// <summary>
        /// Check leap year.
        /// </summary>
        /// <param name="year">year</param>
        /// <returns><c>true</c> - if leap, <c>false</c> - if not leap</returns>
        public static bool IsYearLeap(int year)
        {
            bool is_leap = false;

            if ((year % 4) == 0)
            {
                if (((year % 100) != 0) || ((year % 400) == 0))
                {
                    is_leap = false;
                }
            }

            return is_leap;
        }

        /// <summary>
        /// Days in month.
        /// </summary>
        /// <param name="month">month</param>
        /// <param name="year">year</param>
        /// <returns>days count</returns>
        public static int MonthDays(Month month, int year)
        {
            if ((month == Month.February) && IsYearLeap(year))
            {
                return 28;
            }

            return MaxDays[(int)month - 1];
        }

        /// <summary>
        /// Correcteness check.
        /// </summary>
        /// <param name="day">day</param>
        /// <param name="month">month</param>
        /// <param name="year">year</param>
        /// <returns><c>true</c> - if correct, <c>false</c> - if not correct</returns>
        public static bool IsCorrect(int day, Month month, int year)
        {
            if ((year < MinYear)
                || (month < MinMonth)
                || (month > MaxMonth)
                || (day < MinDay)
                || (day > MonthDays(month, year)))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="day">day</param>
        /// <param name="month">month</param>
        /// <param name="year">year</param>
        public Date(int day, Month month, int year)
        {
            if (!IsCorrect(day, month, year))
            {
                throw new Exception();
            }

            Day = day;
            Month = month;
            Year = year;
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0},{1},{2}", Day, Month, Year);
        }

        /// <summary>
        /// Random date.
        /// </summary>
        /// <param name="max_year">max year</param>
        /// <returns>random date</returns>
        public static Date Random(int max_year)
        {
            int year = Randoms.RandomInInterval(MinYear, max_year);
            Month month = (Month)Randoms.RandomInInterval((int)MinMonth, (int)MaxMonth);

            return new Date(Randoms.RandomInInterval(MinDay, MonthDays(month, year)), month, year);
        }
    }
}
