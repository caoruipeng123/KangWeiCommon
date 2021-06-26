using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KangWeiCommon.Entity
{
    /// <summary>
    /// 表示任意一天中的时间点（小时、分、秒）
    /// </summary>
    public class TimeOfDay
    {
        /// <summary>
        /// 根据给定的小时、分、秒创建TimeOfDay实例对象
        /// </summary>
        /// <param name="hour">小时, 在 0 到 23之间</param>
        /// <param name="minute">分, 在 0 and 59之间</param>
        /// <param name="second">秒, 在 0 and 59之间</param>
        public TimeOfDay(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
            Validate();
        }
        /// <summary>
        /// 根据给定的小时、分（秒：默认0）创建TimeOfDay实例对象
        /// </summary>
        /// <param name="hour">小时, 在 0 到 23之间</param>
        /// <param name="minute">分, 在 0 and 59之间</param>
        public TimeOfDay(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
            Second = 0;
            Validate();
        }
        /// <summary>
        /// 校验Hour、Minute、Second是否合法
        /// </summary>
        private void Validate()
        {
            if (Hour < 0 || Hour > 23)
            {
                throw new ArgumentException("Hour must be from 0 to 23");
            }

            if (Minute < 0 || Minute > 59)
            {
                throw new ArgumentException("Minute must be from 0 to 59");
            }

            if (Second < 0 || Second > 59)
            {
                throw new ArgumentException("Second must be from 0 to 59");
            }
        }
        /// <summary>
        /// 根据给定的小时、分、秒创建TimeOfDay实例对象
        /// </summary>
        /// <param name="hour">小时, 在 0 到 23之间</param>
        /// <param name="minute">分, 在 0 and 59之间</param>
        /// <param name="second">秒, 在 0 and 59之间</param>
        /// <returns></returns>
        public static TimeOfDay HourMinuteAndSecondOfDay(int hour, int minute, int second)
        {
            return new TimeOfDay(hour, minute, second);
        }
        /// <summary>
        /// 根据给定的小时、分（秒：默认0）创建TimeOfDay实例对象
        /// </summary>
        /// <param name="hour">小时, 在 0 到 23之间</param>
        /// <param name="minute">分, 在 0 and 59之间</param>
        /// <returns></returns>
        public static TimeOfDay HourAndMinuteOfDay(int hour, int minute)
        {
            return new TimeOfDay(hour, minute);
        }
        /// <summary>
        /// 返回Day中的Hour (在 0 and 23之间)
        /// </summary>
        public int Hour { get; }
        /// <summary>
        /// 返回Day中的分 (在 0 and 59之间)
        /// </summary>
        public int Minute { get; }
        /// <summary>
        ///返回Day中的秒 (在 0 and 59之间)
        /// </summary>
        public int Second { get; }
        /// <summary>
        /// 判断当前实例时间是否在给定的时间timeOfDay之前。在返回True，不在返回False
        /// </summary>
        /// <param name="timeOfDay">要比较的时间</param>
        /// <returns></returns>
        public bool Before(TimeOfDay timeOfDay)
        {
            if (timeOfDay.Hour > Hour)
            {
                return true;
            }
            if (timeOfDay.Hour < Hour)
            {
                return false;
            }

            if (timeOfDay.Minute > Minute)
            {
                return true;
            }
            if (timeOfDay.Minute < Minute)
            {
                return false;
            }

            if (timeOfDay.Second > Second)
            {
                return true;
            }
            if (timeOfDay.Second < Second)
            {
                return false;
            }

            return false;
        }
        /// <summary>
        /// 判断两个时间是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is TimeOfDay))
            {
                return false;
            }

            TimeOfDay other = (TimeOfDay)obj;

            return other.Hour == Hour && other.Minute == Minute && other.Second == Second;
        }
        /// <summary>
        /// 获取哈希值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (Hour + 1) ^ (Minute + 1) ^ (Second + 1);
        }
        /// <summary>
        /// 截取给定时间的年月日和当前实例的Hour、Minute、Second拼接成一个时间对象返回
        /// </summary>
        /// <param name="dateTime">给定的时间</param>
        public DateTimeOffset GetTimeOfDayForDate(DateTimeOffset? dateTime)
        {
            if (dateTime == null)
            {
                return default;
            }

            DateTimeOffset cal = new DateTimeOffset(dateTime.Value.Date, dateTime.Value.Offset);
            TimeSpan t = new TimeSpan(0, Hour, Minute, Second);
            return cal.Add(t);
        }
        /// <summary>
        /// 返回当前时间的描述格式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "TimeOfDay[" + Hour + ":" + Minute + ":" + Second + "]";
        }
    }
}
