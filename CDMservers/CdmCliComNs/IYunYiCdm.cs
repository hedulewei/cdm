using System.Runtime.InteropServices;

namespace YunYiCdm
{
  //  [Guid("6E8ADBC6-2E94-4B6C-8849-5A55359E863B")]
  //  [Guid("A20C0293-DE81-486C-85FD-37963B117B9D")]
    [Guid("51C070B9-B675-440E-9CE4-540692842EA3")]
    public interface IYunYiCdm
    {
        [DispId(1)]
        string RestHttpClientGet(string host, string method, string param);
        [DispId(2)]
        string SendRestHttpClientRequest(string host, string method, string param);

        [DispId(4)]
        string JsonserializeEx(string countyCode, string userName, string password, string fileName, int kind, int id,
            string absoluteFileName);
    }
}