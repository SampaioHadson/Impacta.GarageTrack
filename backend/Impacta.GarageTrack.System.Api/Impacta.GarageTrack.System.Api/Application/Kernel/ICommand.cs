namespace Impacta.GarageTrack.System.Api.Application.Kernel
{
    // Async
    public interface ICommand
    {
        Task HandleAsync();
    }

    public interface ICommand<Request>
    {
        Task HandleAsync(Request command);
    }

    public interface ICommand<Request, Response>
    {
        Task<Response> HandleAsync(Request command);
    }

    // Sync
    public interface ISyncCommand
    {
        void Handle();
    }

    public interface ISyncCommand<Request>
    {
        void Handle(Request command);
    }

    public interface ISyncCommand<Request, Response>
    {
        Response Handle(Request command);
    }
}
