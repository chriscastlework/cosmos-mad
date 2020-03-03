using System;

namespace CustomLogic.Core.Interfaces
{
    /// <summary>
    /// Some basic user information will need to be decided on each project
    /// </summary>
    public interface ICoreUser
    {
        Guid Id { get; set; }
    }
}
