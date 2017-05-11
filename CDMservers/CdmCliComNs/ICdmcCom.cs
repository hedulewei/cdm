using System.Runtime.InteropServices;

namespace CdmCliComNs
{
  //  [Guid("6E8ADBC6-2E94-4B6C-8849-5A55359E863B")]
    [Guid("A20C0293-DE81-486C-85FD-37963B117B9D")]
    public interface ICdmcCom
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