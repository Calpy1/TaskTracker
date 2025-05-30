﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTrackerCLI.Models
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string CreatedAt { get; set; }
        public string DeadlineAt { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }

        public TaskItem(string title, string? description, string createdAt, string deadlineAt, TaskPriority priority, TaskStatus status)
        {
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            DeadlineAt = deadlineAt;
            Priority = priority;
            Status = status;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Description))
            {
                return $"Title: {Title}\n" +
                    $"Created at: {CreatedAt}\n" +
                    $"Deadline: {DeadlineAt}\n" +
                    $"Priority: {Priority}\n" +
                    $"Status: {Status}\n";
            }

            return $"Title: {Title}\n" +
                $"Description: {Description}\n" +
                $"Created at: {CreatedAt}\n" +
                $"Deadline: {DeadlineAt}\n" +
                $"Priority: {Priority}\n" +
                $"Status: {Status}\n";
        }
    }
}
