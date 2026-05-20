namespace Event___Ticketing_Management_System.Models.PersonalEvents
{
    public class PlanningBudget
    {
        public decimal TotalBudget { get; set; }

        public decimal EstimatedCost { get; set; }

        public decimal ActualCost { get; set; }

        public decimal RemainingBudget => TotalBudget - ActualCost;
    }
}