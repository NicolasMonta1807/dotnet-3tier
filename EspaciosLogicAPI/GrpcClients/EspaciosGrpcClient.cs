using Grpc.Net.Client;

namespace EspaciosLogicAPI.GrpcClients;

public class EspaciosGrpcClient
{
    private readonly Espacios.EspaciosClient _client;

    public EspaciosGrpcClient(string grpcUrl)
    {
        var channel = GrpcChannel.ForAddress(grpcUrl);
        _client = new Espacios.EspaciosClient(channel);
    }

    public async Task<GetAllEspaciosResponse> GetAllEspaciosAsync()
    {
        return await _client.GetAllEspaciosAsync(new GetAllEspaciosRequest());
    }

    public async Task<GetEspacioByIdResponse> GetEspacioByIdAsync(int id)
    {
        return await _client.GetEspacioByIdAsync(new GetEspacioByIdRequest { Id = id });
    }

    public async Task<CreateEspacioResponse> CreateEspacioAsync(CreateEspacioRequest request)
    {
        return await _client.CreateEspacioAsync(request);
    }

    public async Task<UpdateEspacioResponse> UpdateEspacioAsync(UpdateEspacioRequest request)
    {
        return await _client.UpdateEspacioAsync(request);
    }

    public async Task<DeleteEspacioResponse> DeleteEspacioAsync(int id)
    {
        return await _client.DeleteEspacioAsync(new DeleteEspacioRequest { Id = id });
    }
}