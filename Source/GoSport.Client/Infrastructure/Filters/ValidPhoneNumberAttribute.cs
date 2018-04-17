using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web;

namespace GoSport.Client.Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidPhoneNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value  == null)
            {
                return false;
            }
            
            var numberAsString = value as string;

            return Regex.Match(numberAsString, "[0-9]*").Success;
        }
    }
}