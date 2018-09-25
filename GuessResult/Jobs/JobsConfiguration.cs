using GuessResult.Helpers;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GuessResult.Jobs
{
    public class JobsConfiguration
    {
        public static void Configure()
        {
            try
            {
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();

                IJobDetail job = null;
                ITrigger trigger = null;

                job = JobBuilder.Create<FootballDataJob>().Build();

                trigger = TriggerBuilder.Create()
                    .StartNow()
                    .WithSimpleSchedule(ssb => ssb.WithIntervalInMinutes(1)
                        .RepeatForever()
                        .WithMisfireHandlingInstructionIgnoreMisfires()
                    )
                    .Build();

                scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
            }
        }
    }
}