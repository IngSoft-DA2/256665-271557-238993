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
        public int RoomNumber { get; set; }
        public GetOwnerAssignedResponse GetOwnerAssigned { get; set; }
        public int TotalBaths { get; set; }
        public int TotalRooms { get; set; }
        public bool HasTerrace { get; set; }

        public override bool Equals(object objectToCompare)
        {
            GetFlatResponse? toCompare = objectToCompare as GetFlatResponse;

            if (toCompare is null) return false;

            return Id == toCompare.Id &&
                   Floor == toCompare.Floor &&
                   RoomNumber == toCompare.RoomNumber &&
                   GetOwnerAssigned == toCompare.GetOwnerAssigned &&
                   TotalBaths == toCompare.TotalBaths &&
                   TotalRooms == toCompare.TotalRooms &&
                   HasTerrace == toCompare.HasTerrace;
        }
    }
}