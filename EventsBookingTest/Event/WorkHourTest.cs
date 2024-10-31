using EventsBookingBackend.Domain.Event.ValueObjects;

namespace EventsBookingTests.Event;

public class WorkHourTest
{
    [Test]
    public void CheckWorkHour()
    {
        var building = new Building()
        {
            WorkHours =
            [
                new WorkHour()
                {
                    Day = DayOfWeek.Friday,
                    FromHour = 10,
                    ToHour = 18
                }
            ]
        };

        Assert.IsTrue(building.IsOpen);
    }
}