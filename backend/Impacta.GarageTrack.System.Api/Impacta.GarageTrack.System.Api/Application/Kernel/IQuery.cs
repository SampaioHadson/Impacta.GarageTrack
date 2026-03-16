namespace Impacta.GarageTrack.System.Api.Application.Kernel
{
    public interface IQuery<Response>
    {
        Task<Response> Handle();
    }

    public interface IQuery<Filter, Response>
    {
        Task<Response> Handle(Filter request);
    }

    public interface ISyncQuery<Response>
    {
        Response Handle();
    }

    public interface ISyncQuery<Filter, Response>
    {
        Response Handle(Filter request);
    }
}
