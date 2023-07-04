using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;

using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks;

public static class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
    {
        var leaveTypes = new List<LeaveType>()
        {
            new LeaveType { Id = 1, Name = "Annual", DefaultDays = 10, DateCreated = DateTime.Now },
            new LeaveType { Id = 2, Name = "Sick", DefaultDays = 10, DateCreated = DateTime.Now },
            new LeaveType { Id = 3, Name = "Maternity", DefaultDays = 10, DateCreated = DateTime.Now },
            new LeaveType { Id = 4, Name = "Paternity", DefaultDays = 10, DateCreated = DateTime.Now },
            new LeaveType { Id = 5, Name = "Unpaid", DefaultDays = 10, DateCreated = DateTime.Now },
        };

        var mockLeaveTypeRepository = new Mock<ILeaveTypeRepository>();

        mockLeaveTypeRepository
            .Setup(repo => repo.GetAsync(default))
            .ReturnsAsync(leaveTypes);

        mockLeaveTypeRepository
            .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((int id) => leaveTypes.Find(x => x.Id == id));

        mockLeaveTypeRepository
            .Setup(repo => repo.CreateAsync(It.IsAny<LeaveType>()))
            .Returns((LeaveType leaveType) =>
            {
                leaveType.Id = leaveTypes.Max(x => x.Id) + 1;
                leaveType.DateCreated = DateTime.Now;
                leaveType.DateModified = DateTime.Now;
                leaveTypes.Add(leaveType);
                return Task.CompletedTask;
            });

        mockLeaveTypeRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<LeaveType>()))
            .Returns((LeaveType leaveType) =>
            {
                leaveType.DateModified = DateTime.Now;

                var index = leaveTypes.FindIndex(x => x.Id == leaveType.Id);

                if (index >= 0)
                {
                    leaveTypes[index] = leaveType;
                }

                return Task.CompletedTask;
            });

        mockLeaveTypeRepository
            .Setup(repo => repo.DeleteAsync(It.IsAny<LeaveType>()))
            .Returns((LeaveType leaveType) =>
            {
                var index = leaveTypes.FindIndex(x => x.Id == leaveType.Id);

                if (index >= 0)
                {
                    leaveTypes.RemoveAt(index);
                }

                return Task.CompletedTask;
            });

        return mockLeaveTypeRepository;
    }
}