﻿namespace ECERP.Models
{
    using System;
    using Entities;

    public interface IEntity
    {
        object Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        string CreatedById { get; set; }
        string ModifiedById { get; set; }
        byte[] Version { get; set; }
        ApplicationUser CreatedBy { get; set; }
        ApplicationUser ModifiedBy { get; set; }
    }

    public interface IEntity<T> : IEntity
    {
        new T Id { get; set; }
    }
}