using Microsoft.AspNetCore.Mvc.Filters;

namespace BeanJournal_BackEnd.Filters.ActionFilters
{
	/// <summary>
	/// Action filter for response header
	/// </summary>
	public class ResponseHeaderActionFilter : IAsyncActionFilter, IOrderedFilter
	{
		private readonly ILogger<ResponseHeaderActionFilter> _logger;
		private readonly string _key, _value;

		/// <summary>
		/// 
		/// </summary>
		public int Order { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="order"></param>
		public ResponseHeaderActionFilter(ILogger<ResponseHeaderActionFilter> logger, string key, string value, int order)
		{
			_logger = logger;
			_key = key;
			_value = value;
			Order = order;
		}

		/// <summary>
		/// Execution Async
		/// </summary>
		/// <param name="context"></param>
		/// <param name="next"></param>
		/// <returns></returns>
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			_logger.LogInformation("{FilterName}.{MethodName} method - before", nameof(ResponseHeaderActionFilter), nameof(OnActionExecutionAsync));

			await next(); // calls the subsequent filter or action method

			_logger.LogInformation("{FilterName}.{MethodName} method - after", nameof(ResponseHeaderActionFilter), nameof(OnActionExecutionAsync));

			context.HttpContext.Response.Headers[_key] = _value;
		}
	}
}
