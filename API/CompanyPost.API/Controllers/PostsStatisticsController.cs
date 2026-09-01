namespace CompanyPost.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsStatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PostsStatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-statistics")]
        public async Task<IActionResult> GetStatistics(CancellationToken cancellationToken)
        {
            var statisticsResult = await _mediator.Send(new GetPostStatisticsQuery { } , cancellationToken);
            return Ok(statisticsResult);
        }
    }
}