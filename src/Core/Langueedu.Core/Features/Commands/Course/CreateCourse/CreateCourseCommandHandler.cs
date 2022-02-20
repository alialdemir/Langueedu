using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Langueedu.Core.Factories;
using Langueedu.Core.Features.Commands.Balance;
using Langueedu.Core.Features.Queries.BalanceQuesries.GetBalanceByUserId;
using Langueedu.Core.Interfaces;
using Langueedu.Core.Validations;
using Langueedu.SharedKernel.ViewModels.Course;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Langueedu.Core.Features.Commands.Course.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<CourseDetailViewModel>>
{
  private readonly IMediator _mediator;
  private readonly ICourseService _courseService;
  private readonly ILyricsService _lyricsService;
  private readonly ILogger<CreateCourseCommandHandler> _logger;

  public CreateCourseCommandHandler(IMediator mediator,
                                    ICourseService courseService,
                                    ILyricsService lyricsService,
                                    ILogger<CreateCourseCommandHandler> logger)
  {
    _mediator = mediator;
    _courseService = courseService;
    _lyricsService = lyricsService;
    _logger = logger;
  }

  public async Task<Result<CourseDetailViewModel>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
  {
    // Validate create course command
    var validator = new CreateCourseCommandValidatior();
    var validate = validator.Validate(request);
    if (!validate.IsValid)
      return Result<CourseDetailViewModel>.Invalid(validate.AsErrors());

    // Get user balance
    var userBalance = (await _mediator.Send(new GetBalanceByUserIdQuery(request.UserId)));
    if (!userBalance.IsSuccess)
      return Result<CourseDetailViewModel>.Invalid(userBalance.ValidationErrors);

    // Checked if the user has enough balance to enter the course
    if (userBalance.Value.Gold < request.CourseFee)
      return Result<CourseDetailViewModel>.Error("You do not have enough balance to participate in the course!");

    // Create new couse
    var course = await _courseService.CreateCourse(request);
    if (!course.IsSuccess)
      return Result<CourseDetailViewModel>.Error(course.Errors.ToArray());

    // Get lyrics
    var lyrics = await _lyricsService.GetLyricsByTrackId(request.TrackId);
    if (!lyrics.IsSuccess)
      return Result<CourseDetailViewModel>.Error(lyrics.Errors.ToArray());

    // Create balance instance based on balance type
    var balance = BalanceFactory.Create(request.BalanceType, request.UserId);
    if (balance == null)
      return Result<CourseDetailViewModel>.Error("Balance type is not found");

    // User balance has been reduced
    await _mediator.Publish(new BalanceDecreaseCommand(balance, request.CourseFee));

    _logger.LogInformation($"Course number {course.Value.Id} has been created.", course);

    return Result<CourseDetailViewModel>.Success(new CourseDetailViewModel
    {
      CourseId = course.Value.Id,
      YoutubeVideoId = course.Value.Track.YoutubeVideoId,
      Lyrics = lyrics.Value,
    });
  }
}