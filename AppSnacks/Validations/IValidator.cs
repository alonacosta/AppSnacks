using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSnacks.Validations
{
    public interface IValidator
    {

        string NameErro { get; set; }
        string EmailErro { get; set; }
        string PhoneErro { get; set; }
        string PasswordErro { get; set; }
        Task<bool> Validate(string name, string email,
                           string phone, string password);

    }
}
