using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Responses.FlatResponses;

namespace WebModel.Responses.OwnerResponses
{
    public class GetOwnerAssignedResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public override bool Equals(object objectToCompare)
        {
            GetOwnerAssignedResponse? toCompare = objectToCompare as GetOwnerAssignedResponse;

            if (toCompare is null) return false;

            return
                Name == toCompare.Name &&
                Lastname == toCompare.Lastname &&
                Email == toCompare.Email;
        }
    }
}