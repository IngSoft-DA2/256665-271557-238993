﻿using ILoaders;
using WebModel.Requests.LoaderRequests;
using WebModel.Responses.BuildingResponses;

namespace IAdapter;

public interface ILoaderAdapter
{
    public void ValidateInterfaceIsBeingImplemented();
    
    public List<ILoader> GetAllLoaders();
    public List<CreateBuildingResponse> CreateAllBuildingsFromLoad(CreateLoaderRequest createLoaderRequest);

}