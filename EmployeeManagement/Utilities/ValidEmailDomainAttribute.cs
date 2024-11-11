using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Utilities
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string allowdomain;

        public ValidEmailDomainAttribute(string allowdomain)
        {
            this.allowdomain = allowdomain;
        }
        public override bool IsValid(object value)
        {
            string[] strings= value.ToString().Split('@');
            return strings[1].ToUpper()==allowdomain.ToUpper();
        }
    }
}
