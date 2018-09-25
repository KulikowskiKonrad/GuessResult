using Autofac;
using GuessResult.Helpers;
using GuessResult.Repositories.Interfaces;
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
        private IFootballDataApiService _footballDataApiService;

        public FootballDataJob()
        {
            _footballDataApiService = AutofacConfig.Container.Resolve<IFootballDataApiService>();
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                _footballDataApiService.ImportEvents();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error(ex);
            }
        }
    }
}