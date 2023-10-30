namespace BTLWEB.Models
{
    public class ExpenseList
    {
        public List<Expense> Expenses { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}
