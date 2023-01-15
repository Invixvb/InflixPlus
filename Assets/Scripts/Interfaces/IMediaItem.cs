using System.Threading.Tasks;
using Enums;
using Firebase.Storage;

namespace Interfaces
{
    public interface IMediaItem
    {
        MediaItemType Type { get; }
        string FileName { get; }
        StorageReference ReferenceLink { get; }
        Task<byte[]> GetData();
    }
}