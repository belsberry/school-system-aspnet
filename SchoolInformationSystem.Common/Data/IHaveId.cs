using System;

namespace SchoolInformationSystem.Common.Data
{
    public interface IHaveId
    {
        Guid _id { get; set; }
    }
}