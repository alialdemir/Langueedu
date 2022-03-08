using Ardalis.Result;
using Langueedu.Core.Entities.BalanceAggregate;
using Langueedu.Core.Entities.CourseAggregate;
using Langueedu.Core.Enums;
using Langueedu.Core.Features.Commands.Course.CreateCourse;
using Langueedu.Core.Features.Queries.BalanceQuesries.GetBalanceByUserId;
using Langueedu.Core.Interfaces;
using Langueedu.SharedKernel.ViewModels.Course;
using MediatR;
using Moq;
using Xunit;

namespace Langueedu.UnitTests.Core.Handlers;

public class CreateCourseCommandHandlerHandle
{
  private readonly CreateCourseCommandHandler _handler;
  private readonly Mock<ICourseService> _courseService = new();
  private readonly Mock<ILyricsService> _lyricsService = new();
  private readonly Mock<IMediator> _mediator = new();
  private readonly Mock<IAppLogger<CreateCourseCommandHandler>> _logger = new();
  private readonly GetBalanceByUserIdQuery _getBalanceByUserIdQuery = new(Constants.UserId);
  private readonly Balance _balance = new BalanceGold(Constants.UserId);
  private readonly Result<Course> _course;
  private readonly Result<IEnumerable<LyricsViewModel>> _lyrics;

  public CreateCourseCommandHandlerHandle()
  {
    _balance.Increase(1000);
    _course = new(new Course(BalanceTypes.Gold,
                             CourseLevel.Advanced,
                             CourseModes.GapFilling,
                             100));
    _course.Value.Id = 1;
    _course.Value.Track = new("", "", 1);
    _course.Value.Track.Id = 1;


    _lyrics = Result<IEnumerable<LyricsViewModel>>.Success(new List<LyricsViewModel>
    {
      new LyricsViewModel(),
      new LyricsViewModel(),
      new LyricsViewModel(),
    });

    _mediator
      .Setup(x => x.Send(It.IsAny<GetBalanceByUserIdQuery>(), default).Result)
      .Returns(_balance);

    _courseService
      .Setup(x => x.CreateCourse(It.IsAny<CreateCourseCommand>()).Result)
      .Returns(_course);

    _lyricsService
      .Setup(x => x.GetLyricsByTrackId(It.IsAny<short>()).Result)
      .Returns(_lyrics);

    _handler = new CreateCourseCommandHandler(_mediator.Object,
                                              _courseService.Object,
                                              _lyricsService.Object,
                                              _logger.Object);
  }

  [Fact]
  public async Task SendsAddGivenVerify()
  {
    var command = new CreateCourseCommand(BalanceTypes.Gold,
                                          Constants.UserId,
                                          trackId: 1,
                                          CourseModes.GapFilling,
                                          CourseLevel.Advanced,
                                          courseFee: 100);

    await _handler.Handle(command, CancellationToken.None);

    _lyricsService.Verify(sender => sender.GetLyricsByTrackId(1));

    _courseService.Verify(sender => sender.CreateCourse(command));
  }
}

