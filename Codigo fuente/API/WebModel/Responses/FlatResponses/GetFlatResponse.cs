using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Responses.OwnerResponses;
using WebModel.Responses;

namespace WebModel.Responses.FlatResponses
{
    public class GetFlatResponse
    {
        public Guid Id { get; set; }
        public int Floor { get; set; }
        public string RoomNumber { get; set; }
        public GetOwnerResponse? OwnerAssigned { get; set; }
        public int TotalRooms { get; set; }
        public int TotalBaths { get; set; }
        public bool HasTerrace { get; set; }

        public override bool Equals(object objectToCompare)
        {
            GetFlatResponse? toCompare = objectToCompare as GetFlatResponse;
            
            bool ownerEquals = OwnerAssigned == null && toCompare.OwnerAssigned == null ||
                               OwnerAssigned != null && toCompare.OwnerAssigned != null && OwnerAssigned.Equals(toCompare.OwnerAssigned);

            return Id == toCompare.Id &&
                   Floor == toCompare.Floor &&
                   RoomNumber == toCompare.RoomNumber &&
                   ownerEquals && 
                   TotalRooms == toCompare.TotalRooms &&
                   TotalBaths == toCompare.TotalBaths &&
                   HasTerrace == toCompare.HasTerrace;
        }

    }
}