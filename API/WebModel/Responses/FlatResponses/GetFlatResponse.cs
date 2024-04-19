using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Responses.OwnerResponses;
using WebModel.Responses;
using WebModel.Responses.MaintenanceRequestResponses;

namespace WebModel.Responses.FlatResponses
{
    public class GetFlatResponse
    {
        public Guid Id { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public OwnerResponse Owner { get; set; }
        public int TotalBaths { get; set; }
        public int TotalRooms { get; set; }
        public IEnumerable<MaintenanceRequestResponse> MaintenanceRequests { get; set; }

        public override bool Equals(object objectToCompare)
        {
            GetFlatResponse? toCompare = objectToCompare as GetFlatResponse;

            if (toCompare is null) return false;

            return Id == toCompare.Id &&
                   Floor == toCompare.Floor &&
                   RoomNumber == toCompare.RoomNumber &&
                   Owner == toCompare.Owner &&
                   TotalBaths == toCompare.TotalBaths &&
                   MaintenanceRequests.SequenceEqual(toCompare.MaintenanceRequests) &&
                   TotalRooms == toCompare.TotalRooms;
        }



    }
}
