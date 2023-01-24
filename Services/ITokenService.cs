using OsnTestApp.Data;

namespace OsnTestApp.Services
{
    public interface ITokenService
    {
        string CreateToken(Student student);
    }
}
