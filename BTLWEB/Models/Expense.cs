using System;
using System.Collections.Generic;

namespace BTLWEB.Models;

public partial class Expense
{
    public int ExpenseId { get; set; }

    public int? ClassId { get; set; }

    public int? SubjectId { get; set; }

    public int? ChargeAmount { get; set; }

    public virtual Class? Class { get; set; }

    public virtual Subject? Subject { get; set; }
}
