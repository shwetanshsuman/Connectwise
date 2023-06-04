﻿using System.ComponentModel.DataAnnotations;

namespace ConnectWiseBackend.Models
{
    public class MasterEmail
    {
        [Key]
        public int MasterEmailId { get; set; }
        public string EmailName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }
}
