using Gml.Web.Api.Core.Options;
using Gml.Web.Api.Core.Services;
using Microsoft.Extensions.Options;

namespace Gml.Web.Api.Core.Handlers;

public interface IMinecraftHandler
{
    public Task<IResult> GetMetaData(ISystemService systemService, IOptions<ServerSettings> options);
}