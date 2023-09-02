namespace _2.UsersManagement.Application.DTOs.Distributors.Application.Distributor.in_Services
{
    public class ValidateCostsDto
    {
        public bool HasSalesXp { get; set; }
        public bool Has5K { get; set; }
        public bool Has10K { get; set; }
        
        private bool _canBePersonalCredit;
        public bool CanBePersonalCredit 
        { 
            get => Has5K;
            set => _canBePersonalCredit = value;
        }

        private bool _canBeDistributorCredit;
        public bool CanBeDistributorCredit 
        { 
            get
            {
                switch (HasSalesXp)
                {
                    case true when Has5K:
                    case false when Has5K && Has10K:
                        return true;
                    default:
                        return false;
                }
            }
            set => _canBeDistributorCredit = value;
        }
    }
}