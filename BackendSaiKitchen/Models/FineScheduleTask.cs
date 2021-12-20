using System;
using System.Collections.Generic;

#nullable disable

namespace BackendSaiKitchen.Models
{
    public partial class FineScheduleTask
    {
        public string Id { get; set; }
        public string BackupFilePath { get; set; }
        public string ConditionParameter { get; set; }
        public string Creator { get; set; }
        public bool? Editable { get; set; }
        public int? FileClearCount { get; set; }
        public DateTime? NextFireTime { get; set; }
        public string OutputStr { get; set; }
        public DateTime? PreFireTime { get; set; }
        public int? RepeatTime { get; set; }
        public int? RepeatTimes { get; set; }
        public string ScheduleOutput { get; set; }
        public bool? SendBackupFile { get; set; }
        public int? ShowType { get; set; }
        public string TaskCondition { get; set; }
        public string TaskDescription { get; set; }
        public string TaskName { get; set; }
        public string TaskParameter { get; set; }
        public int? TaskState { get; set; }
        public int? TaskType { get; set; }
        public string TemplatePath { get; set; }
        public string TriggerGroup { get; set; }
        public string UserGroup { get; set; }
    }
}
