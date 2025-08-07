namespace itransition_task6_server.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<string> AddPhoto(IFormFile file);
    }
}
