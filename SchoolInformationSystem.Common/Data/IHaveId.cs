using System;

namespace SchoolInformationSystem.Common.Data
{
    public interface IHaveId
    {
        Guid Id { get; set; }
    }
}