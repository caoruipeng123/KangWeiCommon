﻿using KangWeiCommon.DesignPattern;
using System;

namespace KangWeiCommon
{
    /// <summary>
    /// 雪花算法 自增Id生成器
    /// </summary>
    public class IdWorker
    {
        /// <summary>
        /// 基准时间
        /// </summary>
        public const long Twepoch = 1288834974657L;
        /// <summary>
        /// 机器标识位数 机器id 低5位
        /// </summary>
        const int WorkerIdBits = 5;
        /// <summary>
        /// 数据标志位数 数据中心id 高5位
        /// </summary>
        const int DatacenterIdBits = 5;
        /// <summary>
        /// 序列号识位数
        /// </summary>
        const int SequenceBits = 12;
        /// <summary>
        /// 机器ID最大值
        /// </summary>
        const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);
        /// <summary>
        /// 数据标志ID最大值
        /// </summary>
        const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);
        /// <summary>
        /// 序列号ID最大值
        /// </summary>
        private const long SequenceMask = -1L ^ (-1L << SequenceBits);
        /// <summary>
        /// 机器ID偏左移12位
        /// </summary>
        private const int WorkerIdShift = 12;// SequenceBits;
        /// <summary>
        /// 数据ID偏左移17位
        /// </summary>
        private const int DatacenterIdShift = 17;// SequenceBits + WorkerIdBits;
        /// <summary>
        /// 时间毫秒左移22位
        /// </summary>
        public const int TimestampLeftShift = 22;// SequenceBits + WorkerIdBits + DatacenterIdBits;
        /// <summary>
        /// 序列号
        /// </summary>
        private long _sequence = 0L;
        /// <summary>
        /// 上一次生成Id的时间戳
        /// </summary>
        private long _lastTimestamp = -1L;
        /// <summary>
        /// 
        /// </summary>
        public long WorkerId { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public long DatacenterId { get; protected set; }
        /// <summary>
        /// 
        /// </summary>
        public long Sequence
        {
            get { return _sequence; }
            internal set { _sequence = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workerId">机器id</param>
        /// <param name="datacenterId">数据中心id</param>
        /// <param name="sequence"></param>
        public IdWorker(long workerId, long datacenterId, long sequence = 0L)
        {
            // 如果超出范围就抛出异常
            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new ArgumentException(string.Format("worker Id 必须大于0，且不能大于MaxWorkerId： {0}", MaxWorkerId));
            }

            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new ArgumentException(string.Format("region Id 必须大于0，且不能大于MaxWorkerId： {0}", MaxDatacenterId));
            }

            //先检验再赋值
            WorkerId = workerId;
            DatacenterId = datacenterId;
            _sequence = sequence;
        }
        /// <summary>
        /// 全局锁
        /// </summary>
        readonly object _lock = new object();
        /// <summary>
        /// 生成全局唯一Id
        /// </summary>
        /// <returns></returns>
        public long NextId()
        {
            lock (_lock)
            {
                var timestamp = TimeGen();
                if (timestamp < _lastTimestamp)
                {
                    throw new Exception(string.Format("时间戳必须大于上一次生成ID的时间戳.  拒绝为{0}毫秒生成id", _lastTimestamp - timestamp));
                }

                //如果上次生成时间和当前时间相同,在同一毫秒内
                if (_lastTimestamp == timestamp)
                {
                    //sequence自增，和sequenceMask相与一下，去掉高位
                    _sequence = (_sequence + 1) & SequenceMask;
                    //判断是否溢出,也就是每毫秒内超过1024，当为1024时，与sequenceMask相与，sequence就等于0
                    if (_sequence == 0)
                    {
                        //等待到下一毫秒
                        timestamp = TilNextMillis(_lastTimestamp);
                    }
                }
                else
                {
                    //如果和上次生成时间不同,重置sequence，就是下一毫秒开始，sequence计数重新从0开始累加,
                    //为了保证尾数随机性更大一些,最后一位可以设置一个随机数
                    _sequence = 0;
                }

                _lastTimestamp = timestamp;
                return ((timestamp - Twepoch) << TimestampLeftShift) | (DatacenterId << DatacenterIdShift) | (WorkerId << WorkerIdShift) | _sequence;
            }
        }

        /// <summary>
        /// 防止产生的时间比之前的时间还要小（由于NTP回拨等问题）,保持增量的趋势.
        /// </summary>
        /// <param name="lastTimestamp"></param>
        /// <returns></returns>
        private long TilNextMillis(long lastTimestamp)
        {
            var timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        /// <summary>
        /// 获取当前的时间戳
        /// </summary>
        /// <returns></returns>
        private long TimeGen()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        /// <summary>
        /// 1970.1.1 utc时间
        /// </summary>
        static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}
