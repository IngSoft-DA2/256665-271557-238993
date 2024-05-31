using ILoaders;
using WebModel.Requests.LoaderRequests;

namespace IAdapter;

public interface ILoaderAdapter
{
    public void ValidateInterfaceIsBeingImplemented();
    
    public List<ILoader> GetLoaderInterfaces(LoaderRequest loaderRequest);

}