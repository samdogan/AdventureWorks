using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Organization.Jobs
{
    public abstract class BaseJob : IJob
    {
        public BaseJob()
        {

        }

        public abstract Task OnExecute(IJobExecutionContext context);

		public async Task Execute(IJobExecutionContext context)
		{

			

		}
	}
}
