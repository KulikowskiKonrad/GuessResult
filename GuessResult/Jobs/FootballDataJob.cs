using GuessResult.Helpers;
using GuessResult.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessResult.Jobs
{
    [DisallowConcurrentExecution]
    public class FootballDataJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                new FootballDataApiService().ImportEvents();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
            }
        }
    }
}