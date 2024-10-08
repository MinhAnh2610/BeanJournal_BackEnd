using Microsoft.AspNetCore.Mvc.Filters;

namespace BeanJournal_BackEnd.Filters.ResultFilters
{
	/// <summary>
	/// Result Filter for Tag List
	/// </summary>
	public class TagListResultFilter : IAsyncResultFilter
	{
		private readonly ILogger<TagListResultFilter> _logger;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="logger"></param>
		public TagListResultFilter(ILogger<TagListResultFilter> logger)
		{
			_logger = logger;
		}

		/// <summary>
		/// Result Execution Async
		/// </summary>
		/// <param name="context"></param>
		/// <param name="next"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			// TO DO: Before logic
			_logger.LogInformation("{FilterName}.{MethodName} - before", nameof(TagListResultFilter), nameof(OnResultExecutionAsync));

			await next(); // calls the subsequent filter [or] IActionResult

			// TO DO: After logic
			_logger.LogInformation("{FilterName}.{MethodName} - after", nameof(TagListResultFilter), nameof(OnResultExecutionAsync));

			context.HttpContext.Response.Headers["Last-Modified"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
		}
	}
}
