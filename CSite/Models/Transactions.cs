using System;
using System.Collections.Generic;

namespace CSite.Models
{
    public partial class Transactions
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int AccountType { get; set; }
        public long Amount { get; set; }
        public int Type { get; set; }
        public int? OperationId { get; set; }
        public int Operation { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string Notes { get; set; }
    }
}
