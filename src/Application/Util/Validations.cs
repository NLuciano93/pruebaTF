using System;
using System.Linq;

namespace Fusap.TimeSheet.Application.Util
{
    public class Validations
    {
        protected Validations() { }

        public static bool IsValidOrderType(string? sortColumn)
        {
            if (string.IsNullOrEmpty(sortColumn))
            {
                return false;
            }

            string[] orderTypes = { "ASC", "DESC" };
            return orderTypes
                .Contains(sortColumn.ToUpper());
        }
    }
}
