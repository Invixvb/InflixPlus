using System.Threading.Tasks;
using Enums;

namespace Interfaces
{
    public interface IMediaItem
    {
        MediaItemType Type { get; }
        string FileName { get; }
        Task<byte[]> GetData();
    }
}