using Microsoft.AspNetCore.Mvc.Filters;

namespace BeanJournal_BackEnd.Filters
{
	/// <summary>
	/// Action Filter
	/// </summary>
	public class MyLogging : Attribute, IAsyncActionFilter
	{
		private readonly ILogger<MyLogging> _logger;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="logger"></param>
		public MyLogging(ILogger<MyLogging> logger)
		{
			_logger = logger;
		}
		/// <summary>
		/// Action Execution
		/// </summary>
		/// <param name="context"></param>
		/// <param name="next"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			_logger.LogInformation($"Action filter {nameof(MyLogging)} - before");

			await next();

			_logger.LogInformation($"Action filter {nameof(MyLogging)} - after");
		}
	}
}
