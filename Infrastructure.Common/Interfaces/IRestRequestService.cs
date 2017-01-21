namespace Infrastructure.Common.Interfaces
{
    public interface IRestRequestService
    {
        /// <summary>
        /// Sends Get request to WebAPI and returns obtained data.
        /// </summary>
        T Get<T>(string url);
    }
}