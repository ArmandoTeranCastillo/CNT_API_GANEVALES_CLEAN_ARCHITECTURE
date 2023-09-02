using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _1.UsersManagement.Domain.Models.Addresses;
using _1.UsersManagement.Domain.Models.Distributors;
using _3.UsersManagement.Infrastructure__EFCore_.External;
using UsersManagement.Common.Utilities.Enums;

namespace _3.UsersManagement.Infrastructure__EFCore_.Services.Distributors.Application.Utilities
{
    public abstract class ApplyDvUtilities
    {
        public static string CreateSequenceDays(string str)
        {
            var days = str.Split(',');
            var sb = new StringBuilder();
            foreach (var day in days)
            {
                var success = Enum.TryParse(day.Trim(), out DayOfWeekEnum dayEnum);
                if (!success) continue;
                sb.Append((int)dayEnum);
                sb.Append(',');
            }
            return sb.ToString().TrimEnd(',');
        }
        
        public static string SumTotalIncome(JobInfo spouseJob, JobInfo job, IEnumerable<SalesXp> salesXps)
        {
            decimal totalIncome = 0;
            if (spouseJob != null)
            {
                var spouseDIncome = Cipher.StringEncrypting(spouseJob.Income);
                if (decimal.TryParse(spouseDIncome, out var spouseIncome))
                    totalIncome += spouseIncome;
            }
            if (job != null)
            {
                var distributorDIncome = Cipher.StringEncrypting(job.Income);
                if (decimal.TryParse(distributorDIncome, out var distributorIncome))
                    totalIncome += distributorIncome;
            }
            if (salesXps != null)
            {
                totalIncome += salesXps
                    .Select(salesXp => decimal.TryParse(Cipher.StringEncrypting(salesXp.Comission), out var salesXpIncome) ? salesXpIncome : 0)
                    .Sum();
            }
            return totalIncome.ToString("N2");
        }

        public static SalesXp EncryptSalesXp(SalesXp sales)
        {
            sales.company_name = Cipher.StringEncrypting(sales.company_name);
            sales.Limit = Cipher.StringDecrypting(sales.Limit);    
            sales.Comission = Cipher.StringEncrypting(sales.Comission);
            return sales;
        }
        
        public static Prospect EncryptProspect(Prospect prospect)
        {
            prospect.Bills = Cipher.StringEncrypting(prospect.Bills);
            prospect.Services = Cipher.StringEncrypting(prospect.Services);
            prospect.House = Cipher.StringEncrypting(prospect.House);
            return prospect;
        }
    }
}